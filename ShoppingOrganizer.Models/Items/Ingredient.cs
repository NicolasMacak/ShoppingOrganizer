using ShoppingOrganizer.Database.Entities.Items;

namespace ShoppingOrganizer.Models.Items;
public class Ingredient
{
    public int Id { get; set; }
 
    public string Title { get; set; }

    public string Unit {  get; set; }

    public static explicit operator Ingredient(IngredientEntity ingredientEntity)
    {
        return new Ingredient() {
            Id = ingredientEntity.Id,
            Title = ingredientEntity.Title,
            Unit = "Dummy"
        };
    }
}

