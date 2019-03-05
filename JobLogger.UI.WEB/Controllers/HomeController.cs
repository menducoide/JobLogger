using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobLogger.IServices;

namespace JobLogger.UI.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServices.ILogMessageService logMessageService;
        private readonly IServices.ITypeService typeService;

        public HomeController(ILogMessageService logMessageService, ITypeService typeService)
        {
            this.logMessageService = logMessageService;
            this.typeService = typeService;
        }

        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult About()
        {
            string msj = "";

            foreach (var item in typeService.List())
            {
                msj += item.Description + "\n";
            }
            ViewBag.Message = msj;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}