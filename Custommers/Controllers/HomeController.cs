using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Custommers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ServiceProxy.CustommerService.CustommerServiceProxy custommerServiceProxy; 
        public HomeController() 
        { this.custommerServiceProxy = new ServiceProxy.CustommerService.CustommerServiceProxy(); 
        } 
        public ActionResult Index()
        { ServiceProxy.CustommerService.Custommer[] 
                custommers = custommerServiceProxy.GetCustommers();
            return View(custommers); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Test/Create
        public ActionResult Create() { return View(); } 
        // POST: Test/Create
        [HttpPost] 
        public ActionResult Create(FormCollection collection) { 
            try {
                ServiceProxy.CustommerService.Custommer custommer = new ServiceProxy.CustommerService.Custommer();
                UpdateModel(custommer);
                ServiceProxy.CustommerService.Error[] errors =
               custommerServiceProxy.AddCustommer(custommer);

                if (errors.Any()) { ViewData["Error"] = errors; return View(custommer); 
                }else return RedirectToAction("Index"); 
            } catch { return View();
            } }

    }
}