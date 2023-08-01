using AspNetCore.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace StoppedFishing.Data.Models
{
    public class HourBlock
    {
        public int Hour { get; set; }
        public Days Day { get; set; }
    }
}
