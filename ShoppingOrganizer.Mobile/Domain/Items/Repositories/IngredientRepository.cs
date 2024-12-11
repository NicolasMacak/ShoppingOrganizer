using AutoMapper;
using Microsoft.Extensions.Logging;
using ShoppingOrganizer.Database;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Mobile.Core.Repository;
using ShoppingOrganizer.Models.Items;

namespace ShoppingOrganizer.Mobile.Domain.Items.Repositories;
public class IngredientRepository : BaseRepository<Ingredient, IngredientEntity>, IIngredientRepository
{
    public IngredientRepository(DatabaseHandler databaseHandler, IMapper mapper, ILogger<BaseRepository<Ingredient, IngredientEntity>> logger) : base(databaseHandler, mapper, logger)
    {
    }
}