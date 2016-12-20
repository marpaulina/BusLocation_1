using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public List<TimeModels> TimeToNextStopsList { get; set; }

        [Display(Name = "Przystanki")]
        public virtual List<BusStopModels> BusStopsList { get; set; }

        [Required]
        [Display(Name = "Przystanek")]
        public int BusStopId { get; set; }

        [Required]
        [Display(Name = "Czas do nastepnego przystanku")]
        public int TimeId { get; set; }


        Repository repo = new Repository();
        public TrackModels()
        {
            //empty
        }

        public TrackModels(string nameTrack, int busStop, int time)
        {
            NameTrack = nameTrack;
            TimeToNextStopsList = new List<TimeModels>();
            TimeToNextStopsList.Add(repo.GetTimeByID(time));
            BusStopsList = new List<BusStopModels>();
            BusStopsList.Add(repo.GetBusStopByID(busStop));
        }

        public void Update(TrackModels track)
        {
            NameTrack = track.NameTrack;
            TimeToNextStopsList = track.TimeToNextStopsList;
            BusStopsList = track.BusStopsList;
        }

        public void Update(TrackModels track, List<BusStopModels> b, List<TimeModels> time)
        {
            BusStopsList = b;
            TimeToNextStopsList = time;
        }

    }
}