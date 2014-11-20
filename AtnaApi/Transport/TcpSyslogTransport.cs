using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ComponentModel;
using AtnaApi.Model;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO;

namespace AtnaApi.Transport
{
    /// <summary>
    /// A syslog publisher which leverages a tcp connection
    /// </summary>
    [Description("BSD Syslog over Unsecured TCP")]
    public class TcpSyslogTransport : ITransporter
    {
        // Represents the remote endpoint that is being connected
        private IPEndPoint m_remoteEndpoint;

        // Represents the syslog facility to use 
        public const int SYSLOG_FACILITY = 10;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TcpSyslogTransport()
        {
        }

        /// <summary>
        /// Creates a new instance of the ATNA client
        /// </summary>
        public TcpSyslogTransport(IPEndPoint endpoint)
        {
            this.m_remoteEndpoint = endpoint;
        }

        /// <summary>
        /// Creates a new instance of the Syslog transport
        /// </summary>
        public TcpSyslogTransport(DnsEndPoint endpoint)
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
        public String EndPoint { get { return m_remoteEndpoint.ToString(); }
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
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(this.m_remoteEndpoint);
                this.SendMessageInternal(tcpClient.GetStream(), am);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
            }
            finally
            {
                tcpClient.Close();
            }
        }

        /// <summary>
        /// Send a message
        /// </summary>
        protected virtual void SendMessageInternal(Stream stream, AuditMessage am)
        {
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
                
            String iheFormat = this.MessageFormat == MessageFormatType.DICOM ? "DICOM" : "RFC-3881";

            syslogmessage.AppendFormat("<{0}>1 {1:yyyy-MM-dd}T{1:HH:mm:ss.fff}Z {2} {3} {4} IHE+{5} - ",
                (SYSLOG_FACILITY * 8) + severity, DateTime.UtcNow, fqdn, Process.GetCurrentProcess().ProcessName, Process.GetCurrentProcess().Id, iheFormat);
            syslogmessage.Append(AuditTransportUtil.CreateMessageBodyEx(am, this.MessageFormat));

            // Send the message
            // Create the dgram
            String messageBody = syslogmessage.ToString();

#if DEBUG
            Trace.TraceInformation("Sending AUDIT message {0}", messageBody);
#endif
            using (TextWriter tw = new StreamWriter(stream, new System.Text.ASCIIEncoding()))
            {
                tw.Write(tw.Encoding.GetByteCount(messageBody));
                tw.Write(" ");
                tw.Write(messageBody);
            }
        }

        /// <summary>
        /// Gets or sets the message format
        /// </summary>
        public MessageFormatType MessageFormat { get; set; }
    }
}
