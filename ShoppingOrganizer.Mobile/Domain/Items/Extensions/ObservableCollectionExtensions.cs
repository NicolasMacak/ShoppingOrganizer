using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOrganizer.Mobile.Domain.Items.Extensions;
internal static class ObservableCollectionExtensions
{
    public static void UpdateItem<T>(this ObservableCollection<T> collection, T item)
    {
        int index = collection.IndexOf(item);
        collection[index] = item;
    }
}
