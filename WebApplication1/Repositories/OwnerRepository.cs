using Dapper;
using Microsoft.Data.SqlClient;
using WebApplication1.DTO_s;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace WebApplication1.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IConfiguration _config;
 
        public OwnerRepository(IConfiguration config)
        {
            _config = config;
        }
 
        public async Task<bool> CheckOwnerExists(int id)
        {
            const string sql = "SELECT 1 FROM owners WHERE Id = @Id";
            await using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            return await connection.ExecuteScalarAsync<bool>(sql, new { Id = id });
        }
 
        public async Task<OwnerDto> GetOwner(int id)
        {
            const string ownerSql = @"SELECT o.Id, o.FirstName, o.LastName, o.PhoneNumber 
                                      FROM owners o 
                                      WHERE o.Id = @Id";
 
            const string objectsSql = @"SELECT ob.Id, ob.Width, ob.Height, 
                                               ot.Name AS Type, 
                                               w.Name AS Warehouse 
                                        FROM objects ob 
                                        JOIN object_owners oo ON ob.Id = oo.ObjectId 
                                        JOIN object_types ot ON ob.ObjectTypeId = ot.Id 
                                        JOIN warehouses w ON ob.WarehouseId = w.Id 
                                        WHERE oo.OwnerId = @OwnerId";
 
            await using var connection = new SqlConnection(_config.GetConnectionString("Default"));
 
            var owner = await connection.QueryFirstOrDefaultAsync<OwnerDto>(ownerSql, new { Id = id });
 
            if (owner != null)
            {
                var objects = await connection.QueryAsync<OwnedObjectDto>(objectsSql, new { OwnerId = id });
                owner.OwnerObjects = objects.ToList();
            }
 
            return owner;
        }
 
        public async Task<bool> CheckObjectExists(int id)
        {
            const string sql = "SELECT 1 FROM objects WHERE Id = @Id";
            await using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            return await connection.ExecuteScalarAsync<bool>(sql, new { Id = id });
        }
 
        public async Task<OwnerDto> CreateOwner(NewOwnerDto newOwnerDto)
        {
            const string insertOwnerSql = "INSERT INTO owners (FirstName, LastName, PhoneNumber) OUTPUT INSERTED.Id VALUES (@FirstName, @LastName, @PhoneNumber)";
            const string insertObjectOwnerSql = "INSERT INTO object_owners (ObjectId, OwnerId) VALUES (@ObjectId, @OwnerId)";
 
            await using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();
 
            try
            {
                var ownerId = await connection.ExecuteScalarAsync<int>(insertOwnerSql, new
                {
                    newOwnerDto.FirstName,
                    newOwnerDto.LastName,
                    newOwnerDto.PhoneNumber
                }, transaction);
 
                foreach (var objectId in newOwnerDto.OwnerObjects)
                {
                    await connection.ExecuteAsync(insertObjectOwnerSql, new
                    {
                        ObjectId = objectId,
                        OwnerId = ownerId
                    }, transaction);
                }
 
                await transaction.CommitAsync();
 
                return await GetOwner(ownerId);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}