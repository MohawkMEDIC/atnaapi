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
 * Author: khannan
 */

using System.Xml.Serialization;

namespace AtnaApi.Model
{
	/// <summary>
	/// Represents event type codes.
	/// </summary>
	public enum EventTypeCode
	{
		/// <summary>
		/// Represents application activity.
		/// </summary>
		[XmlEnum("SecurityAuditCode-ApplicationActivity")]
		ApplicationActivity,

		/// <summary>
		/// Represents an audit log being used.
		/// </summary>
		[XmlEnum("SecurityAuditCode-AuditLogUsed")]
		AuditLogUsed,

		/// <summary>
		/// Represents an export.
		/// </summary>
		[XmlEnum("SecurityAuditCode-Export")]
		Export,

		/// <summary>
		/// Represents an import.
		/// </summary>
		[XmlEnum("SecurityAuditCode-Import")]
		Import,

		/// <summary>
		/// Represents network activity.
		/// </summary>
		[XmlEnum("SecurityAuditCode-NetworkActivity")]
		NetworkActivity,

		/// <summary>
		/// Represents query activity.
		/// </summary>
		[XmlEnum("SecurityAuditCode-Query")]
		Query,

		/// <summary>
		/// Represents a security alert.
		/// </summary>
		[XmlEnum("SecurityAuditCode-SecurityAlert")]
		SecurityAlert,

		/// <summary>
		/// Represents user authentication.
		/// </summary>
		[XmlEnum("SecurityAuditCode-UserAuthentication")]
		UserAuthentication,

		/// <summary>
		/// Represents the application starting up.
		/// </summary>
		[XmlEnum("SecurityAuditCode-ApplicationStart")]
		ApplicationStart,

		/// <summary>
		/// Represents the application stopping.
		/// </summary>
		[XmlEnum("SecurityAuditCode-ApplicationStop")]
		ApplicationStop,

		/// <summary>
		/// Represents login activity.
		/// </summary>
		[XmlEnum("SecurityAuditCode-Login")]
		Login,

		/// <summary>
		/// Represents logout activity.
		/// </summary>
		[XmlEnum("SecurityAuditCode-Logout")]
		Logout,

		/// <summary>
		/// Represents node authentication.
		/// </summary>
		[XmlEnum("SecurityAuditCode-NodeAuthentication")]
		NodeAuthentication,

		/// <summary>
		/// Represents security attributes being changed.
		/// </summary>
		[XmlEnum("SecurityAuditCode-Securityattributeschanged")]
		SecurityAttributesChanged,

		/// <summary>
		/// Represents security roles being changed.
		/// </summary>
		[XmlEnum("SecurityAuditCode-Securityroleschanged")]
		SecurityRolesChanged,

		/// <summary>
		/// Represents user security attributes being changed.
		/// </summary>
		[XmlEnum("SecurityAuditCode-Usersecurityattributechanged")]
		UserSecurityChanged
	}
}