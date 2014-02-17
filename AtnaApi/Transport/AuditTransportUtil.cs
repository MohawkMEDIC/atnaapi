using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using AtnaApi.Model;

namespace AtnaApi.Transport
{
    /// <summary>
    /// A utility for message processing regardless of the transport being used
    /// </summary>
    public static class AuditTransportUtil
    {

       
        /// <summary>
        /// Create the message body
        /// </summary>
        public static string CreateMessageBody(AuditMessage am)
        {
            StringWriter sw = new StringWriter();
            XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = false });
            XmlSerializer xsz = new XmlSerializer(typeof(AuditMessage));
            xsz.Serialize(xw, am);
            xw.Close();
            return sw.ToString();
        }
    }
}
