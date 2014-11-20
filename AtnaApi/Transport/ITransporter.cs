﻿/**
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
 * Date: 7-5-2012
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtnaApi.Model;
using System.Net;

namespace AtnaApi.Transport
{
    /// <summary>
    /// The message publisher that is to be used
    /// </summary>
    public interface ITransporter
    {
        /// <summary>
        /// Gets or sets the message format
        /// </summary>
        MessageFormatType MessageFormat { get; set; }

        /// <summary>
        /// Gets the endpoint that messages are sent to
        /// </summary>
        String EndPoint { get; set; }

        /// <summary>
        /// Send a message to the remote endpoint
        /// </summary>
        void SendMessage(AuditMessage am);
    }
}
