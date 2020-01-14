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

using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AtnaApi.Model;

namespace AtnaApi.Transport
{
	/// <summary>
	/// Represents a utility for message processing regardless of the transport being used.
	/// </summary>
	public static class AuditTransportUtil
	{
		/// <summary>
		/// Creates the message body.
		/// </summary>
		/// <param name="auditMessage">The audit message.</param>
		/// <returns>Returns the created audit message body.</returns>
		public static string CreateMessageBody(AuditMessage auditMessage)
		{
			return CreateMessageBodyEx(auditMessage, MessageFormatType.RFC3881);
		}

		/// <summary>
		/// Creates the message body in the specified format
		/// </summary>
		/// <param name="auditMessage">The am.</param>
		/// <param name="format">The format.</param>
		/// <returns>Returns the created audit message body.</returns>
		public static string CreateMessageBodyEx(AuditMessage auditMessage, MessageFormatType format)
		{
			using (var stringWriter = new StringWriter())
			using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false }))
			{
				var serializer = new XmlSerializer(typeof(AuditMessage));

				serializer.Serialize(xmlWriter, auditMessage);

				// Is this DICOM?
				return format == MessageFormatType.DICOM ? ConvertAuditToDICOM(stringWriter.ToString()) : stringWriter.ToString();
			}
		}

        /// <summary>
        /// Convert audit string to dicom
        /// </summary>
        public static string ConvertAuditToDICOM(string rfc3881Audit)
        {
            XmlDocument xdocument = new XmlDocument();
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(rfc3881Audit)))
                xdocument.Load(ms);
            foreach (XmlAttribute attr in xdocument.SelectNodes("//*/@code"))
            {
                XmlAttribute newAttr = xdocument.CreateAttribute("csd-code");
                newAttr.Value = attr.Value;
                attr.OwnerElement.Attributes.Append(newAttr);
                attr.OwnerElement.Attributes.Remove(attr);
            }
            foreach (XmlAttribute attr in xdocument.SelectNodes("//*/@displayName"))
            {
                if (attr.OwnerElement.Attributes["originalText"] != null)
                    continue;
                XmlAttribute newAttr = xdocument.CreateAttribute("originalText");
                newAttr.Value = attr.InnerText;
                attr.OwnerElement.Attributes.Append(newAttr);
                attr.OwnerElement.Attributes.Remove(attr);
            }

			// Move the audit source type code
			XmlElement auditSourceType = xdocument.SelectSingleNode("//*[local-name() = 'AuditSourceIdentification']/*[local-name() = 'AuditSourceTypeCode']") as XmlElement;
			if (auditSourceType != null)
			{
				if (auditSourceType.Attributes["csd-code"] != null)
					auditSourceType.ParentNode.Attributes.Append(xdocument.CreateAttribute("code")).Value = auditSourceType.Attributes["csd-code"].Value;
				if (auditSourceType.Attributes["originalText"] != null)
					auditSourceType.ParentNode.Attributes.Append(xdocument.CreateAttribute("originalText")).Value = auditSourceType.Attributes["originalText"].Value;
				if (auditSourceType.Attributes["codeSystemName"] != null)
					auditSourceType.ParentNode.Attributes.Append(xdocument.CreateAttribute("codeSystemName")).Value = auditSourceType.Attributes["codeSystemName"].Value;
				auditSourceType.ParentNode.RemoveChild(auditSourceType);
			}

			return xdocument.OuterXml;
		}

        /// <summary>
        /// Convert audit string to dicom
        /// </summary>
        public static string ConvertAuditToRFC3881(string rfc3881Audit)
        {
            
            XmlDocument document = new XmlDocument();

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(rfc3881Audit)))
                document.Load(ms) ;
            foreach (XmlAttribute attr in document.SelectNodes("//*/@csd-code"))
            {
                XmlAttribute newAttr = document.CreateAttribute("code");
                newAttr.Value = attr.Value;
                attr.OwnerElement.Attributes.Append(newAttr);
                attr.OwnerElement.Attributes.Remove(attr);
            }
            foreach (XmlAttribute attr in document.SelectNodes("//*/@originalText"))
            {
                XmlAttribute newAttr = document.CreateAttribute("displayName");
                newAttr.Value = attr.InnerText;
                attr.OwnerElement.Attributes.Append(newAttr);
                attr.OwnerElement.Attributes.Remove(attr);
            }

			// Move the audit source type code
			if (document.SelectSingleNode("//*[local-name() = 'AuditSourceIdentification']") is XmlElement auditSourceType)
			{
				XmlElement subCode = auditSourceType.AppendChild(document.CreateElement("AuditSourceTypeCode")) as XmlElement;
				if (auditSourceType.Attributes["code"] != null)
					subCode.Attributes.Append(document.CreateAttribute("code")).Value = auditSourceType.Attributes["code"].Value;
				if (auditSourceType.Attributes["originalText"] != null)
					subCode.Attributes.Append(document.CreateAttribute("displayName")).Value = auditSourceType.Attributes["originalText"].Value;
				if (auditSourceType.Attributes["codeSystemName"] != null)
					subCode.Attributes.Append(document.CreateAttribute("codeSystemName")).Value = auditSourceType.Attributes["codeSystemName"].Value;
			}

			return document.OuterXml;
		}
	}
}
