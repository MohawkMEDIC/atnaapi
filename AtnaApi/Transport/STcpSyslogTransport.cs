/* 
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
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using System.Net;

namespace AtnaApi.Transport
{
    /// <summary>
    /// Secure TCP transport
    /// </summary>
    [Description("Secure TCP Transport")]
    public class STcpSyslogTransport : TcpSyslogTransport
    {

        /// <summary>
        /// Default ctor
        /// </summary>
        public STcpSyslogTransport()
        {
        }

        /// <summary>
        /// Creates a new instance of the ATNA client
        /// </summary>
        public STcpSyslogTransport(IPEndPoint endpoint) : base(endpoint)
        {
            
        }

        /// <summary>
        /// Creates a new instance of the Syslog transport
        /// </summary>
        public STcpSyslogTransport(DnsEndPoint endpoint) : base(endpoint)
        {
        }

        /// <summary>
        /// Creates a new instance of the Syslog transport
        /// </summary>
        public STcpSyslogTransport(DnsEndPoint endpoint, X509Certificate2 clientCert, X509Certificate2 serverChain)
            : base(endpoint)
        {
            this.ClientCertificate = clientCert;
            this.ServerCertificate = serverChain;
        }

        /// <summary>
        /// Gets or sets the client certificate
        /// </summary>
        public X509Certificate2 ClientCertificate { get; set; }
        /// <summary>
        /// Gets or sets the client certificate
        /// </summary>
        public X509Certificate2 ServerCertificate { get; set; }

        /// <summary>
        /// Validation for certificates
        /// </summary>
        private bool RemoteCertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {

            // First Validate the chain
            if (certificate == null || chain == null || this.ServerCertificate == null)
                return this.ServerCertificate == null;
            else
            {

                bool isValid = false;
                foreach (var cer in chain.ChainElements)
                    if (cer.Certificate.Thumbprint == this.ServerCertificate.Thumbprint)
                        isValid = true;
                if (!isValid)
                    Trace.TraceError("Certification authority from the supplied certificate doesn't match the expected thumbprint of the CA");
                foreach (var stat in chain.ChainStatus)
                    Trace.TraceWarning("Certificate chain validation error: {0}", stat.StatusInformation);
                isValid &= chain.ChainStatus.Length == 0;
                return isValid;
            }
        }


        /// <summary>
        /// Send the message (wraps in an SSL Stream)
        /// </summary>
        protected override void SendMessageInternal(System.IO.Stream stream, Model.AuditMessage am)
        {
            using (SslStream wrapperStream = new SslStream(stream, false, this.RemoteCertificateValidation, null, EncryptionPolicy.RequireEncryption))
            {
                X509Certificate2Collection clientCerts = new X509Certificate2Collection() { this.ClientCertificate };
                wrapperStream.AuthenticateAsClient(this.EndPoint.ToString(), clientCerts, System.Security.Authentication.SslProtocols.Tls, true);
                base.SendMessageInternal(wrapperStream, am);
            }
        }

    }
}
