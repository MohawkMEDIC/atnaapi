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

namespace AtnaApi.Model
{
	/// <summary>
	/// Represents codified data.
	/// </summary>
	public class AuditCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AuditCode"/> class.
		/// </summary>
		public AuditCode()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuditCode"/> class.
		/// </summary>
		/// <param name="code">The code.</param>
		public AuditCode(string code) : this()
		{
			this.Code = code;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuditCode"/> class.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="codeSystem">The code system.</param>
		public AuditCode(string code, string codeSystem) : this(code)
		{
			this.CodeSystem = codeSystem;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuditCode"/> class.
		/// </summary>
		/// <param name="codeSystem">The code system.</param>
		/// <param name="code">The code.</param>
		/// <param name="displayName">The display name.</param>
		public AuditCode(string codeSystem, string code, string displayName) : this(code, codeSystem)
		{
			this.DisplayName = displayName;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuditCode"/> class.
		/// </summary>
		/// <param name="codeValue">The code value.</param>
		public AuditCode(CodeValue<string> codeValue) : this(codeValue.Code, codeValue.CodeSystem, codeValue.DisplayName)
		{
		}

		/// <summary>
		/// Gets or sets the codified data.
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Gets or sets the OID of the code system from which the code is pulled.
		/// </summary>
		public string CodeSystem { get; set; }

		/// <summary>
		/// Gets or sets the english display name for the code.
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// Converts the audit code to a code value.
		/// </summary>
		/// <returns>Returns a code value.</returns>
		public CodeValue<string> ToCodeValue()
		{
			return new CodeValue<string>(this.Code, this.CodeSystem, this.DisplayName);
		}
	}
}