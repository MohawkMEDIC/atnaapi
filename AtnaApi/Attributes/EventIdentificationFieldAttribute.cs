using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtnaApi.Attributes
{
    /// <summary>
    /// Identifies the fields that the particular data element belongs
    /// </summary>
    public enum EventIdentificationFieldType
    {
        EventDateTime,
        EventType,
        EventId,
        EventOutcome
    }

    /// <summary>
    /// Identifies the event identification field to be populated
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EventIdentificationFieldAttribute : Attribute
    {
        /// <summary>
        /// Event field type
        /// </summary>
        public EventIdentificationFieldAttribute(EventIdentificationFieldType type)
        {
            this.FieldType = type;
        }

        /// <summary>
        /// Identifies the format of the target data
        /// </summary>
        public String DataFormat { get; set; }

        /// <summary>
        /// Identifies the type of field the data represents
        /// </summary>
        public EventIdentificationFieldType FieldType { get; set; }
    }
}
