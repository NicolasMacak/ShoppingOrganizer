using AutoMapper;
using ShoppingOrganizer.Database;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Models.Items;
using System.Linq.Expressions;

namespace ShoppingOrganizer.Mobile.Domain.Items.Repositories;
public class IngredientRepository : IIngredientRepository
{
    private readonly DatabaseHandler _databaseHandler;
    private readonly IMapper _mapper;

    public IngredientRepository(DatabaseHandler databaseHandler, IMapper mapper)
    {
        _databaseHandler = databaseHandler;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<List<Ingredient>> GetAll()
    {
        await _databaseHandler.Init();
        List<IngredientEntity> result = await _databaseHandler.Database.Table<IngredientEntity>().ToListAsync();

        return _mapper.Map<List<Ingredient>>(result);
    }

    /// <inheritdoc/>
    public Task Delete(Expression<Func<IngredientEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<List<Ingredient>> GetByFilter(Expression<Func<IngredientEntity, bool>> ex)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<int> Update(IEnumerable<Ingredient> entity)
    {
        throw new NotImplementedException();
    }
}