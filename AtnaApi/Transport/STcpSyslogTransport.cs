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

using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace AtnaApi.Transport
{
	/// <summary>
	/// Represents a secure TCP transport.
	/// </summary>
	/// <seealso cref="AtnaApi.Transport.TcpSyslogTransport" />
	[Description("Secure TCP Transport")]
	public class STcpSyslogTransport : TcpSyslogTransport
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="STcpSyslogTransport"/> class.
		/// </summary>
		public STcpSyslogTransport()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="STcpSyslogTransport"/> class.
		/// </summary>
		/// <param name="endpoint">The endpoint.</param>
		public STcpSyslogTransport(IPEndPoint endpoint) : base(endpoint)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="STcpSyslogTransport"/> class.
		/// </summary>
		/// <param name="endpoint">The endpoint.</param>
		public STcpSyslogTransport(DnsEndPoint endpoint) : base(endpoint)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="STcpSyslogTransport"/> class.
		/// </summary>
		/// <param name="endpoint">The endpoint.</param>
		/// <param name="clientCert">The client cert.</param>
		/// <param name="serverChain">The server chain.</param>
		public STcpSyslogTransport(DnsEndPoint endpoint, X509Certificate2 clientCert, X509Certificate2 serverChain)
			: base(endpoint)
		{
			this.ClientCertificate = clientCert;
			this.ServerCertificate = serverChain;
		}

		/// <summary>
		/// Gets or sets the client certificate.
		/// </summary>
		/// <value>The client certificate.</value>
		public X509Certificate2 ClientCertificate { get; set; }

		/// <summary>
		/// Gets or sets the client certificate.
		/// </summary>
		/// <value>The server certificate.</value>
		public X509Certificate2 ServerCertificate { get; set; }


		/// <summary>
		/// Send a message.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="auditMessage">The audit message.</param>
		protected override void SendMessageInternal(System.IO.Stream stream, Model.AuditMessage auditMessage)
		{
			using (var wrapperStream = new SslStream(stream, false, this.RemoteCertificateValidation, null, EncryptionPolicy.RequireEncryption))
			{
				var clientCerts = new X509Certificate2Collection
				{
					this.ClientCertificate
				};

				wrapperStream.AuthenticateAsClient(this.EndPoint, clientCerts, System.Security.Authentication.SslProtocols.Tls, true);
				base.SendMessageInternal(wrapperStream, auditMessage);
			}
		}

		/// <summary>
		/// Remotes the certificate validation.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="certificate">The certificate.</param>
		/// <param name="chain">The chain.</param>
		/// <param name="sslPolicyErrors">The SSL policy errors.</param>
		/// <returns><c>true</c> if the certificate is valid, <c>false</c> otherwise.</returns>
		private bool RemoteCertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			// First Validate the chain
			if (certificate == null || chain == null || this.ServerCertificate == null)
			{
				return this.ServerCertificate == null;
			}

			var isValid = false;

			foreach (var cer in chain.ChainElements)
			{
				if (cer.Certificate.Thumbprint == this.ServerCertificate.Thumbprint)
					isValid = true;
			}

			if (!isValid)
			{
				Trace.TraceError("Certification authority from the supplied certificate doesn't match the expected thumbprint of the CA");
			}

			foreach (var stat in chain.ChainStatus)
			{
				Trace.TraceWarning("Certificate chain validation error: {0}", stat.StatusInformation);
			}

			isValid &= chain.ChainStatus.Length == 0;

			return isValid;
		}
	}
}