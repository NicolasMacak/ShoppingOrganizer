using ShoppingOrganizer.Models.Items;
using System.Collections.ObjectModel;
using static ShoppingOrganizer.Models.Items.AttachedItem;

namespace ShoppingOrganizer.Mobile.Domain.Items.Extensions;
internal static class ObservableCollectionExtensions
{
    /// <summary>
    /// Updates item the collection
    /// </summary>
    /// <remarks>
    /// Prior to this, there was undesired behavior. Such as adding updated behavior at the start of the collection
    /// </remarks>
    public static void UpdateItem<T>(this ObservableCollection<T> collection, T item)
    {
        int index = collection.IndexOf(item);
        collection[index] = item;
    }

    /// <summary>
    /// By using this function to insert items, <see cref="AttachmentState.AttachedInitially"/> will be listen  as first.
    /// </summary>
    /// <param name="itemAttachments"></param>
    /// <param name="item"></param>
    public static void InsertItemAttachment(this ObservableCollection<AttachedItem> itemAttachments, AttachedItem item)
    {
        if(item.State == AttachmentState.AttachedInitially)
        {
            itemAttachments.Insert(0, item);
        }
        else
        {
            itemAttachments.Add(item);
        }
    }

    /// <summary>
    /// Cleares the AttachedItems collection and inserts new items with <see cref="InsertItemAttachment"/>
    /// </summary>
    /// <remarks>
    /// Items are ordered by the <see cref="AttachedItem.Title"/>
    /// </remarks>
    public static void ReplaceItems(this ObservableCollection<AttachedItem> itemAttachments, IEnumerable<AttachedItem> newItems)
    {
        itemAttachments.Clear();
        foreach (AttachedItem attachedItem in newItems.OrderByDescending(x => x.Title))
        {
            itemAttachments.InsertItemAttachment(attachedItem);
        }
    }
}
