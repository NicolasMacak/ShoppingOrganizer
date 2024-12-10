using AutoMapper;
using ShoppingOrganizer.Database;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Models.Items;
using System.Linq.Expressions;


namespace ShoppingOrganizer.Mobile.Domain.Items.Repositories;
public class RecipeRepository : IRecipeRepository
{
    private readonly DatabaseHandler _databaseHandler;
    private readonly IMapper _mapper;

    public RecipeRepository(DatabaseHandler databaseHandler, IMapper mapper)
    {
        _databaseHandler = databaseHandler;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<List<Recipe>> GetAll()
    {
        await _databaseHandler.Init();
        List<RecipeEntity> result = await _databaseHandler.Database.Table<RecipeEntity>().ToListAsync();

        return _mapper.Map<List<Recipe>>(result);
    }

    /// <inheritdoc/>
    public async Task Delete(Expression<Func<RecipeEntity, bool>> expression)
    {
        await _databaseHandler.Database.Table<RecipeEntity>().DeleteAsync(expression);
    }

    /// <inheritdoc/>
    public async Task<List<Recipe>> GetByFilter(Expression<Func<RecipeEntity, bool>> ex)
    {
        await _databaseHandler.Init();

        List<RecipeEntity> result = await _databaseHandler.Database
            .Table<RecipeEntity>()
            .Where(ex)
            .ToListAsync();

        return _mapper.Map<List<Recipe>>(result);
    }

    /// <inheritdoc/>
    public Task<int> Update(IEnumerable<Recipe> entity)
    {
        throw new NotImplementedException();
    }
}