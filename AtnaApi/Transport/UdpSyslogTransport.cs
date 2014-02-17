/**
 * Copyright 2012-2013 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 23-8-2012
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using AtnaApi.Model;
using System.Diagnostics;
using System.Net.Sockets;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Xml;
using System.ComponentModel;

namespace AtnaApi.Transport
{
    /// <summary>
    /// Represents an ATNA Client
    /// </summary>
    [Description("BSD Syslog over UDP")]
    public class UdpSyslogTransport : ITransporter
    {
        // Represents the remote endpoint that is being connected
        private IPEndPoint m_remoteEndpoint;

        // Represents the syslog facility to use 
        public const int SYSLOG_FACILITY = 10;

        /// <summary>
        /// Default ctor
        /// </summary>
        public UdpSyslogTransport()
        {
        }

        /// <summary>
        /// Creates a new instance of the ATNA client
        /// </summary>
        public UdpSyslogTransport(IPEndPoint endpoint)
        {
            this.m_remoteEndpoint = endpoint;
        }

        /// <summary>
        /// Creates a new instance of the Syslog transport
        /// </summary>
        public UdpSyslogTransport(DnsEndPoint endpoint)
        {
            this.SetEndpointDns(endpoint);
        }
        /// <summary>
        /// Set the endpoint as a dns entry
        /// </summary>
        /// <param name="endpoint"></param>
        private void SetEndpointDns(DnsEndPoint endpoint)
        {
            var addresses = Dns.GetHostEntry(endpoint.Host);
            if (addresses.AddressList.Count() == 0)
                throw new InvalidOperationException("Cannot create a syslog transport as the hostname doesn't resolve to an IP Address");
            this.m_remoteEndpoint = new IPEndPoint(addresses.AddressList.First(), endpoint.Port);
        }

        /// <summary>
        /// Create the endpoint
        /// </summary>
        public String EndPoint
        {
            get { return m_remoteEndpoint.ToString(); }
            set 
            {
                string[] part = value.Split(':');
                IPAddress ipAdd = null;
                if (IPAddress.TryParse(part[0], out ipAdd))
                    this.m_remoteEndpoint = new IPEndPoint(ipAdd, Int32.Parse(part[1]));
                else
                    this.SetEndpointDns(new DnsEndPoint(part[0], Int32.Parse(part[1])));
            }
        }

        /// <summary>
        /// Send a message to the ATNA client
        /// </summary>
        public void SendMessage(AuditMessage am)
        {
            UdpClient udpClient = new UdpClient();
            try
            {
                udpClient.Connect(this.m_remoteEndpoint);
                StringBuilder syslogmessage = new StringBuilder();
                int severity = 7;
                switch (am.EventIdentification.EventOutcome)
                {
                    case OutcomeIndicator.Success:
                        severity = 5;
                        break;
                    case OutcomeIndicator.MinorFail:
                        severity = 4;
                        break;
                    default:
                        severity = 3;
                        break;
                }

                string domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                string hostName = Dns.GetHostName();
                string fqdn = "";
                if (!hostName.Contains(domainName))
                    fqdn = hostName + "." + domainName;
                else
                    fqdn = hostName;


                syslogmessage.AppendFormat("<{0}>1 {1:yyyy-MM-dd}T{1:HH:mm:ss.fff}Z {2} {3} {4} IHE+RFC-3881 - ",
                    (SYSLOG_FACILITY * 8) + severity, DateTime.UtcNow, fqdn, Process.GetCurrentProcess().ProcessName, Process.GetCurrentProcess().Id);
                syslogmessage.Append(AuditTransportUtil.CreateMessageBody(am));

                // Send the message
                // Create the dgram
                byte[] dgram = System.Text.Encoding.ASCII.GetBytes(syslogmessage.ToString());
                udpClient.Send(dgram, (int)dgram.Length);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
            }
            finally
            {
                udpClient.Close();
            }
        }


    }
}
