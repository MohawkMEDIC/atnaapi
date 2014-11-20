using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AtnaApi.Transport
{
    /// <summary>
    /// Message format type
    /// </summary>
    public enum MessageFormatType
    {
        [XmlEnum("RFC-3881")]
        RFC3881,
        [XmlEnum("DICOM")]
        DICOM
    }
}
