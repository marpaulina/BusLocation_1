using BusLocation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Collections;
using System.Net;
using System.Web.Http;

namespace BusLocation.Controllers
{
    public class ActionForUserController : ApiController
    {
        Repository repo = new Repository();


        //[System.Web.Http.ActionName("GetName")]
        //[System.Web.Http.Route("api/ActionForUserController")]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get()
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            List<BusStopModels> busStops = (List<BusStopModels>)repo.GetAllBusStops();
            List<string> busStopsNames = new List<string>();
            Boolean first = true;
            string city = null;
            foreach (BusStopModels busStop in busStops)
            {
                if (first)
                {
                    city = busStop.City;
                    first = false;
                }
                if (city == busStop.City)
                {
                    busStopsNames.Add(busStop.Name);
                    city = busStop.City;
                }
                else
                {
                    dictionary.Add(city, busStopsNames);
                    city = busStop.City;
                    busStopsNames = null;
                    busStopsNames = new List<string>();
                    busStopsNames.Add(busStop.Name);
                }
            }
            dictionary.Add(city, busStopsNames);
            var response = Request.CreateResponse( HttpStatusCode.OK, dictionary );
            return response;
        }
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get(double lat, double lon)
        {
            Dictionary<string, List<double>> dictionary = new Dictionary<string, List<double>>();
            var busStops = repo.GetAllBusStops();
            double latUp = lat + 0.03;
            double latDown = lat - 0.03;
            double lonUp = lon + 0.03;
            double lonDown = lon - 0.03; 
            foreach (BusStopModels busStop in busStops)
            {
                if((latDown < busStop.Latitiude) && (busStop.Latitiude < latUp) && (lonDown < busStop.Longitiude) && (busStop.Longitiude < lonUp))
                {
                    dictionary.Add(busStop.City +" "+busStop.Name, new List<double> { busStop.Latitiude, busStop.Longitiude });
                }
            }
            if(dictionary.Count() > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, dictionary);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, dictionary);
            }
        }
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get(string login, string password)
        {
            int result;
            List<DriverModels> drivers = (List<DriverModels>) repo.GetAllDrivers();
            foreach(DriverModels driver in drivers)
            {
                if (driver.Nick == login && driver.Password == password)
                {
                    if (driver.RouteID == 0)
                    {
                        result = 1;
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        result = 2;
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
            }
            result = 0;
            return Request.CreateResponse(HttpStatusCode.NotFound, result);
        }
        public HttpResponseMessage GetRoute(string city, string name, string time, Boolean driver)
        {
            name = name.Trim();
            TimeSpan timeUp = TimeSpan.Parse(time).Add(new TimeSpan(1, 20, 0));
            TimeSpan timeDown = TimeSpan.Parse(time).Subtract(new TimeSpan(1, 20, 0));
            Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();

            List<RouteModels> routes = repo.GetAllRoutes();
            List<TimeModels> times = null;
                for (int i = 0; i < routes.Count; i++)
                {
                    List<BusStopModels> busStops = routes[i].Track.BusStopsList;
                    int timeFromFirstBusStop = 0;
                    times = routes[i].Track.TimeToNextStopsList;

                    for (int j = 0; j < busStops.Count; j++)
                    {
                        timeFromFirstBusStop += times[j].value;
                        if(busStops[j].City == city && busStops[j].Name == name)
                        {
                            RouteModels r = routes[i];
                            TimeSpan t = new TimeSpan(0, timeFromFirstBusStop, 0);
                            TimeSpan timeOnBusStop = r.StartTime;
                            TimeSpan newTime = timeOnBusStop.Add(t);
                           
                        
                            if(r.StartTime < timeUp && r.StartTime > timeDown)
                            {
                                if ( driver )
                                {
                                    if (!routes[i].active)
                                    {
                                        dictionary.Add(r.Id, new List<string> { city, name, newTime.ToString() });
                                    }
                                }
                                else
                                {
                                    dictionary.Add(r.Id, new List<string> { city, name, newTime.ToString() });
                                }
                            break;
                            }
                        }
                    }
                }
            
            if (dictionary.Count > 0) {
                return Request.CreateResponse(HttpStatusCode.OK, dictionary);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, dictionary);
            }
        }
        public HttpResponseMessage Get(int routeID, string login, Boolean Activate)
        {
            String data = null;
            DriverModels driver = repo.GetDriverByNick(login);
            RouteModels route = repo.GetRouteByID(routeID);
            
                repo.UpdateRoute(routeID, driver.Id, Activate);
                repo.UpdateDriver(driver.Id, routeID, Activate);
                route = repo.GetRouteByID(routeID);
                driver = repo.GetDriverByNick(login);
                data = " Driver id : " + driver.Id + " login : " + driver.Nick + " route ID : " + driver.RouteID + 
                            " Route id : " + route.Id + " active : "+route.active+" driver ID : " + route.DriverID; 
               
                return Request.CreateResponse(HttpStatusCode.OK, data);
           
        }
        public HttpResponseMessage Get(int routeID)
        {
            int result = 0;
            RouteModels route = repo.GetRouteByID(routeID);
            if(route.active)
            {
                result = 1;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        public HttpResponseMessage Get(string login)
        {
            int result = 0;
            DriverModels driver = repo.GetDriverByNick(login);
            if(driver.RouteID != 0)
            {
                result = 1;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        public HttpResponseMessage GetRoute2(string cityFrom, string nameFrom, string cityTo, string nameTo, TimeSpan time)
        {
            TimeSpan timeUp = time.Add(new TimeSpan(1, 20, 0));
            TimeSpan timeDown = time.Subtract(new TimeSpan(1, 20, 0));
            List<RouteModels> routes = repo.GetAllRoutes();
            List<RouteModels> checkedRoutes = new List<RouteModels>();
            Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
            List<TimeSpan> newTimeStart = new List<TimeSpan>();
            for (int i = 0; i < routes.Count; i++)
            {
                List<BusStopModels> busStops = routes[i].Track.BusStopsList;
                int timeFromFirstBusStop = 0;
                List<TimeModels> times = routes[i].Track.TimeToNextStopsList;
                for (int j = 0; j < busStops.Count; j++)
                {
                    timeFromFirstBusStop += times[j].value;
                    if (busStops[j].City == cityFrom && busStops[j].Name == nameFrom)
                    {
                        RouteModels r = routes[i];
                        TimeSpan t = new TimeSpan(0, timeFromFirstBusStop, 0);
                        TimeSpan timeOnBusStop = r.StartTime;
                        TimeSpan t2 = timeOnBusStop.Add(t);
                        if ( t2 < timeUp && t2 > timeDown)
                        {
                            newTimeStart.Add(t2);
                            checkedRoutes.Add(r);
                        }
                        
                    }
                }
            }
            int k = 0;
            foreach (RouteModels route in checkedRoutes)
            {
                List<BusStopModels> busStops = route.Track.BusStopsList;
                foreach (BusStopModels busStop in busStops)
                {
                    if (busStop.City == cityTo && busStop.Name == nameTo)
                    {
                        dictionary.Add(route.Id, new List<string> { cityFrom, nameFrom, newTimeStart[k].ToString() });
                    }
                }
                k++;
            }
            
            if (dictionary.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, dictionary);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, dictionary);
            }
        }
    }
}