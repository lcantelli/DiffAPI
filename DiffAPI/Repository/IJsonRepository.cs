using System.Threading.Tasks;
using DiffAPI.Models;

namespace DiffAPI.Repository
{
    /// <summary>
    /// Repository Interface
    /// Documentation on Implementation
    /// </summary>
    public interface IJsonRepository
    {
        Task<Json> GetById(string id);
        bool AddOrUpdate(Json obj);
        Task SaveJson(string id, string json, Side side);
        Task SaveChanges();
    }
}