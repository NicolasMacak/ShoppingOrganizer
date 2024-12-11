using ShoppingOrganizer.Database;
using ShoppingOrganizer.Database.Entities.Shops;
using ShoppingOrganizer.Database.Entities.Items;
using SQLite;

namespace ShoppingOrganizer.Database;

public class DatabaseHandler
{
    public SQLiteAsyncConnection Database;

    public DatabaseHandler()
    {
        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
    }

    async public Task Init() // TODO. Volat raz pri starte aplikacie. Pri nejakom loadingu
    {
        //await DeleteAndRepopulateData();

        if (Database is not null) 
        {
            return;
        }

        //await DeleteAndRepopulateData();
        await CreateTablesIfNotExists();
    }

    private async Task CreateTablesIfNotExists()
    {
        await Database.CreateTableAsync<RecipeEntity>();
        await Database.CreateTableAsync<RecipePartEntity>();
        await Database.CreateTableAsync<IngredientEntity>();
        await Database.CreateTableAsync<ShopItemEntity>();
    }

    private async Task DeleteAndRepopulateData()
    {
        await Database.DropTableAsync<RecipeEntity>();
        await Database.DropTableAsync<RecipePartEntity>();
        await Database.DropTableAsync<IngredientEntity>();
        await Database.DropTableAsync<ShopItemEntity>();

        await CreateTablesIfNotExists();

        /* Brokolicova polievka
        200g batatu
        170g brokolice
        380 ml vody - flag: never buying
        Vajcia
        salt
        */
        // Alternative. Binding variants. This alternative uses variants with this ids
        #region data
        var data = new List<object>
        {
            // Brocoli polievka
            new IngredientEntity() {Id = 1, Title = "Batat", Unit = "g"},
            new IngredientEntity() {Id = 2, Title = "Brokolica", Unit = "g"},
            new IngredientEntity() {Id = 3, Title = "Vajcia", Unit = "ks"},
            new IngredientEntity() {Id = 4, Title = "Sol", Unit = "g"},

            new RecipePartEntity() {Id = 1, OwnerRecipeId = 1, Title = "Batat", IngredientId = 1, Quantity = 200},
            new RecipePartEntity() {Id = 2, OwnerRecipeId = 1, Title = "Brokolica", IngredientId = 2, Quantity = 170},
            new RecipePartEntity() {Id = 3, OwnerRecipeId = 1, Title = "Vajcia", IngredientId = 3, Quantity = 4},
            new RecipePartEntity() {Id = 4, OwnerRecipeId = 1, Title = "Sol", IngredientId = 4, Quantity = 1},

            new RecipeEntity() {Id = 1, Title = "Brokolica polievka" },

            // Tahini dip

            new IngredientEntity() {Id = 5, Title = "Korenie", Unit = "g"},
            new IngredientEntity() {Id = 6, Title = "Tahini pasta", Unit = "g"},

            new RecipePartEntity() {Id = 5, OwnerRecipeId = 2, Title = "Sol", IngredientId = 4, Quantity = 1},
            new RecipePartEntity() {Id = 6, OwnerRecipeId = 2, Title = "Korenie", IngredientId = 5, Quantity = 1},
            new RecipePartEntity() {Id = 7, OwnerRecipeId = 2, Title = "Tahini pasta", IngredientId = 6, Quantity = 1},

            new RecipeEntity() {Id = 2, Title = "Tahini dip"},
            
            // Fazula v tortile
            new IngredientEntity() {Id = 7, Title = "Fazula", Unit = "g"},
            new IngredientEntity() {Id = 8, Title = "Ryza", Unit = "g"},

            new RecipePartEntity() {Id = 8, OwnerRecipeId = 3, Title = "Tahini dip", RecipeId = 2, Quantity = 1},
            new RecipePartEntity() {Id = 9, OwnerRecipeId = 3, Title = "Ryza", IngredientId = 7, Quantity = 1},
            new RecipePartEntity() {Id = 9, OwnerRecipeId = 3, Title = "Fazula", IngredientId = 8, Quantity = 1},

            new RecipeEntity() {Id = 3, Title = "Fazula v tortile"}
        };
        #endregion

        await Database.InsertAllAsync(data);
    }


    //public async Task<IEnumerable<T>> GetItemsAsync<T>()
    //{
    //    await Init();
    //    return await _database.Table<T>().ToListAsync();
    //}

    //public async Task<int> SaveItemAsync(IngredientEntity item)
    //{
    //    await Init();
    //    if (item.Id != 0)
    //        return await _database.UpdateAsync(item);
    //    else
    //        return await _database.InsertAsync(item);
    //}

}
