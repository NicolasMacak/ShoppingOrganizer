using ShoppingOrganizer.Models.Items;
using System.Collections.ObjectModel;
using static ShoppingOrganizer.Models.Items.ItemAttachment;

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
    /// By using this function to insert items, <see cref="AttachmentState.AlreadyAttached"/> will be listen  as first.
    /// </summary>
    /// <param name="itemAttachments"></param>
    /// <param name="item"></param>
    public static void InsertItemAttachment(this ObservableCollection<ItemAttachment> itemAttachments, ItemAttachment item)
    {
        if(item.Attachment == AttachmentState.AlreadyAttached)
        {
            itemAttachments.Insert(0, item);
        }
        else
        {
            itemAttachments.Add(item);
        }
    }
}
