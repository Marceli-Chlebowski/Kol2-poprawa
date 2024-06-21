using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Threading.Tasks;
using WebApplication1.Models.WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ObjectRepository : IObjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ObjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OwnedObject> GetObjectAsync(int id)
        {
            return await _context.OwnedObjects
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddObjectAsync(OwnedObject obj)
        {
            _context.OwnedObjects.Add(obj);
            await _context.SaveChangesAsync();
        }
    }
}