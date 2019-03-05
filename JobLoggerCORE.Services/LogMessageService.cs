using System;
using System.IO; 
using System.Linq; 
using JobLoggerCORE.DataAccessObjects;
using JobLoggerCORE.Data;
using JobLoggerCORE.IServices;
using JobLoggerCORE.IRepositories;
using JobLoggerCORE.Common.Exceptions;
namespace JobLoggerCORE.Services
{
    public class LogMessageService : BaseService<DTOLogMessage, LogMessage>, ILogMessageService
    {
        private readonly ILogConfigurationService logConfigurationService;
        private readonly ITypeService typeService;
        private new readonly ILogMessageRepository repository;

        public LogMessageService(ILogConfigurationService logConfigurationService, ITypeService typeService, ILogMessageRepository repository)
        {
            this.logConfigurationService = logConfigurationService;
            this.typeService = typeService;
            this.repository = repository;
            base.repository = repository;
        }

        public void LogMessage(string message, int type)
        {
            try
            {
                var cnf = logConfigurationService.List().FirstOrDefault();
                ConsoleColor console;
                if (cnf == null)
                    throw new BusinessException("The log is not configured yet");
                if (!logConfigurationService.IsValidConfiguration(cnf))
                    throw new BusinessException("Invalid configuration");
                var tp = typeService.GetBy(type);
                if (tp == null)
                    throw new BusinessException("The type specified not exist");
                switch (tp.Description)
                {
                    case "Message":
                        if (!cnf.LogMessage)
                            return;
                        console = ConsoleColor.White;
                        break;
                    case "Error":
                        if (!cnf.LogError)
                            return;
                        console = ConsoleColor.Red;
                        break;
                    case "Warning":
                        if (!cnf.LogWarning)
                            return;
                        console = ConsoleColor.Yellow;
                        break;
                    default:
                        throw new BusinessException("The type specified not exist");
                }
                if (cnf.LogToDataBase)
                    this.Create(new DTOLogMessage { Message = message, IdType = type });
                if (cnf.LogToFile)
                {
                    // string logFileDirectory = ConfigurationManager.AppSettings["LogFileDirectory"].ToString();
                    string logFileDirectory = @"C:\temp\";
                    if (!Directory.Exists(logFileDirectory))
                        Directory.CreateDirectory(logFileDirectory);
                    if (!File.Exists(logFileDirectory + "LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
                    {
                    var file = File.Create(logFileDirectory + "LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                        file.Close();
                    }
                    string currentText = File.ReadAllText(logFileDirectory + "LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                    currentText += DateTime.Now.ToShortDateString() +":" + tp.Description + " - " + message;

                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(logFileDirectory + "LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt", false))
                    {
                        file.Write(currentText);
                    }

                 //   File.WriteAllText(logFileDirectory + "LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt", currentText);
                 
                    
                }
                if (cnf.LogToConsole) {
                    Console.ForegroundColor = console;
                    Console.WriteLine(DateTime.Now.ToShortDateString() + message);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
