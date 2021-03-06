﻿/* 
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
            return CreateMessageBodyEx(am, MessageFormatType.RFC3881);
        }

        /// <summary>
        /// Creates the message body in the specified format
        /// </summary>
        /// <returns></returns>
        public static string CreateMessageBodyEx(AuditMessage am, MessageFormatType format)
        {
            StringWriter sw = new StringWriter();
            
            XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = false });
            XmlSerializer xsz = new XmlSerializer(typeof(AuditMessage));
            xsz.Serialize(xw, am);
            xw.Close();

            // Is this DICOM?
            if (format == MessageFormatType.DICOM)
                return ConvertAuditToDICOM(sw.ToString());
            else
                return sw.ToString();
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
            
            XmlDocument xdocument = new XmlDocument();

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(rfc3881Audit)))
                xdocument.Load(ms) ;
            foreach (XmlAttribute attr in xdocument.SelectNodes("//*/@csd-code"))
            {
                XmlAttribute newAttr = xdocument.CreateAttribute("code");
                newAttr.Value = attr.Value;
                attr.OwnerElement.Attributes.Append(newAttr);
                attr.OwnerElement.Attributes.Remove(attr);
            }
            foreach (XmlAttribute attr in xdocument.SelectNodes("//*/@originalText"))
            {
                XmlAttribute newAttr = xdocument.CreateAttribute("displayName");
                newAttr.Value = attr.InnerText;
                attr.OwnerElement.Attributes.Append(newAttr);
                attr.OwnerElement.Attributes.Remove(attr);
            }

            // Move the audit source type code
            XmlElement auditSourceType = xdocument.SelectSingleNode("//*[local-name() = 'AuditSourceIdentification']") as XmlElement;
            if (auditSourceType != null)
            {
                XmlElement subCode = auditSourceType.AppendChild(xdocument.CreateElement("AuditSourceTypeCode")) as XmlElement;
                if(auditSourceType.Attributes["code"] != null)
                    subCode.Attributes.Append(xdocument.CreateAttribute("code")).Value = auditSourceType.Attributes["code"].Value;
                if (auditSourceType.Attributes["originalText"] != null)
                    subCode.Attributes.Append(xdocument.CreateAttribute("displayName")).Value = auditSourceType.Attributes["originalText"].Value;
                if(auditSourceType.Attributes["codeSystemName"] != null)
                    subCode.Attributes.Append(xdocument.CreateAttribute("codeSystemName")).Value = auditSourceType.Attributes["codeSystemName"].Value;

            }

            return xdocument.OuterXml;
        }
    }
}
