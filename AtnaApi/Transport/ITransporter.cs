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

namespace AtnaApi.Transport
{
	/// <summary>
	/// Represents a transporter.
	/// </summary>
	public interface ITransporter
	{
		/// <summary>
		/// Gets or sets the end point.
		/// </summary>
		/// <value>The end point.</value>
		string EndPoint { get; set; }

		/// <summary>
		/// Gets or sets the message format.
		/// </summary>
		/// <value>The message format.</value>
		MessageFormatType MessageFormat { get; set; }

		/// <summary>
		/// Sends the message.
		/// </summary>
		/// <param name="auditMessage">The audit message.</param>
		void SendMessage(AuditMessage auditMessage);
	}
}