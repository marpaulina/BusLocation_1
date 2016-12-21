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
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Błędna nazwa przystanku (tylko litery).")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Miejscowość")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Błędna miejscowość (tylko litery).")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Szerokość geo")]
        [Range(0.001, float.MaxValue, ErrorMessage = "Błędna szerokość geograficzna.")]
        public float Latitiude { get; set; } //Szerokość

        [Required]
        [Display(Name = "Długość geo")]
        [Range(0.001, float.MaxValue, ErrorMessage = "Błędna długość geograficzna.")]
        public float Longitiude { get; set; } //Dlugosc
        public virtual List<TrackModels> Tracks { get; set; }

        public BusStopModels()
        {

        }
       public BusStopModels(string name, string city, float latitude, float longitiude)
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