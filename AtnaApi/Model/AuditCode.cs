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

namespace MARC.HI.EHRS.SHR.Core.Auditing
{
    /// <summary>
    /// Represents codified data
    /// </summary>
    public class AuditCode
    {
        /// <summary>
        /// The OID of the code system from which the code is pulled
        /// </summary>
        public string CodeSystem { get; set; }
        /// <summary>
        /// The codified data
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// The english display name for the code
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        public AuditCode() { }

        /// <summary>
        /// Create a new instance of the audit code with the specified parameters
        /// </summary>
        /// <param name="code">The code</param>
        public AuditCode(string code)
            : this()
        { this.Code = code; }

        /// <summary>
        /// Create a new instance of the audit code with the specified parameters
        /// </summary>
        /// <param name="code">The code</param>
        /// <param name="codeSystem">The OID of the system from wich the code was drawn</param>
        public AuditCode(string code, string codeSystem)
            : this(code)
        { this.CodeSystem = codeSystem; }

    }
}
