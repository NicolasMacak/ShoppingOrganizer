using SQLite;

namespace ShoppingOrganizer.Database.Entities.Items;

public class RecipeEntity
{
    [PrimaryKey, AutoIncrement]

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}