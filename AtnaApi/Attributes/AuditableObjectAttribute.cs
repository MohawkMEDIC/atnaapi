/* 
 * MEDIC ATNA API 
 * Copyright 2014-2015 Mohawk College of Applied Arts and Technology.
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
using System.Xml.Serialization;
using System.IO;

namespace AtnaApi.Attributes
{
    /// <summary>
    /// Classifies the type of data that a tagged property contains
    /// </summary>
    public enum ElementClassificationType
    {
        /// <summary>
        /// Data contained is an identifier
        /// </summary>
        Identifier,
        /// <summary>
        /// Data contained is a query string
        /// </summary>
        Query,
        /// <summary>
        /// Data contained is a special object
        /// </summary>
        Object
    }

    /// <summary>
    /// Represents an attribute which can be affixed to the auditable object
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AuditableObjectAttribute : Attribute
    {
        /// <summary>
        /// Create a new auditable object type
        /// </summary>
        public AuditableObjectAttribute(AuditableObjectType type, AuditableObjectRole role, AuditableObjectIdType idType)
        {
            this.DataFormat = "{0}";
            this.Type = type;
            this.Role = role;
            this.IDTypeCode = idType;
            this.Class = ElementClassificationType.Identifier;
        }

        /// <summary>
        /// Gets or sets the format of the data when placed in the audit
        /// </summary>
        public String DataFormat { get; set; }
        /// <summary>
        /// Identifies the type of object this property represents
        /// </summary>
        public AuditableObjectType Type { get; set; }
        /// <summary>
        /// Identifies the role which the object plays
        /// </summary>
        public AuditableObjectRole Role { get; set; }
        /// <summary>
        /// Identifies the type of identifier placed
        /// </summary>
        public AuditableObjectIdType IDTypeCode { get; set; }

        /// <summary>
        /// Identifies where to place the data from the object
        /// </summary>
        public ElementClassificationType Class { get; set; }

        /// <summary>
        /// Participant object name or property from which to get this information
        /// </summary>
        public String ParticipantObjectPropertyName { get; set; }

        /// <summary>
        /// True if the property is the target of the lifecycle being described
        /// </summary>
        public Boolean LifeCycleTarget { get; set; }

        /// <summary>
        /// Convert the attribute data into an auditable object
        /// </summary>
        public AuditableObject ToAuditableObject(object value)
        {
            var retVal = new AuditableObject()
            {
                IDTypeCode = new CodeValue<AuditableObjectIdType>(this.IDTypeCode),
                Role = this.Role,
                RoleSpecified = true,
                Type = this.Type, 
                TypeSpecified = true
            };

            switch (this.Class)
            {
                case ElementClassificationType.Identifier:
                    retVal.ObjectId = String.Format(this.DataFormat, value);
                    retVal.ObjectSpecChoice = ObjectDataChoiceType.ParticipantObjectName;
                    retVal.ObjectSpec = this.ParticipantObjectPropertyName;
                    break;
                case ElementClassificationType.Query:
                    retVal.ObjectSpecChoice = ObjectDataChoiceType.ParticipantObjectQuery;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        XmlSerializer xsz = new XmlSerializer(value.GetType());
                        xsz.Serialize(ms, value);
                        retVal.ObjectSpec = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                    }
                    break;
                case ElementClassificationType.Object:
                    retVal.ObjectSpecChoice = ObjectDataChoiceType.ParticipantObjectName;
                    retVal.ObjectSpec = this.ParticipantObjectPropertyName;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        XmlSerializer xsz = new XmlSerializer(value.GetType());
                        xsz.Serialize(ms, value);
                        byte[] data = new byte[ms.Length];
                        Array.Copy(ms.GetBuffer(), data, ms.Length);
                        retVal.ObjectDetail.Add(new ObjectDetailType()
                        {
                            Type = "text/xml",
                            Value = data
                        });
                    }
                    break;
            }

            return retVal;
        }
    }
}
