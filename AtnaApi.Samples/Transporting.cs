using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AtnaApi.Model;
using AtnaApi.Transport;

namespace AtnaApi.Samples
{
    /// <summary>
    /// Illustrates the use of the AtnaApi to send an RFC3881 audit over
    /// UDP syslog, TCP syslog and secure TCP
    /// </summary>
    /// <remarks>
    /// The purpose of this sample is not to illustrate the creation of an audit
    /// message which is valid according to any one IHE transaction, the purpose
    /// of this sample is the illustrate the sending of an audit to a local 
    /// UDP port.
    /// </remarks>
    public class Transporting
    {
        /// <summary>
        /// Run the sample with Udp Transport
        /// </summary>
        public void UdpTransport()
        {

            AuditMessage message = new AuditMessage(
                DateTime.Now, // The timestamp of the audit
                ActionType.Execute, // The action that occurred
                OutcomeIndicator.Success, // The outcome
                EventIdentifierType.ApplicationActivity, // The identification of the event that occurred
                new CodeValue<string>("ITI-X1"));

            UdpSyslogTransport transport = new UdpSyslogTransport(new DnsEndPoint("localhost", 514));
            transport.MessageFormat = MessageFormatType.RFC3881;
            transport.SendMessage(message);

        }

        /// <summary>
        /// Run the sample with Tcp Transport
        /// </summary>
        public void TcpTransport()
        {

            AuditMessage message = new AuditMessage(
                DateTime.Now, // The timestamp of the audit
                ActionType.Execute, // The action that occurred
                OutcomeIndicator.Success, // The outcome
                EventIdentifierType.ApplicationActivity, // The identification of the event that occurred
                new CodeValue<string>("ITI-X1"));

            try
            {
                TcpSyslogTransport transport = new TcpSyslogTransport(new DnsEndPoint("localhost", 514));
                transport.MessageFormat = MessageFormatType.RFC3881;
                transport.SendMessage(message);
            }
            catch(Exception e)
            {
                Trace.TraceError(e.ToString());
            }
        }

        /// <summary>
        /// Run the sample with secured Tcp Transport
        /// </summary>
        public void SecureTcpTransport()
        {

            AuditMessage message = new AuditMessage(
                DateTime.Now, // The timestamp of the audit
                ActionType.Execute, // The action that occurred
                OutcomeIndicator.Success, // The outcome
                EventIdentifierType.ApplicationActivity, // The identification of the event that occurred
                new CodeValue<string>("ITI-X1"));

            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            try
            {
                STcpSyslogTransport transport = new STcpSyslogTransport(new DnsEndPoint("localhost", 514));

                // Select a certificate 
                store.Open(OpenFlags.ReadOnly);
                var selected = X509Certificate2UI.SelectFromCollection(store.Certificates, "Select Certificate", "Select a client certificate for this endpoint", X509SelectionFlag.SingleSelection);

                transport.ClientCertificate = selected[0];

                transport.MessageFormat = MessageFormatType.RFC3881;
                transport.SendMessage(message);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
            }
            finally
            {
                store.Close();
            }
        }

    }
}
