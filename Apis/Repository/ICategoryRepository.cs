using Apis.Models;

namespace Apis.Repository;

public interface ICategoryRepository : IDisposable
{
    Task<IEnumerable<Category>> GetAll();
}
