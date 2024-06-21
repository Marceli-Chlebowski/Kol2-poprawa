using WebApplication1.DTO_s;

namespace WebApplication1.Repositories;

public interface IOwnerRepository
{
    Task<bool> CheckOwnerExists(int ownerId);
    Task<OwnerDto> GetOwner(int ownerId);
    Task<bool> CheckObjectExists(int objectId);
    Task<OwnerDto> CreateOwner(NewOwnerDto ownerDetails);
}
