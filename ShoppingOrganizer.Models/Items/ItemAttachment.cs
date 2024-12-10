namespace ShoppingOrganizer.Models.Items;
public class ItemAttachment
{
    public enum AttachmentState
    {
        AlreadyAttached,
        Removed,
        New,
        Ignored
    }
    public AttachmentState Attachment { get; set; }

    public int? RecipeId { get; set; }

    public int? IngredientId { get; set; }

    public string Title { get; set; } = string.Empty;
}
