
using JobLogger.DataAccessObjects;
namespace JobLogger.IServices
{
    public interface ILogMessageService : IBaseService<DTOLogMessage>
    {
        void LogMessage(string message,int type);
    }
}
