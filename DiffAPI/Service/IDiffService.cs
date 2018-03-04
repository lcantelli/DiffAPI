using DiffAPI.Models;

namespace DiffAPI.Service
{
    public interface IDiffService
    {
        DiffResult ProcessDiff(Json jsonById);
    }
}