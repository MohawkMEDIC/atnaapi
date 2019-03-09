/* 
 * MEDIC ATNA API 
 * Copyright 2014-2019 Mohawk College of Applied Arts and Technology.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * Author: Justin
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtnaApi.Model;
using AtnaApi.Attributes;
using System.Reflection;
using System.Diagnostics;
using System.Collections;

namespace AtnaApi
{
    /// <summary>
    /// Represents a utility that can be used to assist in the construction of audits
    /// </summary>
    public static class AuditUtil
    {
        /// <summary>
        /// Constructs an audit message with basic information attained via the properties in the <paramref name="auditableObject"/>
        /// instance
        /// </summary>
        /// <param name="auditableObject"></param>
        /// <returns></returns>
        public static AuditMessage GenerateAuditObjects(List<object> auditableObject, ActionType action, OutcomeIndicator outcome, EventIdentifierType id, CodeValue<String> typeCode)
        {
            // Construct the basic audit message
            AuditMessage retVal = new AuditMessage(DateTime.Now, action, outcome, id, typeCode);
            retVal.AuditableObjects = new List<AuditableObject>();

            AuditableObjectLifecycle lifecycle = AuditableObjectLifecycle.Export;
            switch (action)
            {
                case ActionType.Create:
                    lifecycle = AuditableObjectLifecycle.Creation;
                    break;
                case ActionType.Delete:
                    lifecycle = AuditableObjectLifecycle.PermanentErasure;
                    break;
                case ActionType.Execute:
                    lifecycle = AuditableObjectLifecycle.Access;
                    break;
                case ActionType.Read:
                    lifecycle = outcome != OutcomeIndicator.Success ? AuditableObjectLifecycle.Access : AuditableObjectLifecycle.Disclosure;
                    break;
                case ActionType.Update:
                    lifecycle = AuditableObjectLifecycle.Amendment;
                    break;
            }

            // Reflect 
            foreach (var itm in auditableObject)
                GenerateAuditObjectsInternal(itm, lifecycle, retVal.AuditableObjects);

            return retVal;

        }
        /// <summary>
        /// Generate an auditable object internal
        /// </summary>
        private static void GenerateAuditObjectsInternal(object auditableObject, AuditableObjectLifecycle? lifecycle, List<AuditableObject> workingList)
        {
            Type auditType = auditableObject.GetType();
            if (auditType.Namespace.StartsWith("System")) return; // Do not analyze system objects

            foreach (var pi in auditType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                // No data then no audit info
                var instanceValue = pi.GetValue(auditableObject, null);
                if (instanceValue != null)
                {

                    object[] customAttributes = pi.GetCustomAttributes(typeof(AuditableObjectAttribute), false);
                    if (customAttributes.Length > 0)
                    {
                        AuditableObjectAttribute aoatt = customAttributes[0] as AuditableObjectAttribute;


                        // Now build the object
                        var auditObj = aoatt.ToAuditableObject(instanceValue);
                        // Lifecycle
                        if (aoatt.LifeCycleTarget && lifecycle.HasValue)
                        {
                            auditObj.LifecycleType = lifecycle.Value;
                            auditObj.LifecycleTypeSpecified = true;
                        }

                        // Attempt to bind the name
                        if (!String.IsNullOrEmpty(auditObj.ObjectSpec) && auditObj.ObjectSpecChoice == ObjectDataChoiceType.ParticipantObjectName)
                        {
                            StringBuilder displayNameBuilder = new StringBuilder();
                            foreach (var pname in auditObj.ObjectSpec.Split('|'))
                            {
                                var objectSpecPi = auditType.GetProperty(pname);
                                if (objectSpecPi != null)
                                    displayNameBuilder.AppendFormat("{0} ", objectSpecPi.GetValue(auditableObject, null));

                            }
                            auditObj.ObjectSpec = displayNameBuilder.ToString();
                        }

                        if (workingList.Exists(i => i.ObjectId == auditObj.ObjectId && i.Role == auditObj.Role && i.Type == auditObj.Type))
                            Trace.TraceWarning("Already informed audit of object {0}.{1}, skipping", auditObj.ObjectSpec, auditObj.ObjectId);
                        else
                            workingList.Add(auditObj);
                    } // Auditable object construction

                    // Cascade down ... 
                    if (instanceValue is IList)
                        foreach (var itm in instanceValue as IEnumerable)
                            GenerateAuditObjectsInternal(itm, null, workingList);
                    else
                        GenerateAuditObjectsInternal(instanceValue, null, workingList);
                }
            }

        }

    }
}
