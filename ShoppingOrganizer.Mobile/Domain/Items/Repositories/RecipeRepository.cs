using ShoppingOrganizer.Database;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Models.Items;
using System.Linq.Expressions;


namespace ShoppingOrganizer.Mobile.Domain.Items.Repositories;
public class RecipeRepository : IRecipeRepository
{
    public RecipeRepository(DatabaseHandler DatabaseHandler)
    {
        _DatabaseHandler = DatabaseHandler;
    }

    private DatabaseHandler _DatabaseHandler;

    public async Task<IEnumerable<Recipe>> GetCollection()
    {
        await _DatabaseHandler.Init();
        var dtoes = await _DatabaseHandler.Database.Table<RecipeEntity>().ToListAsync();

        return dtoes.Select(s => (Recipe)s);
    }

    public async Task<List<RecipePart>> GetRecipeParts(int recipeId)  //TODO move to recipePart repository. Pouzit GetByFilter
    {
        var query = _DatabaseHandler.Database.Table<RecipePartEntity>().Where(x => x.OwnerRecipeId == recipeId);
        var result = await query.ToListAsync();

        return result.Select(x => (RecipePart)x).ToList();
    }

    public async Task<List<Recipe>> GetWhere(Expression<Func<RecipeEntity, bool>> expression)
    {
        var query = _DatabaseHandler.Database.Table<RecipeEntity>().Where(expression);
        var result = await query.ToListAsync();

        return result.Select(s => (Recipe)s).ToList();
    }

    public Task<Recipe> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Expression<Func<RecipeEntity, bool>> expression)
    {
        var deleted = await _DatabaseHandler.Database.Table<RecipeEntity>().DeleteAsync(expression);
    }

    public async Task<List<Recipe>> GetByFilter(Expression<Func<RecipeEntity, bool>> ex)
    {
        await _DatabaseHandler.Init();
        var query = _DatabaseHandler.Database.Table<RecipeEntity>().Where(ex);
        List<RecipeEntity> result = await query.ToListAsync();

        return result.Select(s => (Recipe)s).ToList();
    }

    public Task<int> Update(IEnumerable<Recipe> entity)
    {
        throw new NotImplementedException();
    }
}