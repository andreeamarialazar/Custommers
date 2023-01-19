using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Custommers.Controllers
{
    public class HomeController : Controller
    {
        private readonly CustommerService.ICustommerService custommerServiceClient; 
        public HomeController() 
        { this.custommerServiceClient = new CustommerService.CustommerServiceClient(); 
        } 
        public ActionResult Index()
        { CustommerService.Custommer[] 
                custommers = custommerServiceClient.GetCustommers();
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
            try { CustommerService.Custommer custommer = new CustommerService.Custommer(); 
                UpdateModel(custommer); 
                CustommerService.Error[] errors = custommerServiceClient.AddCustommer(custommer); 
                if (errors.Any()) { ViewData["Error"] = errors; return View(custommer); 
                }else return RedirectToAction("Index"); 
            } catch { return View();
            } }

    }
}