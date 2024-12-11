namespace ShoppingOrganizer.Models.Items;
public class AttachedItem
{
    public AttachedItem(bool attachedInitially, string title)
    {
        AttachedInitially = attachedInitially;
        AttachedCurrently = attachedInitially;
        State = attachedInitially ? AttachmentState.AttachedInitially : AttachmentState.Ignored;
        Title = title;
    }

    /// <summary>
    /// Used in ColorConverter to vissualy differentiate between attachment states
    /// </summary>
    public enum AttachmentState
    {
        AttachedInitially, // Already attached at the start of attachment process
        Removed, // Attached initialy, but removed
        New, // Not attached initially, but added
        Ignored // Not attached initially and not added
    }

    public AttachmentState State { get; set; }

    public bool AttachedInitially {  get; set; }

    public bool AttachedCurrently { get; set; }

    public int? RecipeId { get; set; } // todo. Replace with IsRecipe?

    public int? IngredientId { get; set; }

    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// <see cref="AttachedItem.AttachedCurrently"/> of this item will be reverted and <see cref="AttachmentState"/> updated accordingly 
    /// </summary>
    public void ToggleAttachmentState()
    {
        AttachedCurrently = !AttachedCurrently;

        State = (AttachedInitially, AttachedCurrently) switch
        {
            (true, true) => AttachmentState.AttachedInitially,
            (false, true) => AttachmentState.New,
            (true, false) => AttachmentState.Removed,
            _ => AttachmentState.Ignored
        };
    }

    /// <summary>
    /// <see cref="AttachedCurrently"/> is set to <see cref="AttachedInitially"/> and <see cref="State"/> is set to the <see cref="AttachmentState.AttachedInitially"/>
    /// </summary>
    public void ResetToInitialState()
    {
        State = AttachedInitially ? AttachmentState.AttachedInitially : AttachmentState.Ignored;
        AttachedCurrently = AttachedInitially;
    }
}
