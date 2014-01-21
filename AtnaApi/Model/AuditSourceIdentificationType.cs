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
 * Date: 7-5-2012
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AtnaApi.Model
{
    /// <summary>
    /// Identifies the source of the audit
    /// </summary>
    public class AuditSourceIdentificationType
    {

        /// <summary>
        /// Identifies the entperprise site of the audit
        /// </summary>
        [XmlAttribute("AuditEnterpriseSiteID")]
        public string AuditEnterpriseSiteID { get; set; }

        /// <summary>
        /// Identifies the audit source id
        /// </summary>
        [XmlAttribute("AuditSourceID")]
        public string AuditSourceID { get; set; }

        /// <summary>
        /// Identifies the source type of the audit source
        /// </summary>
        [XmlElement("AuditSourceTypeCode")]
        public List<CodeValue<AuditSourceType>> AuditSourceTypeCode { get; set; }
    }
}
