using System;
using System.Collections.Generic;

namespace JobLoggerCORE.Data
{
    public partial class LogConfiguration
    {
        public int Id { get; set; }
        public bool LogToFile { get; set; }
        public bool LogToDataBase { get; set; }
        public bool LogToConsole { get; set; }
        public bool LogError { get; set; }
        public bool LogWarning { get; set; }
        public bool LogMessage { get; set; }
    }
}
