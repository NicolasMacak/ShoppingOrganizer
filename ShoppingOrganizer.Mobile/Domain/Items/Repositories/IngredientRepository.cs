﻿using ShoppingOrganizer.Database;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Models.Items;
using System.Linq.Expressions;

namespace ShoppingOrganizer.Mobile.Domain.Items.Repositories;
public class IngredientRepository : IIngredientRepository
{
    public IngredientRepository(DatabaseHandler databaseHandler)
    {
        _databaseHandler = databaseHandler;
    }

    private DatabaseHandler _databaseHandler;

    public Task<Ingredient> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Ingredient>> GetCollection()
    {
        await _databaseHandler.Init();
        var dtoes = await _databaseHandler.Database.Table<IngredientEntity>().ToListAsync();

        return dtoes.Select(s => (Ingredient)s);
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Expression<Func<IngredientEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<List<Ingredient>> GetByFilter(Expression<Func<IngredientEntity, bool>> ex)
    {
        throw new NotImplementedException();
    }

    public Task<int> Update(IEnumerable<Ingredient> entity)
    {
        throw new NotImplementedException();
    }
}