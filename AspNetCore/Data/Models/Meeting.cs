using System.ComponentModel.DataAnnotations;


namespace AspNetCore.Data.Models
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }    
        public ICollection<User> Users { get; set; }
    }
}
