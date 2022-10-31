using Apis.Models;
using Microsoft.AspNetCore.Mvc;
using SharedViewModels;

namespace Apis.Repository;

public interface ICategoryRepository : IDisposable
{
    Task<IEnumerable<Category>> GetAll();
    void Create(Category category);
    Task<Category?> Get(int id);
    void Delete(int id);
    void Update(Category category);
    Task SaveAsync();
    bool IsExisted(int id);
}
