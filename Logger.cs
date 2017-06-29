using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace PFGA_Membership
{
    public class ErrorLogger
    {
        bool logToEvent = true;
        bool logToFile = false;
        bool logToDB = false;
        bool logFailed = false;
        bool logSuccess = false;
        string logPath = string.Empty;
        string logEventName = "PFGA Membership";
        bool logVerbose = false;

        public ErrorLogger()
        {
            
            if (ConfigurationManager.AppSettings["ENABLE_EVENT_LOG"] != null)
            {
                logToEvent = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ENABLE_EVENT_LOG"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["ENABLE_EVENT_LOG"]));
            }
            if (ConfigurationManager.AppSettings["ENABLE_LOG_FILE"] != null)
            {
                logToFile = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ENABLE_LOG_FILE"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["ENABLE_LOG_FILE"]));
            }
            if (ConfigurationManager.AppSettings["ENABLE_LOG_TABLE"] != null)
            {
                logToDB = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ENABLE_LOG_TABLE"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["ENABLE_LOG_TABLE"]));
            }
            if (ConfigurationManager.AppSettings["LOG_FAILED_EVENT"] != null)
            {
                logFailed = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["LOG_FAILED_EVENT"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["LOG_FAILED_EVENT"]));
            }
            if (ConfigurationManager.AppSettings["LOG_SUCCESSFUL_EVENT"] != null)
            {
                logSuccess = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["LOG_SUCCESSFUL_EVENT"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["LOG_SUCCESSFUL_EVENT"]));
            }
            if (ConfigurationManager.AppSettings["ERROR_LOG_PATH"] != null)
            {
                logPath = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ERROR_LOG_PATH"]) ? string.Empty : ConfigurationManager.AppSettings["ERROR_LOG_PATH"]);
            }
            if (ConfigurationManager.AppSettings["ERROR_LOG_SOURCE"] != null)
            {
                logEventName = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ERROR_LOG_SOURCE"]) ? string.Empty : ConfigurationManager.AppSettings["ERROR_LOG_SOURCE"].ToString());
            }
            if (ConfigurationManager.AppSettings["VERBOSE_LOGGING"] != null)
            {
                logVerbose = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["VERBOSE_LOGGING"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["VERBOSE_LOGGING"]));
            }
        }

        public void WriteLog(string Message, Exception Err, bool Error)
        {
            PFGA_Membership.Logger EventLog = new PFGA_Membership.Logger(logToEvent, logToFile, logToDB, logFailed, logSuccess, logPath, logEventName);
            EventLog.VerboseLogging = logVerbose;

            Message = string.Concat(DateTime.Now.ToString("u"), " -> ", Message);
            EventLog.WriteToLog(Message, Err, Error);
        }


        public static void Log(string Message, Exception Err, bool Error)
        {
            #region Logging Configuration
            // Get Logging Configuration
            bool logToEvent = true;
            bool logToFile = false;
            bool logToDB = false;
            bool logFailed = false;
            bool logSuccess = false;
            string logPath = string.Empty;
            string logEventName = "Learnflex Reporting Service";
            bool logVerbose = false;

            if (ConfigurationManager.AppSettings["ENABLE_EVENT_LOG"] != null)
            {
                logToEvent = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ENABLE_EVENT_LOG"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["ENABLE_EVENT_LOG"]));
            }
            if (ConfigurationManager.AppSettings["ENABLE_LOG_FILE"] != null)
            {
                logToFile = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ENABLE_LOG_FILE"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["ENABLE_LOG_FILE"]));
            }
            if (ConfigurationManager.AppSettings["ENABLE_LOG_TABLE"] != null)
            {
                logToDB = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ENABLE_LOG_TABLE"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["ENABLE_LOG_TABLE"]));
            }
            if (ConfigurationManager.AppSettings["LOG_FAILED_EVENT"] != null)
            {
                logFailed = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["LOG_FAILED_EVENT"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["LOG_FAILED_EVENT"]));
            }
            if (ConfigurationManager.AppSettings["LOG_SUCCESSFUL_EVENT"] != null)
            {
                logSuccess = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["LOG_SUCCESSFUL_EVENT"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["LOG_SUCCESSFUL_EVENT"]));
            }
            if (ConfigurationManager.AppSettings["ERROR_LOG_PATH"] != null)
            {
                logPath = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ERROR_LOG_PATH"]) ? string.Empty : ConfigurationManager.AppSettings["ERROR_LOG_PATH"]);
            }
            if (ConfigurationManager.AppSettings["ERROR_LOG_SOURCE"] != null)
            {
                logEventName = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ERROR_LOG_SOURCE"]) ? string.Empty : ConfigurationManager.AppSettings["ERROR_LOG_SOURCE"].ToString());
            }
            if (ConfigurationManager.AppSettings["VERBOSE_LOGGING"] != null)
            {
                logVerbose = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["VERBOSE_LOGGING"]) ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["VERBOSE_LOGGING"]));
            }
            #endregion
            // Create an instance of the Error Logger
            PFGA_Membership.Logger EventLog = new PFGA_Membership.Logger(logToEvent, logToFile, logToDB, logFailed, logSuccess, logPath, logEventName);
            EventLog.VerboseLogging = logVerbose;

            Message = string.Concat(DateTime.Now.ToString("u"), " -> ", Message);
            EventLog.WriteToLog(Message, Err, Error);
            if (Error)
            {
                MessageBox.Show("An Error has occurred, check the error log", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
