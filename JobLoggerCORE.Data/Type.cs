using System;
using System.Collections.Generic;

namespace JobLoggerCORE.Data
{
    public partial class Type
    {
        public Type()
        {
            LogMessage = new HashSet<LogMessage>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LogMessage> LogMessage { get; set; }
    }
}
