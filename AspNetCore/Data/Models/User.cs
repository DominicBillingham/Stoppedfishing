using Microsoft.AspNetCore.Identity;
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
        public ICollection<SimpleTimeBlock> TimeBlock { get; set; }
        public ICollection<Meeting> Meetings { get; set; }

    }
}
