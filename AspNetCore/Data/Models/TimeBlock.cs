using AspNetCore.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace StoppedFishing.Data.Models
{
    public class TimeBlock
    {
        [Key]
        public int Id { get; set; }
        public int StartHour { get; set; }
        public int FinalHour { get; set; }
        public Days Day { get; set; }
    }
}
