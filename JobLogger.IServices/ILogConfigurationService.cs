
using JobLogger.DataAccessObjects;
namespace JobLogger.IServices
{
    public interface ILogConfigurationService  : IBaseService<DTOLogConfiguration> 
    {
        bool IsValidConfiguration(DTOLogConfiguration dto);
    }
}
