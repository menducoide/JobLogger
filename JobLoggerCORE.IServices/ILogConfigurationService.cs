
using JobLoggerCORE.DataAccessObjects;
namespace JobLoggerCORE.IServices
{
    public interface ILogConfigurationService  : IBaseService<DTOLogConfiguration> 
    {
        bool IsValidConfiguration(DTOLogConfiguration dto);
    }
}
