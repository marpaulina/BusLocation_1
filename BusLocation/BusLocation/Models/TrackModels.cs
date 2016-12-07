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
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa trasy")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Błędna nazwa trasy.")]
        public String NameTrack { get; set; }
      
        [Display(Name = "Czas do nastepnego przystanku")]
        public List<int> TimeToNextStop { get; set; }

        [Display(Name = "Przystanki")]
        public virtual List<BusStopModels> BusStops { get; set; }

        [Required]
        [Display(Name = "Przystanek")]
        public int BusStopId { get; set; }

        [Required]
        [Display(Name = "Czas do nastepnego przystanku")]
        public int TimeToNext { get; set; }


        Repository repo = new Repository();
        public TrackModels()
        {
            //empty
        }

        public TrackModels(string nameTrack, int busStop, int timeToNext)
        {
            NameTrack = nameTrack;
            TimeToNextStop = new List<int>();
            TimeToNextStop.Add(timeToNext);
            BusStops = new List<BusStopModels>();
            BusStops.Add(repo.GetBusStopByID(busStop));
        }

        public void Update(TrackModels track)
        {
            NameTrack = track.NameTrack;
            TimeToNextStop = track.TimeToNextStop;
            BusStops = track.BusStops;
        }

        public void Update(TrackModels track, List<BusStopModels> b, List<int> time)
        {
            BusStops = b;
            TimeToNextStop = time;
        }

    }
}