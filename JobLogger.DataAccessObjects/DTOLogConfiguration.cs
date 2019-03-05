using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLogger.DataAccessObjects
{
  public  class DTOLogConfiguration
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
