using System.Threading.Tasks;
using DiffAPI.Models;

namespace DiffAPI.Repository
{
    public interface IRepository
    {
        Task<Json> GetById(string id);
        bool AddOrUpdate(Json obj);
        Task SaveJson(string id, string json, Side side);
        Task SaveChanges();
    }
}