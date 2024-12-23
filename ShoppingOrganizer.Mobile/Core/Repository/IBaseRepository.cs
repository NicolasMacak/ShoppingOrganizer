﻿using System.Linq.Expressions;

namespace ShoppingOrganizer.Mobile.Core.Repository;

/// <summary>
/// Used by Source code generator to generate provided methods for each entity's repository
/// </summary>
/// <typeparam name="Model">Used for operation withing application</typeparam>
/// <typeparam name="Entity">Representation of the table structure</typeparam>
public interface IBaseRepository<Model, Entity> 
{
    /// <summary>
    /// Returns records that satisfy provided condition
    /// </summary>
    public Task<List<Model>> GetByFilter(Expression<Func<Entity, bool>> ex);
    /// <summary>
    /// Returns all records
    /// </summary>
    public Task<List<Model>> GetAll();
    /// <summary>
    /// Updates counterpart entities of provided models
    /// </summary>
    public Task Update(IEnumerable<Entity> entity);
    /// <summary>
    /// Deletes records that satisfy the condition
    /// </summary>
    /// <remarks>
    /// Does not support 'IN' SQL statement
    /// </remarks>
    public Task Delete(Expression<Func<Entity, bool>> expression);
}

