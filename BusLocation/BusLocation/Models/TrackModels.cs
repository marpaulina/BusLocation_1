using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusLocation.Models
{
    public class TrackModels
    {
        [Key]
        public String Name { get; set; }
        public List<BusStopModels> BusStops { get; set; }
        public List<Int32> TimeToNextStop { get; set; }

    }
}