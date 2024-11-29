using SQLite;

namespace ShoppingOrganizer.Database.Entities.Items;

public class RecipeEntity
{
    public RecipeEntity() { }

    public RecipeEntity(int id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}