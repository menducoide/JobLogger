//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobLogger.Data
{
    using System;
    using System.Collections.Generic;
    
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
