using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLogger.DataAccessObjects
{
   public class DTOLogMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int IdType { get; set; }

    }
}
