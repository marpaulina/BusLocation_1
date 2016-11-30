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

        public ActionResult Create()
        {
            return View();
        }
    }
}