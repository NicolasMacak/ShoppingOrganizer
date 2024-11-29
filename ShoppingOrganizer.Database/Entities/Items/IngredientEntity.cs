using SQLite;

namespace ShoppingOrganizer.Database.Entities.Items;

public class IngredientEntity
{
    public IngredientEntity() { }

    public IngredientEntity(int id, string title) // todo toRemove
    {
        Id = id;
        Title = title;
    }

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Unit { get; set; } // TODO can db hold enum?
}