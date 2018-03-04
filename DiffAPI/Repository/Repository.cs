using System.Linq;
using System.Threading.Tasks;
using DiffAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiffAPI.Repository
{
    public class Repository : IRepository
    {
        private readonly DatabaseContext _db;
        private readonly DbSet<Json> _dbSet;

        public Repository(DatabaseContext db)
        {
            _db = db;
            _dbSet = db.Set<Json>();
        }

        public async Task<Json> GetById(string id)
        {
            IQueryable<Json> query = _dbSet;
            var result = await query.FirstOrDefaultAsync(x => x.JsonId == id);
            return result;
        }

        public bool AddOrUpdate(Json obj)
        {
            if (_dbSet.Any(x => x.JsonId == obj.JsonId))
            {
                _dbSet.Update(obj);
            }
            else
            {
                _dbSet.Add(obj);
            }

            return true;
        }

        public async Task SaveJson(string id, string json, Side side)
        {
            var jsonById = await GetById(id);

            if (side == Side.Left)
            {
                jsonById.Left = json;
            }
            else
            {
                jsonById.Right = json;
            }

            jsonById.JsonId = id;

            AddOrUpdate(jsonById);

            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}