using Microsoft.AspNetCore.Identity;
using StoppedFishing.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string DisplayName { get; set; }

        public ICollection<SimpleTimeBlock> SimpleBlocks { get; set; }
        public ICollection<TimeBlock> TimeBlocks { get; set; }
        public ICollection<Meeting> Meetings { get; set; }

    }
}
