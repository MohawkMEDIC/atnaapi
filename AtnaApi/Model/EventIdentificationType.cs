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
using System.ComponentModel;

namespace AtnaApi.Model
{

    /// <summary>
    /// Event Identifier
    /// </summary>
    public enum EventIdentifierType
    {
        [Category("IHE")]
        [XmlEnum("IHE0001")]
        ProvisioningEvent,
        [Category("IHE")]
        [XmlEnum("IHE0002")]
        MedicationEvent,
        [Category("IHE")]
        [XmlEnum("IHE0003")]
        ResourceAssignment,
        [Category("IHE")]
        [XmlEnum("IHE0004")]
        CareEpisode,
        [Category("IHE")]
        [XmlEnum("IHE0005")]
        CareProtocol,
        [Category("DCM")]
        [XmlEnum("110111")]
        ProcedureRecord,
        [Category("DCM")]
        [XmlEnum("110112")]
        Query,
        [Category("DCM")]
        [XmlEnum("110114")]
        UserAuthentication,
        [Category("DCM")]
        [XmlEnum("110110")]
        PatientRecord,
        [Category("DCM")]
        [XmlEnum("110109")]
        OrderRecord,
        [Category("DCM")]
        [XmlEnum("110108")]
        NetowrkActivity,
        [Category("DCM")]
        [XmlEnum("110107")]
        Import,
        [Category("DCM")]
        [XmlEnum("110106")]
        Export,
        [Category("DCM")]
        [XmlEnum("110100")]
        ApplicationActivity,
        [Category("IHE")]
        [XmlEnum("IHE0006")]
        Disclosure,
        [Category("CDT")]
        [XmlEnum("CDT-100002")]
        CDTPatientSearchActivity,
        [Category("DCM")]
        [XmlEnum("110132")]
        UseOfRestrictedFunction,
        [Category("DCM")]
        [XmlEnum("110127")]
        EmergencyOverrideStarted,

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
        /// Represents the event outcome
        /// </summary>
        [XmlAttribute("EventOutcomeIndicator")]
        public OutcomeIndicator EventOutcome { get; set; }

        /// <summary>
        /// Represents the id of the event
        /// </summary>
        [XmlElement("EventID")]
        public CodeValue<EventIdentifierType> EventId { get; set; }

        /// <summary>
        /// Represents the event type
        /// </summary>
        [XmlElement("EventTypeCode")]
        public List<CodeValue<String>> EventType { get; set; }
    }
}
