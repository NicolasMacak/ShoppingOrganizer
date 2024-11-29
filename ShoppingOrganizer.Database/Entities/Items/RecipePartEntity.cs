using SQLite;

namespace ShoppingOrganizer.Database.Entities.Items;
/// <summary>
/// Reference to either recipe or a ingredient
/// </summary>
public class RecipePartEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int OwnerRecipeId {  get; set; }
    public int? RecipeId { get; set; }
    public int? IngredientId { get; set; }
    public string Title { get; set; }
    public double Quantity {  get; set; }
    public string? Description { get; set; }
}
