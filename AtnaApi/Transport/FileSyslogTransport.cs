using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AtnaApi.Transport
{
    /// <summary>
    /// File syslog transport
    /// </summary>
    [Description("BSD Syslog File Appender")]
    public class FileSyslogTransport : ITransporter
    {


        /// <summary>
        /// Gets or sets the endpoint (file) to be appended
        /// </summary>
        public String EndPoint
        {
            get;
            set;
        }

        /// <summary>
        /// Sends the audit message to the file appender
        /// </summary>
        public void SendMessage(Model.AuditMessage am)
        {
            String message = AuditTransportUtil.CreateMessageBodyEx(am,this.MessageFormat);
            // Rotates the logs each day
            String fileName = Path.Combine(Path.GetDirectoryName(this.EndPoint), String.Format("{0}.{1}.{2}",
                Path.GetFileNameWithoutExtension(this.EndPoint),
                DateTime.Now.ToString("yyyyMMdd"),
                Path.GetExtension(this.EndPoint)));

            try
            {
                using (TextWriter fs = File.AppendText(fileName))
                    fs.WriteLine("{0:MMM dd HH:mm:ss} {1} {2}({3}): {4}", DateTime.Now, Environment.UserName, Process.GetCurrentProcess().ProcessName, Process.GetCurrentProcess().Id, message);
            }
            catch (Exception e)
            {
                Trace.TraceError("Could not write audit to file {0}. Error was {1}.\r\nMessage:\r\n{2}", fileName, e, message);
            }
        }

        /// <summary>
        /// Gets or sets the message format
        /// </summary>
        public MessageFormatType MessageFormat { get; set; }
    }
}
