using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusLocation.Models
{
    public class TrackModels
    {
        [Key]
        [Required]
        public String NameTrack { get; set; }
      
        [Display(Name = "Czas do nastepnego przystanku")]
        public List<int> TimeToNextStop { get; set; }
      
        public List<BusStopModels> BusStops { get; set; }

        [Required]
        [Display(Name = "Bus Stops")]
        public int BusStopId { get; set; }

        [Required]
        [Display(Name = "Czas do nastepnego przystanku")]
        public int TimeToNext { get; set; }


        Repository repo = new Repository();
        TrackModels()
        {
            //empty
        }

        TrackModels(string nameTrack, int busStop, int timeToNext)
        {
            NameTrack = nameTrack;
            TimeToNextStop.Add(timeToNext);
            BusStops.Add(repo.GetBusStopByID(busStop));
        }
        //TrackModels(string nameTrack, List<int> timeToNeztStop, List<BusStopModels> busStops)
        //{
        //    NameTrack = nameTrack;
        //    TimeToNextStop = timeToNeztStop;
        //    BusStops = busStops;
        //}
        //TrackModels(string nameTrack, int timeToNeztStop, BusStopModels busStops)
        //{
        //    NameTrack = nameTrack;
        //    TimeToNextStop.Add(timeToNeztStop);
        //    BusStops.Add(busStops);
        //}
        //TrackModels(string nameTrack, byte timeToNeztStop, string busStopName)
        //{
        //    NameTrack = nameTrack;
        //    TimeToNextStop.Add(timeToNeztStop);
        //    BusStops.Add(repo.GetBusStopByName(busStopName));
        //}
        //TrackModels(string nameTrack, string busStopName, byte timeToNeztStop)
        //{
        //    NameTrack = nameTrack;
        //    TimeToNextStop.Add(timeToNeztStop);
        //    BusStops.Add(repo.GetBusStopByName(busStopName));
        //}
        public void Update(TrackModels track)
        {
            NameTrack = track.NameTrack;
            TimeToNextStop = track.TimeToNextStop;
            BusStops = track.BusStops;
        }

    }
}