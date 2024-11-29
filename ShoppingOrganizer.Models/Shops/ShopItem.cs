using ShopOrganizer.Database.Entities.Shops;

namespace ShopOrganizer.Models.Shops;

public class ShopItem // do obchodu
{
    public ShopItem()
    {
        Price = 0;
    }
    public ShopItem(int id, string title, string price)
    {
        Id = id;
        Price = decimal.Parse(price);
    }
    public int Id { get; set; }
    public int IngredientId {  get; set; }
    public int ShopId {  get; set; }
    public Decimal Price { get; set; }

    public static explicit operator ShopItem(ShopItemEntity shopItemDto)
    {
        return new ShopItem();
    }
}

