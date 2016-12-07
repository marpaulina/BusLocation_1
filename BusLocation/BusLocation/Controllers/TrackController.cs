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

        public ActionResult Create()
        {
            List<BusStopModels> busStops = (List<BusStopModels>) repo.GetAllBusStops(); 
            ViewBag.BusStops = busStops;
            return View();
        }

        [HttpPost]
        public ActionResult Create(string add, string create, TrackModels model)
        {
            List<BusStopModels> busStops = (List<BusStopModels>)repo.GetAllBusStops();
            ViewBag.BusStops = busStops;

            if (add != null)
            {
                if (!(repo.GetLastTrack().NameTrack == model.NameTrack))
                {
                    repo.AddTrack(model);
                }
                else
                {
                    List<BusStopModels> b = repo.GetLastTrack().BusStops;
                    b.Add(repo.GetBusStopByID(model.BusStopId));
                    repo.UpdateTrack(repo.GetLastTrack(), b);
                }
                return View(repo.GetLastTrack());
            }
            else
            {
                return View("Tracks", repo.GetLastTrack());
            }

        }
        [HttpPost]
        public ActionResult AddBusStop( TrackModels model)
        {
            List<BusStopModels> busStops = (List<BusStopModels>)repo.GetAllBusStops();
            ViewBag.BusStops = busStops;
            if (!(repo.GetLastTrack().NameTrack == model.NameTrack))
            {
                repo.AddTrack(model);
            }
            else
            {
                repo.UpdateTrack(model);
            }
            return View(repo.GetLastTrack());
        }

        public ActionResult Delete(int id)
        {
            TrackModels track = repo.GetLastTrack();
            List<BusStopModels> busStops = track.BusStops;
            foreach ( BusStopModels b in busStops)
            {
                if(b.Id == id)
                {
                    busStops.Remove(b);
                    break;
                }
            }
            track.BusStops = busStops;
            repo.UpdateTrack(track);
            return View("Create", repo.GetLastTrack());
        }
          public ActionResult DeleteTrack(int id)
        {
            TrackModels t = repo.GetTrackByID(id);
            t.BusStops = null;
            repo.UpdateTrack(t);
            repo.DeleteTrackByID(id);
            return View("Tracks", repo.GetAllTracks());
        }
       
    }
}