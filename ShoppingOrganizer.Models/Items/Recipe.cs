namespace ShoppingOrganizer.Models.Items;
public class Recipe
{
    public Recipe() { }

    public Recipe(
        int id,
        string title,
        string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<RecipePart>? RecipesParts { get; set; }
}

