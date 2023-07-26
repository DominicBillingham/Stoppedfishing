using System.ComponentModel.DataAnnotations;
using AspNetCore.Data.Enums;

namespace AspNetCore.Data.Models
{
    public class SimpleTimeBlock
    {
        [Key]
        public int Id { get; set; }
        public SimpleBlocks SimpleBlock { get; set; }
        public Days Day { get; set; }

    }
}
