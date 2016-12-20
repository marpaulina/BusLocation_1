using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusLocation.Models
{
    public class TimeModels
    {
        [Key]
        public int Id { get; set; }

        public int value { get; set; }

        public virtual List<TrackModels> TracksList { get; set; }
    }
}