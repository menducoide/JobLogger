using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobLoggerCore.UI.Web.Models;
using JobLoggerCORE.Data;
using JobLoggerCORE.IServices;
using JobLoggerCORE.DataAccessObjects;
namespace JobLoggerCore.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITypeService typeService;

        public HomeController(ITypeService typeService)
        {
            this.typeService = typeService;
        }

        public IActionResult Index()
        {
            ViewBag.Type = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(typeService.List(),"Id", "Description",null);
            return View();
        }
        [HttpPost]
        public IActionResult Index(DTOLogMessage dto)
        {
            ViewBag.Type = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(typeService.List(), "Id", "Description", null);
            return PartialView("_Success");
        }
        public IActionResult About()
        {
           
            ViewData["Message"] = typeService.List().FirstOrDefault().Description;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
