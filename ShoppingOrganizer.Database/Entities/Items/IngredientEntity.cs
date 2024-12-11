using SQLite;

namespace ShoppingOrganizer.Database.Entities.Items;

public class IngredientEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty; // TODO can db hold enum?
}