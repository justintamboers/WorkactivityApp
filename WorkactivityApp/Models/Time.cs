using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkactivityApp.Models
{
    [Owned]
    public class Time
    {
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [NotMapped]
        [Display(Name = "Duration")]
        public TimeSpan Duration => EndTime - StartTime;
    }


}
