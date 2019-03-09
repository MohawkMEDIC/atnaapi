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

using System.ComponentModel;
using System.Xml.Serialization;

namespace AtnaApi.Model
{
	/// <summary>
	/// Classifies the type of identifier that a auditable object may have
	/// </summary>
	public enum AuditableObjectIdType
	{
		/// <remarks>Use with object type code Person</remarks>
		[XmlEnum("1")]
		[Category("RFC-3881")]
		MedicalRecord = 0x01,

		/// <remarks>Use with object type code Person</remarks>
		[XmlEnum("2")]
		[Category("RFC-3881")]
		PatientNumber = 0x02,

		/// <remarks>Use with object type code Person</remarks>
		[XmlEnum("3")]
		[Category("RFC-3881")]
		EncounterNumber = 0x03,

		/// <remarks>Use with object type code Person</remarks>
		[XmlEnum("4")]
		[Category("RFC-3881")]
		EnrolleeNumber = 0x04,

		/// <remarks>Use with object type code Person</remarks>
		[XmlEnum("5")]
		[Category("RFC-3881")]
		SocialSecurityNumber = 0x05,

		/// <remarks>Use with object type code Person</remarks>
		[XmlEnum("6")]
		[Category("RFC-3881")]
		AccountNumber = 0x06,

		/// <remarks>Use with object type code Person or Organization</remarks>
		[XmlEnum("7")]
		[Category("RFC-3881")]
		GuarantorNumber = 0x07,

		/// <remarks>Use with object type code SystemObject</remarks>
		[XmlEnum("8")]
		[Category("RFC-3881")]
		ReportName = 0x08,

		/// <remarks>Use with object type code SystemObject</remarks>
		[XmlEnum("9")]
		[Category("RFC-3881")]
		ReportNumber = 0x09,

		/// <remarks>Use with object type code SystemObject</remarks>
		[XmlEnum("10")]
		[Category("RFC-3881")]
		SearchCritereon = 0x0a,

		/// <remarks>Use with object type code Person or SystemObject</remarks>
		[XmlEnum("11")]
		[Category("RFC-3881")]
		UserIdentifier = 0x0b,

		/// <remarks>Use with object type code SystemObject</remarks>
		[XmlEnum("12")]
		[Category("RFC-3881")]
		Uri = 0x0c,

		/// <summary>
		/// IHE XDS Submission Set
		/// </summary>
		[XmlEnum("urn:uuid:a54d6aa5-d40d-43f9-88c5-b4633d873bdd")]
		[Category("IHE XDS Meta Data")]
		XDSSubmissionSet,

		/// <summary>
		/// IHE XDS Submission Set Author
		/// </summary>
		[XmlEnum("urn:uuid:a7058bb9-b4e4-4307-ba5b-e3f0ab85e12d")]
		[Category("IHE XDS Meta Data")]
		XDSSubmissionSetAuthor,

		/// <summary>
		/// IHE XDS Submission Set Content/Type
		/// </summary>
		[XmlEnum("urn:uuid:aa543740-bdda-424e-8c96-df4873be8500")]
		[Category("IHE XDS Meta Data")]
		XDSSubmissionSetContentTypeCode,

		/// <summary>
		/// IHE XDS Submission Set Patient ID
		/// </summary>
		[XmlEnum("urn:uuid:6b5aea1a-874d-4603-a4bc-96a0a7b38446")]
		[Category("IHE XDS Meta Data")]
		XDSSubmissionSetPatientId,

		/// <summary>
		/// IHE XDS Submission Set Source ID
		/// </summary>
		[XmlEnum("urn:uuid:554ac39e-e3fe-47fe-b233-965d2a147832")]
		[Category("IHE XDS Meta Data")]
		XDSSubmissionSetSourceId,

		/// <summary>
		/// IHE XDS Submission Set Unique ID
		/// </summary>
		[XmlEnum("urn:uuid:96fdda7c-d067-4183-912e-bf5ee74998a8")]
		[Category("IHE XDS Meta Data")]
		XDSSubmissionSetUniqueId,

		/// <summary>
		/// IHE XDS Document Entry
		/// </summary>
		[XmlEnum("urn:uuid:7edca82f-054d-47f2-a032-9b2a5b5186c1")]
		[Category("IHE XDS Meta Data")]
		XDSDocumentEntry,

		/// <summary>
		/// IHE XDS Document Entry Author
		/// </summary>
		[XmlEnum("urn:uuid:93606bcf-9494-43ec-9b4e-a7748d1a838d")]
		[Category("IHE XDS Meta Data")]
		XDSDocumentEntryAuthor,

		/// <summary>
		/// IHE XDS Document Entry Class Code
		/// </summary>
		[XmlEnum("urn:uuid:41a5887f-8865-4c09-adf7-e362475b143a")]
		[Category("IHE XDS Meta Data")]
		XDSDocumentEntryClassCode,

		/// <summary>
		/// IHE XDS Document Entry Confidentiality Code3
		/// </summary>
		[XmlEnum("urn:uuid:f4f85eac-e6cb-4883-b524-f2705394840f")]
		[Category("IHE XDS Meta Data")]
		XDSDocumentEntryConfidentialityCode,

