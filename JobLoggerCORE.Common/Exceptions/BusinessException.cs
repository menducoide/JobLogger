using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLoggerCORE.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) {

        }

    }
}
