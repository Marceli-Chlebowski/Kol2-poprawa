namespace WebApplication1.Models
{
    public class Object_Owner
    {
        public int ObjectId { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Type { get; set; }

        public int OwnerId { get; set; } // Foreign key
        public Owner Owner { get; set; } // Navigation property
    }
}

