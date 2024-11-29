using System.Linq.Expressions;

namespace ShoppingOrganizer.Mobile.Core.Interfaces;

/// <summary>
/// Encapsulates base database operations
/// </summary>
/// <typeparam name="Model"></typeparam>
public interface IBaseRepository<Model, Entity> 
{
    public Task<Model> GetById(int id);
    public Task<IEnumerable<Model>> GetCollection();
    public Task<int> Update(IEnumerable<Model> entity);
    public Task Delete(Expression<Func<Entity, bool>> expression);
    public Task<List<Model>> GetByFilter(Expression<Func<Entity, bool>> ex);
}

