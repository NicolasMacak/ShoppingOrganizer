using AutoMapper;
using Microsoft.Extensions.Logging;
using ShoppingOrganizer.Database;
using System.Linq.Expressions;

namespace ShoppingOrganizer.Mobile.Core.Repository;
public class BaseRepository<Model, Entity> : IBaseRepository<Model, Entity> where Entity : new()
{
    protected DatabaseHandler _databaseHandler;
    protected IMapper _mapper;
    
    private readonly ILogger<BaseRepository<Model, Entity>> _logger;

    /// <inheritdoc/>
    public BaseRepository(DatabaseHandler database, IMapper mapper, ILogger<BaseRepository<Model, Entity>> logger)
    {
        _databaseHandler = database;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task Delete(Expression<Func<Entity, bool>> expression)
    {
        await _databaseHandler.Init();

        try
        {
            await _databaseHandler.Database.Table<Entity>().DeleteAsync(expression);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Database operation was not successfull");
        }
    }

    /// <inheritdoc/>
    public async Task<List<Model>> GetAll()
    {
        await _databaseHandler.Init();
        List<Entity> result = new();

        try
        {
            result = await _databaseHandler.Database.Table<Entity>().ToListAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Database operation was not successfull");
        }

        return _mapper.Map<List<Model>>(result);
    }

    /// <inheritdoc/>
    public async Task<List<Model>> GetByFilter(Expression<Func<Entity, bool>> expression)
    {
        await _databaseHandler.Init();
        List<Entity> result = new();

        try
        {
            result = await _databaseHandler.Database
              .Table<Entity>()
              .Where(expression)
              .ToListAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Database operation was not successfull");
        }

        return _mapper.Map<List<Model>>(result);
    }

    /// <inheritdoc/>
    public async Task Update(IEnumerable<Entity> entities)
    {
        await _databaseHandler.Init();

        try
        {
            await _databaseHandler.Database.InsertAllAsync(entities);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Database operation was not successfull");
        }
    }
}
