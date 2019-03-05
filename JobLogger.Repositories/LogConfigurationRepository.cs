using JobLogger.Data;
using JobLogger.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLogger.Repositories
{
   public class LogConfigurationRepository :BaseRepository<LogConfiguration> , ILogConfigurationRepository
    {
    }
}
