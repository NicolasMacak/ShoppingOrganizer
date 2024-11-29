using ShoppingOrganizer.Mobile.Domain.Items.ContentPages;

namespace ShoppingOrganizer.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(IngredientDetail), typeof(IngredientDetail));
            Routing.RegisterRoute(nameof(RecipeDetail), typeof(RecipeDetail));
            Routing.RegisterRoute(nameof(AttachRecipesModal), typeof(AttachRecipesModal));
        }
    }
}
