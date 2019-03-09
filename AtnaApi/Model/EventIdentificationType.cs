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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace AtnaApi.Model
{
	/// <summary>
	/// Event Identifier
	/// </summary>
	public enum EventIdentifierType
	{
		[Description("Provisioning Event")]
		[Category("IHE")]
		[XmlEnum("IHE0001")]
		ProvisioningEvent,

		[Description("Medication Event")]
		[Category("IHE")]
		[XmlEnum("IHE0002")]
		MedicationEvent,

		[Description("Resource Assignment")]
		[Category("IHE")]
		[XmlEnum("IHE0003")]
		ResourceAssignment,

		[Description("Care Episode")]
		[Category("IHE")]
		[XmlEnum("IHE0004")]
		CareEpisode,

		[Description("Care Protocol")]
		[Category("IHE")]
		[XmlEnum("IHE0005")]
		CareProtocol,

		[Description("Disclosure")]
		[Category("IHE")]
		[XmlEnum("IHE0006")]
		Disclosure,

		[Description("Patient Search Activity")]
		[Category("CDT")]
		[XmlEnum("CDT-100002")]
		CDTPatientSearchActivity,

		[Description("Application Activity")]
		[Category("DCM")]
		[XmlEnum("110100")]
		ApplicationActivity,

		[Description("Audit Log Used")]
		[Category("DCM")]
		[XmlEnum("110101")]
		AuditLogUsed,

		[Description("Begin Transfer Instances")]
		[Category("DCM")]
		[XmlEnum("110102")]
		BeginTransferInstances,

		[Description("Instances Accessed")]
		[Category("DCM")]
		[XmlEnum("110103")]
		DicomInstancesAccessed,

		[Description("Instances Transferred")]
		[Category("DCM")]
		[XmlEnum("110104")]
		DicomInstancesTransffered,

		[Description("Study Deleted")]
		[Category("DCM")]
		[XmlEnum("110105")]
		StudyDeleted,

		[Description("Export")]
		[Category("DCM")]
		[XmlEnum("110106")]
		Export,

		[Description("Import")]
		[Category("DCM")]
		[XmlEnum("110107")]
		Import,

		[Description("Network Activity")]
		[Category("DCM")]
		[XmlEnum("110108")]
		NetowrkActivity,

		[Description("Order Record")]
		[Category("DCM")]
		[XmlEnum("110109")]
		OrderRecord,

		[Description("Patient Record")]
		[Category("DCM")]
		[XmlEnum("110110")]
		PatientRecord,

		[Description("Procedure Record")]
		[Category("DCM")]
		[XmlEnum("110111")]
		ProcedureRecord,

		[Description("Query")]
		[Category("DCM")]
		[XmlEnum("110112")]
		Query,

		[Description("Security Alert")]
		[Category("DCM")]
		[XmlEnum("110113")]
		SecurityAlert,

		[Description("User Authentication")]
		[Category("DCM")]
		[XmlEnum("110114")]
		UserAuthentication,

		[Description("Emergency Override Started")]
		[Category("DCM")]
		[XmlEnum("110127")]
		EmergencyOverrideStarted,

		[Description("Use of a restricted function")]
		[Category("DCM")]
		[XmlEnum("110132")]
		UseOfRestrictedFunction,

		[Description("Login")]
		[Category("DCM")]
		[XmlEnum("110122")]
		Login,

		[Description("Logout")]
		[Category("DCM")]
		[XmlEnum("110123")]
		Logout
	}

	/// <summary>
	/// Represents an event identification type
	/// </summary>
	public class EventIdentificationType
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public EventIdentificationType()
		{
			this.EventType = new List<CodeValue<string>>();
		}

		/// <summary>
		/// Represents the event action
		/// </summary>
		[XmlAttribute("EventActionCode")]
		public ActionType ActionCode { get; set; }

		/// <summary>
		/// Represents the date/time of the event
		/// </summary>
		[XmlAttribute("EventDateTime")]
		public DateTime EventDateTime { get; set; }

		/// <summary>
		/// Represents the id of the event
		/// </summary>
		[XmlElement("EventID")]
		public CodeValue<EventIdentifierType> EventId { get; set; }

		/// <summary>
		/// Represents the event outcome
		/// </summary>
		[XmlAttribute("EventOutcomeIndicator")]
		public OutcomeIndicator EventOutcome { get; set; }

		/// <summary>
		/// Represents the event type
		/// </summary>
		[XmlElement("EventTypeCode")]
		public List<CodeValue<String>> EventType { get; set; }
	}
}