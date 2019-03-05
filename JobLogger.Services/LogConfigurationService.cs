using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobLogger.DataAccessObjects;
using JobLogger.Data;
using JobLogger.IRepositories;
using JobLogger.IServices;

namespace JobLogger.Services
{
  public  class LogConfigurationService : BaseService<DTOLogConfiguration,LogConfiguration>,ILogConfigurationService
    {
        public LogConfigurationService(ILogConfigurationRepository repository)
        {
            this.repository = repository;
        }

        public bool IsValidConfiguration(DTOLogConfiguration dto) {
        
            if (dto == null)
                return false;          
            if (!dto.LogError && !dto.LogMessage && !dto.LogWarning)
                return false;
            if (!dto.LogToFile && !dto.LogToConsole && !dto.LogToDataBase)
                return false;

            return true;
        }
    }
}
