using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkactivityApp.Models
{
    [Owned]
    public class Time
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public List<AddedTime> AddedTimes { get; set; } = new();

        [NotMapped]
        public TimeSpan InitialDuration => EndTime - StartTime;

        [NotMapped]
        public TimeSpan Duration => InitialDuration + TimeSpan.FromTicks(AddedTimes.Sum(a => a.Duration.Ticks));
    }
}