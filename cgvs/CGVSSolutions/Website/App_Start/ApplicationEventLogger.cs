using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Website
{
    public class ApplicationEventLogger
    {
        private static void Log(string LogMessage, string logName)
        {
            using (EventLog eventLog = new EventLog(logName))
            {
                eventLog.Source = logName;
                eventLog.WriteEntry(LogMessage, EventLogEntryType.Information, 101, 1);
            }
        }
        private static string CheckMessage(string RawMessage)
        {
            return Regex.Replace(RawMessage, @"[^0-9a-zA-Z:,]+", "_");
        }
        public static void LogApplication(string logMessage)
        {
            Log(CheckMessage(logMessage), "Application");
        }
    }
}