using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobLogger.DependencyInjection;
using JobLogger.IServices;
using JobLogger.Services;
using JobLogger.Repositories;
using JobLogger.IRepositories;
using JobLogger.Common.Exceptions;
using Moq;
using JobLogger.DataAccessObjects;
using System.Collections.Generic;
using System.IO;
using System.Configuration;

namespace JobLogger.Test
{
    [TestClass]
    public class LogMessageServiceTest
    {
        private ILogMessageService logMessageService;

        [TestInitialize]
        public void initialize()
        {
            // logMessageService = UnityResolver.Resolve<Il>
            UnityResolver.Resolve(out logMessageService);
        }

        [TestMethod]
        public void LogMessageTest_WithNullCnf()
        {
            var ex = Assert.ThrowsException<BusinessException>(() => logMessageService.LogMessage("ola", 1));
            Assert.AreEqual(ex.Message, MessageExceptions.LogCnfNull);
        }

        [TestMethod]
        public void LogMessageTesT()
        {
            //Arrange
            Mock<ILogConfigurationService> mockLogConfigurationService = new Mock<ILogConfigurationService>();
            Mock<ITypeService> mockTypeService = new Mock<ITypeService>();
            ILogMessageRepository repository = UnityResolver.Resolve<ILogMessageRepository>();
            var idType = 1;
            DTOLogConfiguration dtoCnf;
            string messageToSend = "Test message";
            string logFileDirectory = ConfigurationManager.AppSettings["LogFileDirectory"].ToString();

            var sm = DateTime.Now.ToString("yyyyMMdd");
            if (File.Exists(logFileDirectory + "LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
                File.Delete(logFileDirectory + "LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt");


            //Case : Configuration is null
            mockLogConfigurationService.Setup(t => t.List()).Returns(()=> { return new List<DTOLogConfiguration>(); });
            logMessageService = new LogMessageService(mockLogConfigurationService.Object, mockTypeService.Object, repository);

            var ex = Assert.ThrowsException<BusinessException>(() => logMessageService.LogMessage(messageToSend, 1));
            Assert.AreEqual(ex.Message, MessageExceptions.LogCnfNull);

            // Case : Invalid Configuration
            dtoCnf = new DTOLogConfiguration
            {
                LogToConsole = false,
                LogToDataBase = false,
                LogToFile = false
            };
            mockLogConfigurationService.Setup(t => t.List()).
                Returns(
                () => {
                    return new List<DTOLogConfiguration>()
                    {dtoCnf }; });
            logMessageService = new LogMessageService(mockLogConfigurationService.Object, mockTypeService.Object, repository);
             ex = Assert.ThrowsException<BusinessException>(() => logMessageService.LogMessage(messageToSend, 1));
            Assert.AreEqual(ex.Message, MessageExceptions.InvalidCnf);

            //Case : Invalid Type
            dtoCnf = new DTOLogConfiguration
            {
                LogToConsole = true,
                LogToDataBase = true,
                LogToFile = true,
                LogError = true,
                LogMessage = true,
                LogWarning = true
            };
            mockLogConfigurationService.Setup(t => t.List()).
               Returns(
               () => {
                   return new List<DTOLogConfiguration>()
                   {dtoCnf };
               });
            mockLogConfigurationService.Setup(t => t.IsValidConfiguration(dtoCnf)).
             Returns(
             () => {
               return true;
             });
          
            mockTypeService.Setup(t => t.GetBy(idType)).
              Returns(
              () => {
                  return null;
              });
            logMessageService = new LogMessageService(mockLogConfigurationService.Object, mockTypeService.Object, repository);
            ex = Assert.ThrowsException<BusinessException>(() => logMessageService.LogMessage(messageToSend, idType));
            Assert.AreEqual(ex.Message, MessageExceptions.TypeNotExist);

            // Case : Work Correctly
            idType = 1;

            mockTypeService.Setup(t => t.GetBy(idType)).
            Returns(
            () => {
               return new DTOType
                {
                   Id=idType,
                   Description = "Message"
               };
            });
            logMessageService = new LogMessageService(mockLogConfigurationService.Object, mockTypeService.Object, repository);
            int countLogMessageService = logMessageService.List().Count;
            logMessageService.LogMessage(messageToSend, idType);
            Assert.IsTrue(countLogMessageService< logMessageService.List().Count);
            Assert.IsTrue(File.Exists(logFileDirectory + "LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt"));
           

        }

    }
}
