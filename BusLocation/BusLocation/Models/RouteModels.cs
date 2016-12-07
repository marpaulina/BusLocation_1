using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusLocation.Models
{
    public class RouteModels
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Trasa")]
        public TrackModels  Track { get; set; }

        [Display(Name = "Czas połączenia")]
        public TimeSpan StartTime { get; set; }
    }
}