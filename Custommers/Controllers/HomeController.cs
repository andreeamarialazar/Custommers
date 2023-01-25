using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Custommers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ServiceProxy.CustommerServiceProxy custommerServiceProxy;
        private readonly ResourceProxy.CustommerResourceProxy custommerResourceProxy;
        public HomeController()
        {
            this.custommerServiceProxy = new ServiceProxy.CustommerServiceProxy();
            this.custommerResourceProxy = new ResourceProxy.CustommerResourceProxy();
        }
        public ActionResult Index()
        {
            ServiceProxy.Custommer[]
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ServiceProxy.Custommer custommer = new ServiceProxy.Custommer();
                UpdateModel(custommer);
                ServiceProxy.Error[] errors =
               custommerServiceProxy.AddCustommer(custommer);

                if (errors.Any())
                {
                    ViewData["Error"] = errors; return View(custommer);
                }
                else return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/EDIT
        public ActionResult Edit(Guid ID) {

            ServiceProxy.Custommer custommer = new ServiceProxy.Custommer();
            custommer= custommerServiceProxy.GetCustommerByID(ID);
            
            return View(custommer); }
        // POST: Test/EDIT
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                ResourceProxy.Custommer custommer = new ResourceProxy.Custommer();
                UpdateModel(custommer);
               
               custommerResourceProxy.UpdateCustommer(custommer);

               
               return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //GET: /DETEILS
        public ActionResult Details(Guid ID)
        {

            ServiceProxy.Custommer custommer = new ServiceProxy.Custommer();
            custommer = custommerServiceProxy.GetCustommerByID(ID);

            return View(custommer);
        }

        public ActionResult Delete(Guid ID)
        {
            try
            {
                custommerResourceProxy.DeleteCustommer(ID);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

            
        }

    }
}