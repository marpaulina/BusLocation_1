using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BusLocation.Models
{
    public class Repository : IRepository
    {
        public static ApplicationDbContext dbContext = new ApplicationDbContext();
        //***************************************************** DRIVERS
        #region DRIVERS
        public void AddDriver(DriverModels driver)
        {
            dbContext.Drivers.Add(driver);
            dbContext.SaveChanges();
        }
        public void DeleteDriverByID(int driverId)
        {
            DriverModels driver = dbContext.Drivers.Find(driverId);
            dbContext.Drivers.Remove(driver);
            dbContext.SaveChanges();
        }
        public IEnumerable<DriverModels> GetAllDrivers()
        {
            return dbContext.Drivers.ToList();
        }
        public DriverModels GetDriverByID(int driverId)
        {
            return dbContext.Drivers.Find(driverId);
        }
        public DriverModels GetDriverByNick(string nick)
        {
            foreach(DriverModels driver in GetAllDrivers())
            {
                if(driver.Nick == nick)
                {
                    return driver;
                }
            }
            return null;
        }
        public void UpdateDriver(DriverModels driver)
        {
            dbContext.Drivers.Find(driver.Id).Update(driver);
            dbContext.SaveChanges();
        }
        public void UpdateDriver(DriverModels driver, int userRequestID)
        {
            dbContext.Drivers.Find(driver.Id).Update(userRequestID);
            dbContext.SaveChanges();
        }
        public void UpdateDriver(int driverID, double lat, double lon)
        {
            dbContext.Drivers.Find(driverID).Update(lat, lon);
            dbContext.SaveChanges();
        }
        public void UpdateDriver(int driverID, int routeID, int busStopID, TimeSpan time, Boolean activate)
        {
            if (activate)
            {
                dbContext.Drivers.Find(driverID).Update(routeID, busStopID, time);
                dbContext.SaveChanges();
            }
            else
            {
                dbContext.Drivers.Find(driverID).Update(0,0,new TimeSpan(0,0,0));
                dbContext.SaveChanges();
            }
        }

        #endregion
        //************************************************ BUS STOPS
        #region BUSSTOPS 
        public IEnumerable<BusStopModels> GetAllBusStops()
        {
            return dbContext.BusStops.ToList();
        }
        public BusStopModels GetBusStopByID(int busStopId)
        {
            return dbContext.BusStops.Find(busStopId);
        }
        public BusStopModels GetBusStopByName(string  busStopName)
        {
            return dbContext.BusStops.Find(busStopName);
        }

        public void AddBusStop(BusStopModels busStop)
        {
            dbContext.BusStops.Add(busStop);
            dbContext.SaveChanges();
        }

        public void DeleteBusStopByID(int busStopId)
        {
            BusStopModels busStop = dbContext.BusStops.Find(busStopId);
            dbContext.BusStops.Remove(busStop);
            dbContext.SaveChanges();
        }

        public void UpdateBusStop(BusStopModels busStop)
        {
            dbContext.BusStops.Find(busStop.Id).Update(busStop);
            dbContext.SaveChanges();
        }
        #endregion
        //************************************************ TRACKS
        #region TRACKS
        public List<TrackModels> GetAllTracks()
        {
            return (List<TrackModels>) dbContext.Tracks.ToList();
        }
        public TrackModels GetTrackByID(int trackID)
        {
            return dbContext.Tracks.Find(trackID);
        }
        public TrackModels GetTrackByName(String trackName)
        { 
            foreach(TrackModels track in GetAllTracks())
            {
                if (track.NameTrack.Equals(trackName))
                { 
                    return track;
                }
            }
            return null;
        }

        public void AddTrack(TrackModels track)
        {
            TrackModels track2 = new TrackModels(track.NameTrack, track.BusStopId, track.TimeId);
            dbContext.Tracks.Add(track2);
            dbContext.SaveChanges();
        }

        public void DeleteTrackByID(int trackID)
        {
            TrackModels track = dbContext.Tracks.Find(trackID);
            dbContext.Tracks.Remove(track);
            dbContext.SaveChanges();
        }

        public void UpdateTrack(TrackModels track)
        {
            dbContext.Tracks.Find(track.Id).Update(track);
            dbContext.SaveChanges();
        }
        public List<BusStopModels> GetBusStopsFromTrack(int trackID)
        {
            return dbContext.Tracks.Find(trackID).BusStopsList;
        }

        public List<TimeModels> GetTimeList(int trackID)
        {
            return dbContext.Tracks.Find(trackID).TimeToNextStopsList;
        }
        public TrackModels GetLastTrack()
        {
            return GetAllTracks()[GetAllTracks().Count-1] ; 
        }


        #endregion

        //************************************************ ROUTES
        #region ROTES
        public List<RouteModels> GetAllRoutes()
        {
            return (List<RouteModels>)dbContext.Routes.ToList();
        }
        

        public RouteModels GetRouteByID(int routeID)
        {
            return dbContext.Routes.Find(routeID);
        }

        public void AddRoute(RouteModels route)
        {
            RouteModels route2 = new RouteModels(route.StartTime, route.TrackId);
            dbContext.Routes.Add(route2);
            dbContext.SaveChanges();
        }

        public void DeleteRouteByID(int routeID)
        {
            RouteModels r = GetRouteByID(routeID);
            dbContext.Routes.Remove(r);
            dbContext.SaveChanges();
        }

        public void UpdateRoute(RouteModels route)
        {
            dbContext.Routes.Find(route.Id).Update(route);
            dbContext.SaveChanges();
        }

        public void UpdateRoute(int routeID, int driverID, Boolean activate)
        {
            dbContext.Routes.Find(routeID).Update(driverID, activate);
            dbContext.SaveChanges();
        }
        public void ActivateRoute(int routeID)
        {
            dbContext.Routes.Find(routeID).active = true;
            dbContext.SaveChanges();
        }
        public void DeactivativateRoute(int routeID)
        {
            dbContext.Routes.Find(routeID).active = false;
            dbContext.SaveChanges();
        }
       
        public List<RouteModels> GetAllActiveRoutes()
        {
            List<RouteModels> routeList = new List<RouteModels>();

            foreach(RouteModels route in (List<RouteModels>)dbContext.Routes.ToList())
            {
                if (route.active)
                {
                    routeList.Add(route);
                }
            }
            return routeList;
        }

        #endregion
        #region UserRequest
        public List<UserRequestModels> GetAllUserRequest()
        {
            return (List<UserRequestModels>)dbContext.UserRequest.ToList();
        }


        public UserRequestModels GetUserRequestByID(int userRequestID)
        {
            return dbContext.UserRequest.Find(userRequestID);
        }
        public void UpdateUserRequest(int userRequestID, string message)
        {
            dbContext.UserRequest.Find(userRequestID).Update(message);
            dbContext.SaveChanges();
        }
        public void UpdateUserRequest(int userRequestID)
        {
            dbContext.UserRequest.Find(userRequestID).Update();
            dbContext.SaveChanges();
        }

        public void AddUserRequest(int driverID, string userCity, string userBusStop)
        {
            UserRequestModels userRequest = new UserRequestModels(driverID, userCity, userBusStop);
            dbContext.UserRequest.Add(userRequest);
            dbContext.SaveChanges();
        }

        public void DeleteUserRequestByID(int userRequestID)
        {
            UserRequestModels r = GetUserRequestByID(userRequestID);
            dbContext.UserRequest.Remove(r);
            dbContext.SaveChanges();
        }
        public int LastUserRequest()
        {
            return GetAllUserRequest().Last().Id;
        }
        #endregion

        //************************************************

        #region
        public TimeModels GetTimeByID(int timeID)
        {
            return dbContext.Time.Find(timeID);
        }

        public void AddTime(TimeModels time)
        {
            TimeModels timeTmp = new TimeModels(time.value);
            dbContext.Time.Add(timeTmp);
            dbContext.SaveChanges();
        }
        public void AddTime(int value)
        {
            TimeModels timeTmp = new TimeModels(value);
            dbContext.Time.Add(timeTmp);
            dbContext.SaveChanges();
        }
        public List<TimeModels> GetAllTimes()
        {
            return (List<TimeModels>) dbContext.Time.ToList();
        }

        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

       
    }
}