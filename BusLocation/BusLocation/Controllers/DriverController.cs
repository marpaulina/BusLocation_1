using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BusLocation.Models;

namespace BusLocation.Controllers
{
    public class DriverController : Controller
    {
        Repository repo = new Repository();
        // GET: Driver
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Drivers()
        {
            bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (val1)
            {
                return View(repo.GetAllDrivers());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Drivers(DriverModels model)
        {
            bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (val1)
            {
                return View(repo.GetAllDrivers());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Create()
        {
                return View();
          
        }

        [HttpPost]
        public ActionResult Create(DriverModels model)
        {
            if (ModelState.IsValid)
            {
                repo.AddDriver(model);
                return View("Drivers", repo.GetAllDrivers());
            }
            return View(model);
        }

       
        public ActionResult Delete(int id)
        {
                repo.DeleteDriverByID(id);
                return View("Drivers", repo.GetAllDrivers());
        }

        public ActionResult Edit(int id)
        {
            return View(repo.GetDriverByID(id));
        }

        [HttpPost]    
        public ActionResult Edit(DriverModels model)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateDriver(model);
                return View("Drivers", repo.GetAllDrivers());
            }
            return View(model);
        }


    }
}