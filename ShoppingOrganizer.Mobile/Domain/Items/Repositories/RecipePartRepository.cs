using AutoMapper;
using ShoppingOrganizer.Database;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Models.Items;
using System.Linq.Expressions;

namespace ShoppingOrganizer.Mobile.Domain.Items.Repositories;
public class RecipePartRepository : IRecipePartRepository
{

    private readonly DatabaseHandler _databaseHandler;
    private readonly IMapper _mapper;

    public RecipePartRepository(DatabaseHandler databaseHandler, IMapper mapper) {
        _databaseHandler = databaseHandler;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<List<RecipePart>> GetAll()
    {
        await _databaseHandler.Init();
        List<RecipePartEntity> result = await _databaseHandler.Database.Table<RecipePartEntity>().ToListAsync();

        return _mapper.Map<List<RecipePart>>(result);
    }

    /// <inheritdoc/>
    public Task<int> Update(IEnumerable<RecipePart> entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task Delete(Expression<Func<RecipePartEntity, bool>> expression)
    {
        return _databaseHandler.Database.Table<RecipePartEntity>().DeleteAsync(expression);
    }

    /// <inheritdoc/>
    public async Task<List<RecipePart>> GetByFilter(Expression<Func<RecipePartEntity, bool>> ex)
    {
        List<RecipePartEntity> result = await _databaseHandler.Database
            .Table<RecipePartEntity>()
            .Where(ex)
            .ToListAsync();

        return _mapper.Map<List<RecipePart>>(result);
    }
}
