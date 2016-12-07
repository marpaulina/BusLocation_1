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

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Miejscowość")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Szerokość geo")]
        public double Latitiude { get; set; } //Szerokość

        [Required]
        [Display(Name = "Długość geo")]
        public double Longitiude { get; set; } //Dlugosc
        
        public virtual List<TrackModels> Tracks { get; set; }

        public BusStopModels()
        {

        }
       public BusStopModels(string name, string city, double latitude, double longitiude)
        {
            Name = name;
            City = city;
            Latitiude = latitude;
            Longitiude = longitiude;
        }

        public void Update(BusStopModels busStop)
        {
            Name = busStop.Name;
            City = busStop.City;
            Latitiude = busStop.Latitiude;
            Longitiude = busStop.Longitiude;
        }

    }
}