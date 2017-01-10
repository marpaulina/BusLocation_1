using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusLocation.Models
{
    public class UserRequestModels
    {
        [Key]
        public int Id { get; set; }
        public string UserCity { get; set; }
        public string UserBusStopName { get; set; }
        public Boolean DriversGetRequest { get; set; }
        public string Message { get; set; }
        public virtual DriverModels Driver { get; set; }
        public TimeSpan TimeAddRequest { get; set; }

        Repository repo = new Repository();

        public UserRequestModels()
        {
            //Empty
        }
        public UserRequestModels(int driverID, string city, string busStopName)
        {
            DriversGetRequest = false;
            TimeAddRequest = DateTime.Now.TimeOfDay;
            Driver = repo.GetDriverByID(driverID);
            UserCity = city;
            UserBusStopName = busStopName;
            Message = "";
        }

        public void Update(string message)
        {
            Message = message;
        }
        public void Update()
        {
           DriversGetRequest = true;
        }
    }
}