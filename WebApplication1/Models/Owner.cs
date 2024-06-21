namespace WebApplication1.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Object_Owner> Object_Owners { get; set; } // Navigation property
    }
}

