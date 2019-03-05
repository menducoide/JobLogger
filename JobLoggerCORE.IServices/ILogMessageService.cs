
using JobLoggerCORE.DataAccessObjects;
namespace JobLoggerCORE.IServices
{
    public interface ILogMessageService : IBaseService<DTOLogMessage>
    {
        void LogMessage(string message,int type);
    }
}
