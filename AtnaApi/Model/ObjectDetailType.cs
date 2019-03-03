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

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace AtnaApi.Model
{
    /// <summary>
    /// Represents an object detail type.
    /// </summary>
    public class ObjectDetailType
    {

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectDetailType"/> class.
		/// </summary>
		public ObjectDetailType()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectDetailType"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public ObjectDetailType(string type)
		{
			this.Type = type;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectDetailType"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="value">The value.</param>
		public ObjectDetailType(string type, byte[] value) : this(type)
		{
			this.Value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectDetailType"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="value">The value.</param>
		public ObjectDetailType(string type, object value) : this(type)
		{
			var formatter = new BinaryFormatter();

			using (var memoryStream = new MemoryStream())
			{
				formatter.Serialize(memoryStream, value);

				this.Value = memoryStream.ToArray();
			}
		}


		/// <summary>
		/// Gets or sets the type of the detail.
		/// </summary>
		[XmlAttribute("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the value of the detail.
        /// </summary>
        [XmlAttribute("value")]
        public byte[] Value { get; set; }

    }
}
