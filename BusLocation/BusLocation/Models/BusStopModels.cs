using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusLocation.Models
{
    public class BusStopModels
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String City { get; set; }
        public double Latitiude { get; set; } //Szerokość
        public double Longitiude { get; set; } //Dlugosc

    }
}