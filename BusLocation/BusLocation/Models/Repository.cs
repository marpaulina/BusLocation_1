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
        public void UpdateDriver(DriverModels driver)
        {
            dbContext.Drivers.Find(driver.Id).Update(driver);
            dbContext.SaveChanges();
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

        public void AddTrack(TrackModels track)
        {
            TrackModels track2 = new TrackModels(track.NameTrack, track.BusStopId, track.TimeToNext);
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
        public void UpdateTrack(TrackModels track, List<BusStopModels> b, List <int> time)
        {
            dbContext.Tracks.Find(track.Id).Update(track, b, time);
            dbContext.SaveChanges();
        }
        public List<BusStopModels> GetBusStopsFromTrack(int trackID)
        {
            return dbContext.Tracks.Find(trackID).BusStops;
        }

        public List<int> GetTimeList(int trackID)
        {
            return dbContext.Tracks.Find(trackID).TimeToNextStop;
        }
        public TrackModels GetLastTrack()
        {

            return GetAllTracks()[GetAllTracks().Count-1] ; 
        }


        #endregion

        //************************************************ ROUTES
        #region ROTES
        public IEnumerable<RouteModels> GetAllRoutes()
        {
            throw new NotImplementedException();
        }

        #endregion

        //************************************************
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