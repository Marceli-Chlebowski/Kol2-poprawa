using WebApplication1.Models.WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IObjectRepository
{
    Task<OwnedObject> GetObjectAsync(int id);
    Task AddObjectAsync(OwnedObject obj);
}
