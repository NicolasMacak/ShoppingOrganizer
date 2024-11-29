using ShoppingOrganizer.Database;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Models.Items;
using SQLite;
using System.Linq.Expressions;

namespace ShoppingOrganizer.Mobile.Domain.Items.Repositories;
public class RecipePartRepository : IRecipePartRepository
{
    public RecipePartRepository(DatabaseHandler DatabaseHandler) {
        _DatabaseHandler = DatabaseHandler;
    }

    private readonly DatabaseHandler _DatabaseHandler;

    public Task<RecipePart> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<RecipePart>> GetCollection()
    {
        await _DatabaseHandler.Init();
        var dtoes = await _DatabaseHandler.Database.Table<RecipePartEntity>().ToListAsync();

        return dtoes.Select(s => (RecipePart)s);
    }

    public Task<int> Update(IEnumerable<RecipePart> entity)
    {
        throw new NotImplementedException();
        //List<RecipePart>

        //return _DatabaseHandler.Database.Inse
    }

    public Task Delete(Expression<Func<RecipePartEntity, bool>> expression)
    {
        return _DatabaseHandler.Database.Table<RecipePartEntity>().DeleteAsync(expression);
    }

    public async Task<List<RecipePart>> GetByFilter(Expression<Func<RecipePartEntity, bool>> ex)
    {
        AsyncTableQuery<RecipePartEntity> query = _DatabaseHandler.Database.Table<RecipePartEntity>().Where(ex);
        List<RecipePartEntity> result = await query.ToListAsync();

        return result.Select(s => (RecipePart)s).ToList();
    }
}
