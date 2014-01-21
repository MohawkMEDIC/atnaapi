using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AtnaApi.Model
{
    /// <summary>
    /// Object detail type
    /// </summary>
    public class ObjectDetailType
    {

        /// <summary>
        /// Gets or sets the type of the detail
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the value of the detail
        /// </summary>
        [XmlAttribute("value")]
        public byte[] Value { get; set; }

    }
}
