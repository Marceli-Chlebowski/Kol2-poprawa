namespace WebApplication1.Models
{
    namespace WebApplication1.Models
    {
        public class OwnedObject
        {
            public int Id { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public string Type { get; set; }

            public int OwnerId { get; set; }
            public Owner Owner { get; set; }
        }
    }
}

