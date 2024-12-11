using AutoMapper;
using MetroLog;
using Microsoft.Extensions.Logging;
using ShoppingOrganizer.Database;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Mobile.Core.Repository;
using ShoppingOrganizer.Models.Items;
using System.Reflection;

namespace ShoppingOrganizer.Mobile.Domain.Items.Repositories;
public class RecipePartRepository : BaseRepository<RecipePart, RecipePartEntity>, IRecipePartRepository
{
    public RecipePartRepository(DatabaseHandler databaseHandler, IMapper mapper, ILogger<BaseRepository<RecipePart, RecipePartEntity>> logger): base(databaseHandler, mapper, logger)
    {
    }
}
