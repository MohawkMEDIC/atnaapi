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

namespace AtnaApi.Attributes
{
    /// <summary>
    /// Represents and identifies the fields that the particular data element belongs.
    /// </summary>
    public enum EventIdentificationFieldType
    {
        EventDateTime,
        EventType,
        EventId,
        EventOutcome
    }

    /// <summary>
    /// Represents and identifies the event identification field to be populated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EventIdentificationFieldAttribute : Attribute
    {
        /// <summary>
        /// Event field type
        /// </summary>
        public EventIdentificationFieldAttribute(EventIdentificationFieldType type)
        {
            this.FieldType = type;
        }

        /// <summary>
        /// Identifies the format of the target data
        /// </summary>
        public String DataFormat { get; set; }

        /// <summary>
        /// Identifies the type of field the data represents
        /// </summary>
        public EventIdentificationFieldType FieldType { get; set; }
    }
}
