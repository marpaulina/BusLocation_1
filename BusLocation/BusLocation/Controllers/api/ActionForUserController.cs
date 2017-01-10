using BusLocation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace BusLocation.Controllers
{
    public class ActionForUserController : ApiController
    {
        Repository repo = new Repository();

        //Pobieranie nazw miast i przystankow do spinner list
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCitiesAndBusStopsNames()
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            List<BusStopModels> busStops = (List<BusStopModels>)repo.GetAllBusStops();
            List<string> busStopsNames = new List<string>();
            Boolean first = true;
            string city = null;
            int i = busStops.Count;
            string resul = "";
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
                    bool b = false;
                    foreach (string key in dictionary.Keys)
                    {
                        if (key == city)
                        {
                            b = true;
                        }
                    }
                    if (b)
                    {
                        List<String> listTmp = new List<string>();
                        listTmp = dictionary[city];
                        listTmp = listTmp.Concat(busStopsNames).ToList();
                        dictionary[city] = listTmp;
                    }
                    else
                    {
                        dictionary.Add(city, busStopsNames);
                    }

                    city = busStop.City;
                    busStopsNames = null;
                    busStopsNames = new List<string>();
                    busStopsNames.Add(busStop.Name);
                }
            }
            bool b2 = false;
            foreach (string key in dictionary.Keys)
            {
                if (key == city)
                {
                    b2 = true;
                }
            }
            if (b2)
            {
                List<String> listTmp = new List<string>();
                listTmp = dictionary[city];
                listTmp = listTmp.Concat(busStopsNames).ToList();
                dictionary[city] = listTmp;
            }
            else
            {
                dictionary.Add(city, busStopsNames);
             }
                resul = city;
                var response = Request.CreateResponse(HttpStatusCode.OK, dictionary);
                return response;
            
        }
        //Pobieranie lokalizacji najbliższych przystanków
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetLocationNearestBusStops(double lat, double lon)
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

        //Sprawdzanie czy kierowca ma wybrane polaczenie
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetHasDriverActiveRoute(string login)
        {
            
            int result = 0;
            DriverModels driver = repo.GetDriverByNick(login);
            if (driver.RouteID != 0)
            {
                result = 1;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllRequestToDriver(string driverlogin)
        {
            String result = "";
            DriverModels driver = repo.GetDriverByNick(driverlogin);
            foreach( UserRequestModels req in driver.UsersRequests)
            {
                result = result + req.Id + "," + req.UserCity + "," + req.UserBusStopName + ";";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        //Wysylanie aktualnej lokalizacji przez kierowce
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage LocationDriver(string login, double lat, double lon)
        {
            String result = "";

            DriverModels driver = repo.GetDriverByNick(login);
            repo.UpdateDriver(driver.Id, lat, lon);
            driver = repo.GetDriverByNick(login);
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            int min = 0;

            bool driverWillNotStop = false;
            List<UserRequestModels> usersRequests = repo.GetAllUserRequest();
            foreach (UserRequestModels usersRequest in usersRequests)
            {
                min = currentTime.Subtract(usersRequest.TimeAddRequest).Minutes;
                driverWillNotStop = (min > 5);
                if (!driverWillNotStop)
                {
                    if (driver.Id == usersRequest.Driver.Id && !usersRequest.DriversGetRequest)
                    {
                        driver.UsersRequests.Add(usersRequest);
                        repo.UpdateUserRequest(usersRequest.Id);
                        result = result + usersRequest.Id + "," + usersRequest.UserCity + "," + usersRequest.UserBusStopName + "; ";
                    }
                }
                else
                {
                repo.UpdateDriver(usersRequest.Driver, usersRequest.Id);
                repo.DeleteUserRequestByID(usersRequest.Id);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //Sprawdzanie czy login i haslo są poprawne
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CheckLogin(string login, string password)
        {
            string result = "0,";
            List<DriverModels> drivers = (List<DriverModels>) repo.GetAllDrivers();
            foreach(DriverModels driver in drivers)
            {
                if (driver.Nick == login && driver.Password == password)
                {
                    if (driver.RouteID == 0)
                    {
                        result = "1,";
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        BusStopModels busStop = repo.GetBusStopByID(driver.BusStopID);
                        result = "2,"+driver.RouteID+";"+busStop.City+" "+busStop.Name+" "+driver.TimeFromBusStop;
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //Odpowiedz na request usera
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage AnswearToUser(string login, int userRequestID, string message)
        {
            string result = "";
            UserRequestModels userRequest = repo.GetUserRequestByID(userRequestID);
            repo.UpdateUserRequest(userRequestID, message);

            DriverModels driver = repo.GetDriverByNick(login);
            repo.UpdateDriver(driver, userRequestID);
            
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }

        //Sprwdzanie czy polaczenie jest aktywne
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get(int routeID)
        {
            string result = "0";
            RouteModels route = repo.GetRouteByID(routeID);
            DriverModels driver = repo.GetDriverByID(route.DriverID);
            if (route.active)
            {
                result = "1,"+ driver.Latitude + "," + driver.Longitude;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        //usun userReques
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage RemoveAllRequest(Boolean delete)
        {
            string result = "0";
            List<UserRequestModels> reques = repo.GetAllUserRequest();
            foreach (UserRequestModels req in reques)
            {
                repo.DeleteUserRequestByID(req.Id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
       
    //Prosba o zatrzymanie na przystanku
    [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage RequestToStopDriver(int routeID, string city, string name)
        {
            string result = "0";
            RouteModels route = repo.GetRouteByID(routeID);
            if (route.DriverID != 0)
            {
                repo.AddUserRequest(route.DriverID, city, name);
                result = repo.LastUserRequest() + "";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //Sprawdzenie czy kierowca odpowiedział
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CheckingDriversAnswear(int userRequestID)
        {
            string result = "0";
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            UserRequestModels userRequest = repo.GetUserRequestByID(userRequestID);
            int min = currentTime.Subtract(userRequest.TimeAddRequest).Minutes;

            bool driverWillNotStop = (min > 5) || userRequest.Message.Contains("false");
            if (driverWillNotStop)
            {
                if (min > 5)
                {
                  repo.UpdateDriver(userRequest.Driver, userRequestID);
                }
                result = "nie zatrzyma";
                repo.DeleteUserRequestByID(userRequestID);              
            }
            else
            {
                if (userRequest.Message.Contains("true"))
                {
                    result = "zatrzyma";
                    repo.DeleteUserRequestByID(userRequestID);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // Aktywacja lub deaktywacja połączenia
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetActiveteOrDeacuivate(int routeID, string city, string name, string time, string login, Boolean Activate)
        {
            //powinno
            string data = null;
            DriverModels driver = repo.GetDriverByNick(login);
            RouteModels route = repo.GetRouteByID(routeID);

            List<BusStopModels> busStops = (List<BusStopModels>)repo.GetAllBusStops();
            int busStopId = 0;
            foreach (BusStopModels busStop in busStops)
            {
                if(busStop.City == city && busStop.Name == name)
                {
                    busStopId = busStop.Id;
                }
            }
            repo.UpdateRoute(routeID, driver.Id, Activate);
            repo.UpdateDriver(driver.Id, routeID, busStopId, TimeSpan.Parse(time), Activate);
            route = repo.GetRouteByID(routeID);
            driver = repo.GetDriverByNick(login);
            data = " Driver id : " + driver.Id + " login : " + driver.Nick + " route ID : " + driver.RouteID + 
                            " Route ID : " + route.Id + " busStop ID :"+ driver.BusStopID + " time : "+ driver.TimeFromBusStop.ToString() + " active : "+route.active+" driver ID : " + route.DriverID;    
            return Request.CreateResponse(HttpStatusCode.OK, data);
           
        }

        //Pobieranie polaczennia po wybraniu miasta,przystaku,czasu
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetRoute(string city, string name, TimeSpan time, Boolean driver)
        {
            name = name.Trim();
            TimeSpan timeUp = time.Add(new TimeSpan(1, 20, 0));
            TimeSpan timeDown = time.Subtract(new TimeSpan(1, 20, 0));
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
                    if (busStops[j].City == city && busStops[j].Name == name)
                    {
                        RouteModels r = routes[i];
                        TimeSpan t = new TimeSpan(0, timeFromFirstBusStop, 0);
                        TimeSpan timeOnBusStop = r.StartTime;
                        TimeSpan newTime = timeOnBusStop.Add(t);


                        if (r.StartTime < timeUp && r.StartTime > timeDown)
                        {
                            if (driver)
                            {
                                if (!routes[i].active)
                                {
                                    dictionary.Add(r.Id, new List<string> {routes[i].Track.NameTrack ,city, name, newTime.ToString() });
                                }
                            }
                            else
                            {
                                dictionary.Add(r.Id, new List<string> { routes[i].Track.NameTrack, city, name, newTime.ToString() });
                            }
                            break;
                        }
                    }
                }
            }

            if (dictionary.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, dictionary);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, dictionary);
            }
        }

        // Pobieranie polaczennia po wybraniu miasta i przystaku początkowego, miasta i przystanku koncowego, czasu
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
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
                        dictionary.Add(route.Id, new List<string> { route.Track.NameTrack, cityFrom, nameFrom, newTimeStart[k].ToString() });
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