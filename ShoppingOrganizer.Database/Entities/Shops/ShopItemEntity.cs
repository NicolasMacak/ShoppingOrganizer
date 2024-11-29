using SQLite;

namespace ShoppingOrganizer.Database.Entities.Shops;
[Table("ShopItems")]
public class ShopItemEntity
{
    public ShopItemEntity() { }
    public ShopItemEntity(int id, string title, string price) // todo. to remove
    {
        Id = id;
        Title = title;
        Price = price;
    }

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Price { get; set; }
    public double QuantityAmmount { get; set; }
    public string QuantityUnit { get; set; }
}