		/// <summary>
		/// IHE XDS  Document Entry Event Code List
		/// </summary>
		[XmlEnum("urn:uuid:2c6b8cb7-8b2a-4051-b291-b1ae6a575ef4")]
		[Category("IHE XDS Meta Data")]
		XDSDocumentEntryEventCodeList,

		/// <summary>
		/// IHE XDS Document Entry Format Code
		/// </summary>
		[XmlEnum("urn:uuid:a09d5840-386c-46f2-b5ad-9c3699a4309d")]
		[Category("IHE XDS Meta Data")]
		XDSDocumentEntryFormatCode,

		/// <summary>
		/// IHE XDS Document Entry Health Care Facility Type
		/// </summary>
		[XmlEnum("urn:uuid:f33fb8ac-18af-42cc-ae0e-ed0b0bdb91e1")]
		[Category("IHE XDS Meta Data")]
		XDSDocumentEntryHealthCareFacilityTypeCode,

		/// <summary>
		/// IHE XDS Document Entry Patient Id
		/// </summary>
		[XmlEnum("urn:uuid:58a6f841-87b3-4a3e-92fd-a8ffeff98427")]
		[Category("IHE XDS Meta Data")]
		XDSDocumentEntryPatientId,

		/// <summary>
		/// IHE XDS Document Practice Setting
		/// </summary>
		[XmlEnum("urn:uuid:cccf5598-8b07-4b77-a05e-ae952c785ead")]
		[Category("IHE XDS Meta Data")]
		XDSDocumentEntryPracticeSettingCode,

		/// <summary>
		/// IHE XDS Document Entry Type
		/// </summary>
		[XmlEnum("urn:uuid:f0306f51-975f-434e-a61c-c59651d33983")]
		[Category("IHE XDS Meta Data")]
		XDSDocumentEntryTypeCode,

		/// <summary>
		/// IHE XDS Document Entry Unique Id
		/// </summary>
		[XmlEnum("urn:uuid:2e82c1f6-a085-4c72-9da3-8640a32e42ab")]
		[Category("IHE XDS Meta Data")]
		XDSDocumentEntryUniqueId,

		/// <summary>
		/// IHE XDS Folder
		/// </summary>
		[XmlEnum("urn:uuid:d9d542f3-6cc4-48b6-8870-ea235fbc94c2")]
		[Category("IHE XDS Meta Data")]
		XDSFolder,

		/// <summary>
		/// IHE XDS Folde Code
		/// </summary>
		[XmlEnum("urn:uuid:1ba97051-7806-41a8-a48b-8fce7af683c5")]
		[Category("IHE XDS Meta Data")]
		XDSFolderCodeList,

		/// <summary>
		/// IHE XDS Folder Patient ID
		/// </summary>
		[XmlEnum("urn:uuid:f64ffdf0-4b97-4e06-b79f-a52b38ec2f8a")]
		[Category("IHE XDS Meta Data")]
		XDSFolderPatientId,

		/// <summary>
		/// IHE XDS Folder Unique ID
		/// </summary>
		[XmlEnum("urn:uuid:75df8f67-9973-4fbe-a900-df66cefecc5a")]
		[Category("IHE XDS Meta Data")]
		XDSFolderUniqueId,

		[Category("IHE XDS Meta Data")]
		[XmlEnum("urn:uuid:917dc511-f7da-4417-8664-de25b34d3def")]
		APND,

		[Category("IHE XDS Meta Data")]
		[XmlEnum("urn:uuid:60fd13eb-b8f6-4f11-8f28-9ee000184339")]
		RPLC,

		[Category("IHE XDS Meta Data")]
		[XmlEnum("urn:uuid:ede379e6-1147-4374-a943-8fcdcf1cd620")]
		XFRM,

		[Category("IHE XDS Meta Data")]
		[XmlEnum("urn:uuid:b76a27c7-af3c-4319-ba4c-b90c1dc45408")]
		XFRM_RPLC,

		[Category("IHE XDS Meta Data")]
		[XmlEnum("urn:uuid:8ea93462-ad05-4cdc-8e54-a8084f6aff94")]
		signs,

		[Category("IHE XDS Meta Data")]
		[XmlEnum("urn:uuid:10aa1a4b-715a-4120-bfd0-9760414112c8")]
		XDSDocumentEntryStub,

		[Description("Patient Demographic Query")]
		[Category("IHE Transactions")]
		[XmlEnum("ITI-47")]
		ITI47,

		[Description("PIX Query")]
		[Category("IHE Transactions")]
		[XmlEnum("ITI-45")]
		ITI45,

		[Category("IHE Transactions")]
		[XmlEnum("ITI-21")]
		ITI21,

		[Category("IHE Transactions")]
		[XmlEnum("ITI-18")]
		ITI18,

		[Category("IHE Transactions")]
		[Description("Patient Demographic Query")]
		[XmlEnum("ITI-9")]
		ITI9
	}
}