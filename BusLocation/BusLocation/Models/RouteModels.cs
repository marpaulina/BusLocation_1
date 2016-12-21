using System;
using System.ComponentModel.DataAnnotations;

namespace BusLocation.Models
{
    public class RouteModels
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Trasa")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Błędna nazwa trasy (tylko litery).")]
        public virtual TrackModels  Track { get; set; }

        [Display(Name = "Trasa")]
        public int TrackId { get; set; }

        [Display(Name = "Godzina odjazdu")]
        [DataType(DataType.Time, ErrorMessage ="Błędna godzina (np 11:10).")]
        public TimeSpan StartTime { get; set; }

        Repository repo = new Repository();

        public RouteModels()
        {
            //empty
        }

        public RouteModels(TimeSpan startTime, int trackId)
        {
            Track = repo.GetTrackByID(trackId);
            StartTime = startTime;
        }
        public RouteModels(RouteModels route)
        {
            if (route.Track.Equals(null))
            {
                Track = repo.GetTrackByID(route.TrackId);
            }
            else
            {
                Track = route.Track;
            }
            StartTime = route.StartTime;
        }

        public void Update(TimeSpan startTime, int trackId)
        {
            Track = repo.GetTrackByID(trackId);
            StartTime = startTime;
        }
        public  void Update(RouteModels route)
        {
            Track = repo.GetTrackByID(route.TrackId);
            StartTime = route.StartTime;
        }
    }
}