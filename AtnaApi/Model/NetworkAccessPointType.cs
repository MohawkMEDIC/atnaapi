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
    /// Represents the type of network access point
    /// </summary>
    public enum NetworkAccessPointType
    {
        /// <summary>
        /// The identifier is a machine name
        /// </summary>
        [XmlEnum("1")]
        MachineName = 0x1,
        /// <summary>
        /// Identifier is an IP address
        /// </summary>
        [XmlEnum("2")]
        IPAddress = 0x2,
        /// <summary>
        /// Identifier is a telephone number
        /// </summary>
        [XmlEnum("3")]
        TelephoneNumber = 0x3,
        /// <summary>
        /// Unknown
        /// </summary>
        [XmlEnum("")]
        Unknown = 0x0
    }
}
