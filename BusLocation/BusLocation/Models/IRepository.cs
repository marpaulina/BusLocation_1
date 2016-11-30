using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLocation.Models
{
    interface IRepository :IDisposable
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

        #region Route
        IEnumerable<RouteModels> GetAllRoutes();
        #endregion

        #region Track
        IEnumerable<TrackModels> GetAllTracks();
        #endregion

        void Save();
    }
}
