using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.WebApplication1.Models;

namespace WebApplication1.Models;

public class Object_Type
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<OwnedObject> Objects { get; set; }
}
