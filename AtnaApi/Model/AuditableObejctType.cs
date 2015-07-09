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
    /// Identifies the type of auditable objects in the system
    /// </summary>
    public enum AuditableObjectType
    {
        [XmlEnum("1")]
        Person = 1,
        [XmlEnum("2")]
        SystemObject = 2,
        [XmlEnum("3")]
        Organization = 3, 
        [XmlEnum("4")]
        Other = 4
    }
}
