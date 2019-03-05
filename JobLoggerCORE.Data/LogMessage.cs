using System;
using System.Collections.Generic;

namespace JobLoggerCORE.Data
{
    public partial class LogMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int IdType { get; set; }

        public virtual Type IdTypeNavigation { get; set; }
    }
}
