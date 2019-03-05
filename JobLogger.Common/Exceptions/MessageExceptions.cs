using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLogger.Common.Exceptions
{
  public static  class MessageExceptions
    {
        public static readonly string TypeNotExist = "The type specified not exist";
        public static readonly string InvalidCnf = "Invalid configuration";
        public static readonly string LogCnfNull = "The log is not configured yet";

    }
}
