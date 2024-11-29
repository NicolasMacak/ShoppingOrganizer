using ShoppingOrganizer.Database.Entities.Items;

namespace ShoppingOrganizer.Models.Items;
public class RecipePart
{
    public int Id { get; set; }
    public int OwnerRecipeId {  get; set; }
    public int? RecipeId { get; set; }
    public int? Ingredientid { get; set; }
    public string Title { get; set; }
    public double Quantity {  get; set; }
    public string? Description { get; set; }

    public static explicit operator RecipePart(RecipePartEntity dto)
    {
        return new RecipePart()
        {
            Id = dto.Id,
            OwnerRecipeId = dto.OwnerRecipeId,
            Description = dto.Description,
            Ingredientid = dto.IngredientId,
            RecipeId = dto.RecipeId,
            Quantity = dto.Quantity,
            Title = dto.Title
        };
    }
}
