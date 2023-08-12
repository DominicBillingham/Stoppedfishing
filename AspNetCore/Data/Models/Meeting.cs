using System.ComponentModel.DataAnnotations;


namespace AspNetCore.Data.Models
{
    public class Meeting
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        [Range(0, 23)]
        public int startHour { get; set; }
        [Range(0, 23)]
        public int endHour { get; set; }
        public ICollection<User> Users { get; set; }
        public User Owner { get; set; }
    }
}
