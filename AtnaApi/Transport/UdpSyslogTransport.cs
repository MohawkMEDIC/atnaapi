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

using AtnaApi.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AtnaApi.Transport
{
	/// <summary>
	/// Represents a UDP syslog transport.
	/// </summary>
	/// <seealso cref="AtnaApi.Transport.ITransporter" />
	[Description("BSD Syslog over UDP")]
	public class UdpSyslogTransport : ITransporter
	{
		/// <summary>
		/// Represents the syslog facility.
		/// </summary>
		public const int SYSLOG_FACILITY = 10;

		/// <summary>
		/// The remote endpoint.
		/// </summary>
		private IPEndPoint remoteEndpoint;

		/// <summary>
		/// Initializes a new instance of the <see cref="UdpSyslogTransport"/> class.
		/// </summary>
		public UdpSyslogTransport()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UdpSyslogTransport"/> class.
		/// </summary>
		/// <param name="endpoint">The endpoint.</param>
		public UdpSyslogTransport(IPEndPoint endpoint)
		{
			this.remoteEndpoint = endpoint;
		}

		/// <summary>
		/// Creates a new instance of the Syslog transport
		/// </summary>
		public UdpSyslogTransport(DnsEndPoint endpoint)
		{
			this.SetEndpointDns(endpoint);
		}

		/// <summary>
		/// Gets or sets the end point.
		/// </summary>
		/// <value>The end point.</value>
		public string EndPoint
		{
			get => remoteEndpoint.ToString();
			set
			{
				var part = value.Split(':');
				if (IPAddress.TryParse(part[0], out var ipAddress))
				{
					this.remoteEndpoint = new IPEndPoint(ipAddress, int.Parse(part[1]));
				}
				else
				{
					this.SetEndpointDns(new DnsEndPoint(part[0], int.Parse(part[1])));
				}
			}
		}

		/// <summary>
		/// Gets or sets the message format
		/// </summary>
		/// <value>The message format.</value>
		public MessageFormatType MessageFormat { get; set; }

		/// <summary>
		/// Sends the message.
		/// </summary>
		/// <param name="am">The am.</param>
		public void SendMessage(AuditMessage am)
		{
			var udpClient = new UdpClient();

			try
			{
				udpClient.Connect(this.remoteEndpoint);
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

				String iheFormat = this.MessageFormat == MessageFormatType.DICOM ? "DICOM+RFC3881" : "IHE+RFC3881";

				syslogmessage.AppendFormat("<{0}>1 {1:yyyy-MM-dd}T{1:HH:mm:ss.fff}Z {2} {3} {4} {5} - ",
					(SYSLOG_FACILITY * 8) + severity, DateTime.UtcNow, fqdn, Process.GetCurrentProcess().ProcessName, Process.GetCurrentProcess().Id, iheFormat);
				syslogmessage.Append(AuditTransportUtil.CreateMessageBodyEx(am, this.MessageFormat));

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

		/// <summary>
		/// Set the endpoint as a dns entry
		/// </summary>
		/// <param name="endpoint">The endpoint.</param>
		/// <exception cref="System.InvalidOperationException">Cannot create a syslog transport as the hostname doesn't resolve to an IP Address</exception>
		private void SetEndpointDns(DnsEndPoint endpoint)
		{
			var addresses = Dns.GetHostEntry(endpoint.Host);
			if (!addresses.AddressList.Any())
			{
				throw new InvalidOperationException("Cannot create a syslog transport as the hostname doesn't resolve to an IP Address");
			}

			this.remoteEndpoint = new IPEndPoint(addresses.AddressList.First(), endpoint.Port);
		}
	}
}