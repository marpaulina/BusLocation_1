using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusLocation.Models;

namespace BusLocation.Controllers
{
    public class RouteController : Controller
    {
        Repository repo = new Repository();
        // GET: Route
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Routes()
        {
            return View(repo.GetAllRoutes());
        }

        [HttpPost]
        public ActionResult Routes(RouteModels model)
        {
            return View(model);
        }
        public ActionResult Create()
        {
            List<TrackModels> tracks = (List<TrackModels>)repo.GetAllTracks();
            ViewBag.Tracks = tracks;
            return View();
        }

        [HttpPost]
        public ActionResult Create(RouteModels model)
        {
            if (ModelState.IsValid)
            {
                repo.AddRoute(model);
                return View("Routes", repo.GetAllRoutes());
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            repo.DeleteRouteByID(id);
            return View("Routes", repo.GetAllRoutes());
        }

        public ActionResult Edit(int id)
        {
            List<TrackModels> tracks = (List<TrackModels>)repo.GetAllTracks();
            ViewBag.Tracks = tracks;
            return View(repo.GetRouteByID(id));
        }

        [HttpPost]
        public ActionResult Edit(RouteModels model)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateRoute(model);
                return View("Routes", repo.GetAllRoutes());
            }
            return View(model);
        }
    }
}