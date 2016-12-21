using BusLocation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusLocation.Controllers
{
    public class BusStopController : Controller
    {
        Repository repo = new Repository();
        // GET: BosStop
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BusStops()
        {
            return View(repo.GetAllBusStops());
        }

        [HttpPost]
        public ActionResult BusStops(BusStopModels model)
        {
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BusStopModels model)
        {
            if (ModelState.IsValid)
            {
                repo.AddBusStop(model);
                return View("BusStops", repo.GetAllBusStops());
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            repo.DeleteBusStopByID(id);
            return View("BusStops", repo.GetAllBusStops());
        }

        public ActionResult Edit(int id)
        {
            return View(repo.GetBusStopByID(id));
        }

        [HttpPost]
        public ActionResult Edit(BusStopModels model)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateBusStop(model);
                return View("BusStops", repo.GetAllBusStops());
            }
            return View(model);
        }
    }
}