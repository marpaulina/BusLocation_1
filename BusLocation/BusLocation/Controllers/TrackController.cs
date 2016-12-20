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
            //dodanie 50 intow do timemodels
            //if(repo.GetTimeByID(0) == null)
            //{
            //   for(int i = 1; i <= 50; i++)
            //    {
            //        repo.AddTime(i);
            //    }
            //}
            List<TrackModels> t = repo.GetAllTracks();
            return View(t);
        }
        [HttpPost]
        public ActionResult Tracks(TrackModels model)
        {
            setDataToView();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.TextBoxNameEnable = true;
            setDataToView();
            return View();
        }

        [HttpPost]
        public ActionResult Create(string add, string create, TrackModels model)
        {
            setDataToView();
            if (add != null)
            {
                if ( repo.GetAllTracks() == null || repo.GetAllTracks().Count == 0 || Session["TextBoxNameDisable"].Equals(false))
                {
                    ViewBag.TextBoxNameEnable = false;
                    repo.AddTrack(model);

                }
                else
                {
                    TrackModels track = repo.GetLastTrack(); 
                    ViewBag.TextBoxNameEnable = false;
                    List<BusStopModels> busStops = track.BusStopsList;
                    busStops.Add(repo.GetBusStopByID(model.BusStopId));

                    List<TimeModels> times = track.TimeToNextStopsList;
                    times.Add(repo.GetTimeByID(model.TimeId));

                    track.BusStopsList = busStops;
                    track.TimeToNextStopsList = times;
                    repo.UpdateTrack(track);
                }
                return View(repo.GetLastTrack());
            }
            else
            {
                if (repo.GetLastTrack().BusStopsList.Count > 1)
                {
                    //cos tu nie gara
                    //repo.GetLastTrack().TimeToNextStopsList.Remove(repo.GetLastTrack().TimeToNextStopsList.Count() - 1);
                    return View("Tracks", repo.GetAllTracks());
                }
                else
                {
                    //tu tez
                    //ViewBag.Crete = "Trasa musi zawierac conajmniej dwa przystanki";
                    return View(repo.GetLastTrack());
                }
            }
        }

        public ActionResult Delete(int id, int trackId, Boolean creat)
        {
            setDataToView();
            ViewBag.TextBoxNameEnable = false;

            TrackModels track = repo.GetTrackByID(trackId);
            List<BusStopModels> busStops = track.BusStopsList;
            List<TimeModels> times = track.TimeToNextStopsList;
            
            for(int i = 0; i < busStops.Count; i++)
            {
                if(busStops[i].Id == id)
                {
                    busStops.RemoveAt(i);
                    times.RemoveAt(i);
                }
            }
            track.BusStopsList = busStops;
            track.TimeToNextStopsList = times;
            repo.UpdateTrack(track);

            if (creat){
                return View("Create", repo.GetTrackByID(trackId));
            }
            else {
                return View("Edit", repo.GetTrackByID(trackId));
            }
        }

          public ActionResult DeleteTrack(int id)
        {
            TrackModels t = repo.GetTrackByID(id);
            t.BusStopsList = null;
            t.TimeToNextStopsList = null;
            repo.UpdateTrack(t);
            repo.DeleteTrackByID(id);
            return View("Tracks", repo.GetAllTracks());
        }

        public ActionResult Edit(int id)
        {
            ViewBag.TextBoxNameEnable = true;
            setDataToView();
            return View(repo.GetTrackByID(id));
        }

        [HttpPost]
        public ActionResult Edit(string add, string save, TrackModels model)
        {
            setDataToView();
            ViewBag.TextBoxNameEnable = true;
            TrackModels track = repo.GetTrackByID(model.Id);
            if(track.NameTrack != model.NameTrack)
            {
                track.NameTrack = model.NameTrack;
            }
            if (add != null)
            {
                List<BusStopModels> busStops = track.BusStopsList;
                busStops.Add(repo.GetBusStopByID(model.BusStopId));

                List<TimeModels> times = track.TimeToNextStopsList;
                times.Add(repo.GetTimeByID(model.TimeId));

                track.BusStopsList = busStops;
                track.TimeToNextStopsList = times;
                repo.UpdateTrack(track);

                return View(repo.GetTrackByID(model.Id));
            }
            else
            {
                repo.UpdateTrack(track);
                return View("Tracks", repo.GetAllTracks());
            }
        }
        public void setDataToView()
        {
            List<BusStopModels> busStops = (List<BusStopModels>)repo.GetAllBusStops();
            ViewBag.BusStops = busStops;
            List<TimeModels> times = repo.GetAllTimes();
            ViewBag.Times = times;
        }
    }

}