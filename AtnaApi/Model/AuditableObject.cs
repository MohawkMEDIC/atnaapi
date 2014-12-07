/**
 * Copyright 2012-2013 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 23-8-2012
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AtnaApi.Model
{
    /// <summary>
    /// Identifies an object that adds context to the audit
    /// </summary>
    public class AuditableObject
    {
        /// <summary>
        /// Creates a new auditable object
        /// </summary>
        public AuditableObject()
        {
            this.ObjectDetail = new List<ObjectDetailType>();
        }

        /// <summary>
        /// Identifies the object in the event
        /// </summary>
        [XmlAttribute("ParticipantObjectID")]
        public string ObjectId { get; set; }
        /// <summary>
        /// Identifies the type of object being expressed
        /// </summary>
        [XmlAttribute("ParticipantObjectTypeCode")]
        public AuditableObjectType Type { get; set; }
        [XmlIgnore]
        public bool TypeSpecified { get; set; }
        /// <summary>
        /// Identifies the role type of the object
        /// </summary>
        [XmlAttribute("ParticipantObjectTypeCodeRole")]
        public AuditableObjectRole Role { get; set; }
        [XmlIgnore]
        public bool RoleSpecified { get; set; }
        /// <summary>
        /// Identifies where in the lifecycle of the object this object is currently within
        /// </summary>
        [XmlAttribute("ParticipantObjectDataLifeCycle")]
        public AuditableObjectLifecycle LifecycleType { get; set; }
        [XmlIgnore]
        public bool LifecycleTypeSpecified { get; set; }
        /// <summary>
        /// Identifies the type of identifier supplied
        /// </summary>
        [XmlElement("ParticipantObjectIDTypeCode")]
        public CodeValue<AuditableObjectIdType> IDTypeCode { get; set; }
        /// <summary>
        /// Participant object query
        /// </summary>
        [XmlElement("ParticipantObjectQuery")]
        [XmlElement("ParticipantObjectName")]
        [XmlChoiceIdentifier("ObjectSpecChoice")]
        public string ObjectSpec { get; set; }

        /// <summary>
        /// Identifies the additional detail for the object
        /// </summary>
        [XmlElement("ParticipantObjectDetail")]
        public List<ObjectDetailType> ObjectDetail { get; set; }

        /// <summary>
        /// Gets or sets the audit choice
        /// </summary>
        [XmlIgnore]
        public ObjectDataChoiceType ObjectSpecChoice { get; set; }
    }
}
