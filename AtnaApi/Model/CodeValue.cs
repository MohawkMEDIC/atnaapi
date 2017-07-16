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
using System.Reflection;
using System.ComponentModel;

namespace AtnaApi.Model
{
    /// <summary>
    /// Represents codified data
    /// </summary>
    public class CodeValue<T>
    {
        // Code Syste,
        private string m_codeSystem = String.Empty;
        private string m_description = String.Empty;

        /// <summary>
        /// The OID of the code system from which the code is pulled
        /// </summary>
        [XmlAttribute("codeSystemName")]
        public string CodeSystem
        {
            get
            {
                if (!String.IsNullOrEmpty(this.m_codeSystem))
                    return this.m_codeSystem;

                // Enum type?
                if (typeof(T).IsEnum)
                {
                    FieldInfo fi = typeof(T).GetField(this.StrongCode.ToString());
                    object[] category = fi.GetCustomAttributes(typeof(CategoryAttribute), false);
                    if (category.Length > 0)
                        return (category[0] as CategoryAttribute).Category;
                }
                return null;
            }
            set
            {
                m_codeSystem = value;
            }
        }

        /// <summary>
        /// Gets or sets the codified data
        /// </summary>
        [XmlAttribute("code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// The codified data
        /// </summary>
        [XmlIgnore]
        public T StrongCode
        {
            get
            {
                if (typeof(T) == typeof(String))
                    return (T)(Object)this.Code;
                else if (typeof(T).IsEnum)
                {
                    var fi = Array.Find(typeof(T).GetFields(), o =>
                    {
                        object[] ci = o.GetCustomAttributes(typeof(XmlEnumAttribute), false);
                        if (ci.Length == 0)
                            return o.Name == this.Code;
                        else
                            return (ci[0] as XmlEnumAttribute).Name == this.Code;
                    });
                    if (fi == null)
                        return default(T);
                    else
                        return (T)fi.GetValue(null);
                }
                return default(T);
            }
            set
            {
                if (typeof(T).IsEnum)
                {
                    object[] ci = typeof(T).GetField(value.ToString()).GetCustomAttributes(typeof(XmlEnumAttribute), false);
                    if (ci.Length == 0)
                        this.Code = value.ToString();
                    else
                        this.Code = (ci[0] as XmlEnumAttribute).Name;
                }
                else
                    this.Code = value.ToString();
            }
        }

        /// <summary>
        /// The english display name for the code
        /// </summary>
        [XmlAttribute("displayName")]
        public string DisplayName
        {
            get
            {
                if (!String.IsNullOrEmpty(this.m_description))
                    return this.m_description;

                // Enum type?
                if (typeof(T).IsEnum)
                {
                    try
                    {
                        FieldInfo fi = typeof(T).GetField(this.StrongCode?.ToString());
                        object[] category = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (category.Length > 0)
                            return (category[0] as DescriptionAttribute)?.Description;
                        else
                            return this.StrongCode?.ToString();
                    }
                    catch
                    {
                        return null;
                    }

                }
                return null;
            }
            set
            {
                m_description = value;
            }
        }
        /// <summary>
        /// Identifies the code or concept in the original system 
        /// </summary>
        [XmlAttribute("originalText")]
        public string OriginalText { get; set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        public CodeValue() { }

        /// <summary>
        /// Create a new instance of the audit code with the specified parameters
        /// </summary>
        /// <param name="code">The code</param>
        public CodeValue(T code)
            : this()
        { this.StrongCode = code; }

        /// <summary>
        /// Create a new instance of the audit code with the specified parameters
        /// </summary>
        /// <param name="code">The code</param>
        /// <param name="codeSystem">The OID of the system from wich the code was drawn</param>
        public CodeValue(T code, string codeSystem)
            : this(code)
        { this.CodeSystem = codeSystem; }


        /// <summary>
        /// Create a new instance of the audit code with the specified parameters
        /// </summary>
        /// <param name="code">The code</param>
        /// <param name="codeSystem">The OID of the system from wich the code was drawn</param>
        public CodeValue(T code, string codeSystem, String displayName)
            : this(code, codeSystem)
        { this.DisplayName = displayName; }
    }
}
