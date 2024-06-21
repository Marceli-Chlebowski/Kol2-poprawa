using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Object_Owner
    {
        [Key]
        public int ObjectId { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Type { get; set; }
        public int OwnerId { get; set; } 
        public Owner Owner { get; set; } 
    }
}

