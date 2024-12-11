using ShoppingOrganizer.Mobile.Core.Repository;
using ShoppingOrganizer.Models.Items;
using ShoppingOrganizer.Database.Entities.Items;

namespace ShoppingOrganizer.Mobile.Domain.Items.Repositories;
public interface IIngredientRepository : IBaseRepository<Ingredient, IngredientEntity>
{
}

