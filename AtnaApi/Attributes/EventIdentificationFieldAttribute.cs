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

namespace AtnaApi.Attributes
{
	/// <summary>
	/// Represents and identifies the fields that the particular data element belongs.
	/// </summary>
	public enum EventIdentificationFieldType
	{
		/// <summary>
		/// Represents the event date time.
		/// </summary>
		EventDateTime,

		/// <summary>
		/// Represents the event type.
		/// </summary>
		EventType,

		/// <summary>
		/// Represents the event identifier.
		/// </summary>
		EventId,

		/// <summary>
		/// Represents the event outcome.
		/// </summary>
		EventOutcome
	}

	/// <summary>
	/// Represents and identifies the event identification field to be populated.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class EventIdentificationFieldAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EventIdentificationFieldAttribute"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public EventIdentificationFieldAttribute(EventIdentificationFieldType type) : this(null, type)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EventIdentificationFieldAttribute"/> class.
		/// </summary>
		/// <param name="dataFormat">The data format.</param>
		/// <param name="type">The type.</param>
		public EventIdentificationFieldAttribute(string dataFormat, EventIdentificationFieldType type)
		{
			this.DataFormat = dataFormat;
			this.FieldType = type;
		}

		/// <summary>
		/// Represents the format of the target data.
		/// </summary>
		public string DataFormat { get; set; }

		/// <summary>
		/// Represents the type of field of the data.
		/// </summary>
		public EventIdentificationFieldType FieldType { get; set; }
	}
}