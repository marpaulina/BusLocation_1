using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusLocation.Models;

namespace BusLocation.Controllers
{
    public class TrackController : Controller
    {
        Repository repo = new Repository();
        // GET: Track
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Tracks()
        {
            return View(repo.GetAllTracks());
        }
        [HttpPost]
        public ActionResult Tracks(TrackModels model)
        {
            return View(model);
        }

        public ActionResult CreateTrack()
        {
            List<BusStopModels> busStops = (List<BusStopModels>) repo.GetAllBusStops(); 
            ViewBag.BusStops = busStops;
            return View("Create");
        }

        [HttpPost]
        public ActionResult CreateTrack(TrackModels model)
        {
            //repo.AddTrack(model);
           
            return View("Create", repo.GetAllTracks());
        }
        [HttpPost]
        public ActionResult AddBusStop(TrackModels model)
        {
            //repo.AddTrack(model);
            return View("Create", repo.GetAllTracks());
        }
    }
}