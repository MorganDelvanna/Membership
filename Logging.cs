#region Using directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

#endregion

namespace PFGA_Membership
{
    public class Logger
    {
        #region Private Members
        private bool _eventLogging;
        private bool _fileLogging;
        private bool _dbLogging;

        private bool _logFails;
        private bool _logSuccess;
        private bool _verbose = true;

        private string _logFilePath;

        private string _eventSource;
        private string _eventLogType = "Application";
        
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the Logging Class, initializes the options for where logging is done
        /// </summary>
        /// <param name="logToEventViewer">Enables or disables logging to the Windows Event Viewer</param>
        /// <param name="logToFile">Enables or disables logging to a file</param>
        /// <param name="logToDatabase">Enables or disables logging to the LearnFlex database</param>
        /// <param name="logFailures">Enables or disables logging items flagged as Errors</param>
        /// <param name="logSuccesses">Enables or disables logging items NOT flagged as Errors</param>
        /// <param name="logFilePath">The path of a file that errors will be written to if logging to a file is enabled</param>
        /// <param name="eventLogSource">The Event Log source that will be written to if logging to the Windows Event Viewer is enabled</param>
        public Logger(bool logToEventViewer, bool logToFile, bool logToDatabase, bool logFailures, bool logSuccesses, string logFilePath, string eventLogSource)
        {
            _eventLogging = logToEventViewer;
            _fileLogging = logToFile;
            _dbLogging = logToDatabase;
            _logFails = logFailures;
            _logSuccess = logSuccesses;
            _logFilePath = getPath(logFilePath);
            _eventSource = eventLogSource;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Enables or disables logging to the Windows Event Viewer
        /// </summary>
        public bool loggingToEventViewer
        {
            get {return _eventLogging;}
            set {_eventLogging = value;}
        }

        /// <summary>
        /// Enables or disables logging to a file
        /// </summary>
        public bool loggingToFile
        {
            get { return _fileLogging; }
            set { _fileLogging = value; }
        }

        /// <summary>
        /// Enables or disables logging to the LearnFlex database
        /// </summary>
        public bool loggingToDatabase
        {
            get { return _dbLogging; }
            set { _dbLogging = value; }
        }

        /// <summary>
        /// Enables or disables logging items flagged as Errors
        /// </summary>
        public bool loggingFailures
        {
            get { return _logFails; }
            set { _logFails = value; }
        }

        /// <summary>
        /// Enables or disables logging items NOT flagged as Errors
        /// </summary>
        public bool loggingSuccesses
        {
            get { return _logSuccess; }
            set { _logSuccess = value; }
        }

        /// <summary>
        /// The path of a file that errors will be written to if logging to a file is enabled
        /// </summary>
        public string loggingFilePath
        {
            get { return _logFilePath; }
            set { _logFilePath = getPath(value); }
        }

        /// <summary>
        /// The Event Log source that will be written to if logging to the Windows Event Viewer is enabled
        /// </summary>
        public string eventLoggingSource
        {
            get { return _eventSource; }
            set { _eventSource = value; }
        }

        /// <summary>
        /// The Event Log Type that our event will be written to, the default is "Application"
        /// </summary>
        public string eventLoggingType
        {
            get { return _eventLogType; }
            set { _eventLogType = value; }
        }

        public bool VerboseLogging
        {
            get { return _verbose; }
            set { _verbose = value; }
        }

        #endregion

        /// <summary>
        /// This is the function that writes our text to the even log
        /// </summary>
        /// <param name="text">Text to be inserted into the log</param>
        /// <param name="err">If an Exception object has been added to the message it will be writen to the Log</param>
        /// <param name="error">True if this is an Error, false if this is a Success</param>
        public void WriteToLog(string text, Exception Err, bool error)
        {
            int intError;

            if ((_logFails && error) || (_logSuccess && !error))
            {
                StringBuilder sMessage = new StringBuilder(text);

                try
                {
                    if ((Err != null) && _verbose)
                    {
                        sMessage.AppendLine();
                        sMessage.AppendLine("Source: " + Err.Source);
                        sMessage.AppendLine(Err.Message);
                        sMessage.AppendLine("Stack Trace: ");
                        sMessage.AppendLine(Err.StackTrace);
                    }
                    else if (Err != null)
                    {
                        sMessage.Append(Err.Message);
                    }

                }
                catch (Exception ex)
                {
                    // Try to log the error created by the logger
                    WriteToLog("Error creating Verbose log message", ex, true);
                }

                // File Logging is enabled
                if (_fileLogging && _logFilePath.Length > 0)
                {
                    try
                    {
                        // Check if the file exists
                        FileInfo fi = new FileInfo(_logFilePath);
                        if (!fi.Exists)
                        {
                            //Create a file to write to.
                            using (StreamWriter sw = File.CreateText(_logFilePath))
                            {
                                sw.Flush();
                                sw.Close();
                            }
                        }
                        using (StreamWriter sw = File.AppendText(_logFilePath))
                        {
                            sw.WriteLine(sMessage.ToString());
                            sw.Flush();
                            sw.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Disable File Logging
                        loggingToFile = false;
                        WriteToLog("Logging to a File has been disabled due to the following error: ", ex, true);
                    }
                }

               

                // Windows Event Logging is enabled
                if (_eventLogging)
                {
                    try
                    {
                        EventLogEntryType msgType;

                        // If the Event Source doesn't exist try to create it
                        if (!System.Diagnostics.EventLog.SourceExists(_eventSource))
                        {
                            System.Diagnostics.EventLog.CreateEventSource(_eventSource, _eventLogType);
                        }

                        // The EventLogEntryType depends if this is a success or failure type event
                        if (error)
                        {
                            msgType = EventLogEntryType.Error;
                        }
                        else
                        {
                            msgType = EventLogEntryType.Information;
                        }

                        // Write to the log
                        System.Diagnostics.EventLog.WriteEntry(_eventSource, sMessage.ToString(), msgType);
                    }
                    catch (Exception ex)
                    {
                        // Disable Event Viewer Logging
                        loggingToEventViewer = false;
                        WriteToLog("Logging to the Event Viewer has been disabled due to the following error: ", ex, true);
                    }
                }

                // Write error to Database is enabled
                if (_dbLogging)
                {
                    try
                    {
                        intError = SaveErrorDetailstoDB(text, Err);
                    }
                    catch (Exception ex)
                    {
                        // Disable Event Viewer Logging
                        loggingToDatabase = false;
                        WriteToLog("Logging to the Database has been disabled due to the following error: ", ex, true);
                    }
                }
            }
        }

        private int SaveErrorDetailstoDB(string Msg, Exception Err)
        {
            try
            {

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getPath(string Value)
        {
            // If this folder is relative to the app location
            if (Value.StartsWith("\\"))
            {
                Value = string.Concat(Environment.CurrentDirectory, Value);
            }

            // App is providing the Filename
            if (Value.EndsWith("\\"))
            {
                Assembly asm = Assembly.GetEntryAssembly();
                String Name = Path.GetFileNameWithoutExtension(asm.CodeBase);

                if (Name.Length > 0)
                {
                    Name.Replace(" ", string.Empty);
                }
                Value = string.Concat(Value, Name, DateTime.Today.ToString("yyyy_MM_dd"), ".txt");
            }

            // Filename contains a Date Pattern
            Value = Value.Replace("YYYY", DateTime.Today.Year.ToString());
            Value = Value.Replace("MM", DateTime.Today.Month.ToString());
            Value = Value.Replace("DD", DateTime.Today.Day.ToString());


            return Value;

        }
    }
}
