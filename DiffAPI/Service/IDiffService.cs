using DiffAPI.Models;

namespace DiffAPI.Service
{
    /// <summary>
    /// Diff Service Interface
    /// Documentation on Implementation
    /// </summary>
    public interface IDiffService
    {
        DiffResult ProcessDiff(Json jsonById);
    }
}