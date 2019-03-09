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
using System.Xml.Serialization;

namespace AtnaApi.Model
{
	/// <summary>
	/// Represents data related to an audit event.
	/// </summary>
	[XmlRoot("AuditMessage")]
	public class AuditMessage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AuditMessage"/> class.
		/// </summary>
		public AuditMessage()
		{
			this.EventIdentification = new EventIdentificationType();
			this.Actors = new List<AuditActorData>();
			this.SourceIdentification = new List<AuditSourceIdentificationType>();
			this.AuditableObjects = new List<AuditableObject>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuditMessage"/> class.
		/// </summary>
		/// <param name="timeStamp">The time stamp.</param>
		/// <param name="actionCode">The action code.</param>
		/// <param name="outcome">The outcome.</param>
		/// <param name="eventIdentifier">The event identifier.</param>
		/// <param name="eventTypeCode">The event type code.</param>
		public AuditMessage(DateTime timeStamp, ActionType actionCode, EventIdentifierType eventIdentifier, CodeValue<string> eventTypeCode) : this()
		{
			this.EventIdentification = new EventIdentificationType
			{
				EventDateTime = DateTime.Now,
				ActionCode = actionCode,
				EventId = new CodeValue<EventIdentifierType>(eventIdentifier)
			};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuditMessage"/> class.
		/// </summary>
		/// <param name="timeStamp">The time stamp.</param>
		/// <param name="actionCode">The action code.</param>
		/// <param name="outcome">The outcome.</param>
		/// <param name="eventIdentifier">The event identifier.</param>
		/// <param name="eventTypeCode">The event type code.</param>
		public AuditMessage(DateTime timeStamp, ActionType actionCode, OutcomeIndicator outcome, EventIdentifierType eventIdentifier, CodeValue<string> eventTypeCode) : this(timeStamp, actionCode, eventIdentifier, eventTypeCode)
		{
			this.EventIdentification.EventOutcome = outcome;
			this.EventIdentification.EventType.Add(eventTypeCode);
		}

		/// <summary>
		/// Represents the actors within the audit event
		/// </summary>
		[XmlElement("ActiveParticipant")]
		public List<AuditActorData> Actors { get; set; }

		/// <summary>
		/// Represents other objects of interest
		/// </summary>
		[XmlElement("ParticipantObjectIdentification")]
		public List<AuditableObject> AuditableObjects { get; set; }

		/// <summary>
		/// Represents the type of event
		/// </summary>
		[XmlElement("EventIdentification")]
		public EventIdentificationType EventIdentification { get; set; }

		/// <summary>
		/// Identifies the source of the audit
		/// </summary>
		[XmlElement("AuditSourceIdentification")]
		public List<AuditSourceIdentificationType> SourceIdentification { get; set; }
	}
}