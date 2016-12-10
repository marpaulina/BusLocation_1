using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLocation.Models
{
    interface IRepository : IDisposable
    {
        #region Driver

        IEnumerable<DriverModels> GetAllDrivers();
        DriverModels GetDriverByID(int driverId);
        void AddDriver(DriverModels driver);
        void DeleteDriverByID(int deriverId);
        void UpdateDriver(DriverModels driver);

        #endregion

        #region BusStop
        IEnumerable<BusStopModels> GetAllBusStops();
        BusStopModels GetBusStopByID(int busStopId);
        void AddBusStop(BusStopModels busStop);
        void DeleteBusStopByID(int busStopId);
        void UpdateBusStop(BusStopModels busStop);

        #endregion

        #region Track
        List<TrackModels> GetAllTracks();
        TrackModels GetTrackByID(int trackID);
        void AddTrack(TrackModels track);
        void DeleteTrackByID(int trackID);
        void UpdateTrack(TrackModels track);
        List<BusStopModels> GetBusStopsFromTrack(int trackID);
        List<int> GetTimeList(int trackID);
        TrackModels GetLastTrack();

        #endregion

        #region Route
        List<RouteModels> GetAllRoutes();
        RouteModels GetRouteByID(int routeID);
        void AddRoute(RouteModels route);
        void DeleteRouteByID(int routeID);
        void UpdateRoute(RouteModels route);
        #endregion

        void Save();
    }
}
