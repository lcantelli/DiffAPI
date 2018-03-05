using System.Linq;
using System.Threading.Tasks;
using DiffAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiffAPI.Repository
{
    /// <summary>
    /// Data Access Layer
    /// Repository of Json object
    /// </summary>
    public class JsonRepository : IJsonRepository
    {
        private readonly DatabaseContext _db;
        private readonly DbSet<Json> _dbSet;

        public JsonRepository(DatabaseContext db)
        {
            _db = db;
            _dbSet = db.Set<Json>();
        }

        //Get JSON By ID
        public async Task<Json> GetById(string id)
        {
            IQueryable<Json> query = _dbSet;
            var result = await query.FirstOrDefaultAsync(x => x.JsonId == id);
            return result;
        }

        //Updates if ID exists, otherwise Add to database
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

        //Builds JSON object and saves into database
        public async Task SaveJson(string id, string json, Side side)
        {
            var jsonById = await GetById(id) ?? new Json();

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
        
        /// <summary>
        /// Apply database changes asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}