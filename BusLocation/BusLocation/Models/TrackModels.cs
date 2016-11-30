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
        public String NameTrack { get; set; }
        [Required]
        public List<Int32> TimeToNextStop { get; set; }
        public List<BusStopModels> BusStops { get; set; }
       

    }
}