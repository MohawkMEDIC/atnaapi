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
using System.Xml.Serialization;

namespace AtnaApi.Model
{
    /// <summary>
    /// Identifies roles of objects in the audit event
    /// </summary>
    public enum AuditableObjectRole
    {
        /// <remarks>Use with object type Person</remarks>
        [XmlEnum("1")]
        Patient = 0x01,
        /// <remarks>Use with object type Organization</remarks>
        [XmlEnum("2")]
        Location = 0x02,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("3")]
        Report = 0x03,
        /// <remarks>Use with object type Person or Organization</remarks>
        [XmlEnum("4")]
        Resource = 0x04,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("5")]
        MasterFile = 0x05,
        /// <remarks>Use with object type Person, SysObject</remarks>
        [XmlEnum("6")]
        User = 0x06,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("7")]
        List = 0x07,
        /// <remarks>Use with object type Person</remarks>
        [XmlEnum("8")]
        Doctor = 0x08,
        /// <remarks>Use with object type Organization</remarks>
        [XmlEnum("9")]
        Subscriber = 0x09,
        /// <remarks>Use with object type Person, Organization</remarks>
        [XmlEnum("10")]
        Guarantor = 0x0a,
        /// <remarks>Use with object type SyOBject</remarks>
        [XmlEnum("11")]
        SecurityUser = 0x0b,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("12")]
        SecurityGroup = 0x0c,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("13")]
        SecurityResource = 0x0d,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("14")]
        SecurityGranularityDefinition = 0x0e,
        /// <remarks>Use with object type Person or Organization</remarks>
        [XmlEnum("15")]
        Provider = 0x0f,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("16")]
        DataDestination = 0x10,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("17")]
        DataRepository = 0x11,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("18")]
        Schedule = 0x12,
        /// <remarks>Use with object type Person</remarks>
        [XmlEnum("19")]
        Customer = 0x13,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("20")]
        Job = 0x14,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("21")]
        JobStream = 0x15,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("22")]
        Table = 0x16,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("23")]
        RoutingCriteria = 0x17,
        /// <remarks>Use with object type SysObject</remarks>
        [XmlEnum("24")]
        Query = 0x18
    }
}
