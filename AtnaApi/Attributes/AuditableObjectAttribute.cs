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

using AtnaApi.Model;
using System;
using System.IO;
using System.Xml.Serialization;

namespace AtnaApi.Attributes
{
	/// <summary>
	/// Represents and classifies the type of data that a tagged property contains.
	/// </summary>
	public enum ElementClassificationType
	{
		/// <summary>
		/// Represents a classification type indicating that the data contained is an identifier.
		/// </summary>
		Identifier,

		/// <summary>
		/// Represents a classification type indicating that the data contained is a query.
		/// </summary>
		Query,

		/// <summary>
		/// Represents a classification type indicating that the data contained is an object.
		/// </summary>
		Object
	}

	/// <summary>
	/// Represents an attribute which can be affixed to the auditable object.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class AuditableObjectAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AuditableObjectAttribute"/> class.
		/// </summary>
		public AuditableObjectAttribute(AuditableObjectType type, AuditableObjectRole role, AuditableObjectIdType idType)
		{
			this.DataFormat = "{0}";
			this.Type = type;
			this.Role = role;
			this.IDTypeCode = idType;
			this.Class = ElementClassificationType.Identifier;
		}

		/// <summary>
		/// Represents where to place the data from the object.
		/// </summary>
		/// <value>The class.</value>
		public ElementClassificationType Class { get; set; }

		/// <summary>
		/// Gets or sets the format of the data when placed in the audit.
		/// </summary>
		public String DataFormat { get; set; }

		/// <summary>
		/// Represents the type of identifier placed
		/// </summary>
		public AuditableObjectIdType IDTypeCode { get; set; }

		/// <summary>
		/// True if the property is the target of the lifecycle being described.
		/// </summary>
		public bool LifeCycleTarget { get; set; }

		/// <summary>
		/// Represents a participant object name or property from which to get this information.
		/// </summary>
		public string ParticipantObjectPropertyName { get; set; }

		/// <summary>
		/// Represents the role which the object plays.
		/// </summary>
		public AuditableObjectRole Role { get; set; }

		/// <summary>
		/// Represents the type of object of this property.
		/// </summary>
		public AuditableObjectType Type { get; set; }

		/// <summary>
		/// Converts the attribute data into an auditable object.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Returns the converted auditable object instance.</returns>
		public AuditableObject ToAuditableObject(object value)
		{
			var retVal = new AuditableObject()
			{
				IDTypeCode = new CodeValue<AuditableObjectIdType>(this.IDTypeCode),
				Role = this.Role,
				RoleSpecified = true,
				Type = this.Type,
				TypeSpecified = true
			};

			switch (this.Class)
			{
				case ElementClassificationType.Identifier:
					retVal.ObjectId = String.Format(this.DataFormat, value);
					retVal.ObjectSpecChoice = ObjectDataChoiceType.ParticipantObjectName;
					retVal.ObjectSpec = this.ParticipantObjectPropertyName;
					break;

				case ElementClassificationType.Query:
					retVal.ObjectSpecChoice = ObjectDataChoiceType.ParticipantObjectQuery;
					using (MemoryStream ms = new MemoryStream())
					{
						XmlSerializer xsz = new XmlSerializer(value.GetType());
						xsz.Serialize(ms, value);
						retVal.ObjectSpec = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
					}
					break;

				case ElementClassificationType.Object:
					retVal.ObjectSpecChoice = ObjectDataChoiceType.ParticipantObjectName;
					retVal.ObjectSpec = this.ParticipantObjectPropertyName;
					using (MemoryStream ms = new MemoryStream())
					{
						XmlSerializer xsz = new XmlSerializer(value.GetType());
						xsz.Serialize(ms, value);
						byte[] data = new byte[ms.Length];
						Array.Copy(ms.GetBuffer(), data, ms.Length);
						retVal.ObjectDetail.Add(new ObjectDetailType()
						{
							Type = "text/xml",
							Value = data
						});
					}
					break;
			}

			return retVal;
		}
	}
}