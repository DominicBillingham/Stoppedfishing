using System.ComponentModel.DataAnnotations;


namespace AspNetCore.Data.Models
{
    public class Meeting
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        [Range(0, 23)]
        public int StartHour { get; set; }
        [Range(0, 23)]
        public int EndHour { get; set; }

        public DateTimeOffset StartDay {get; set;}
        public DateTimeOffset EndDay { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
