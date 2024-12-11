using AutoMapper;
using Microsoft.Extensions.Logging;
using ShoppingOrganizer.Database;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Mobile.Core.Repository;
using ShoppingOrganizer.Models.Items;

namespace ShoppingOrganizer.Mobile.Domain.Items.Repositories;
public class RecipeRepository : BaseRepository<Recipe, RecipeEntity>, IRecipeRepository
{

    public RecipeRepository(DatabaseHandler databaseHandler, IMapper mapper, ILogger<BaseRepository<Recipe, RecipeEntity>> logger) : base(databaseHandler, mapper, logger)
    {
    }
}