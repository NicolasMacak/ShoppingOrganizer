using Microsoft.Extensions.Logging;
using ShoppingOrganizer.Database;
using ShoppingOrganizer.Mobile.Domain.Items.ContentPages;
using ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;

namespace ShoppingOrganizer.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            RegisterPagesAndViewModels(builder.Services);
            RegisterServices(builder.Services);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<DatabaseHandler>();

            services.AddTransient<IRecipeRepository, RecipeRepository>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<IRecipePartRepository, RecipePartRepository>();
        }

        private static void RegisterPagesAndViewModels(IServiceCollection services)
        {
            #region Items

            services.AddSingleton<Ingredients>();
            services.AddSingleton<IngredientsViewModel>();

            services.AddTransient<IngredientDetail>();
            services.AddTransient<IngredientDetailViewModel>();

            services.AddTransient<Recipes>();
            services.AddTransient<RecipesViewModel>();

            services.AddTransient<RecipeDetail>();
            services.AddTransient<RecipeDetailViewModel>();

            services.AddTransient<AttachRecipesModal>();
            services.AddTransient<AttachRecipesModalViewModel>();

            #endregion
        }
    }
}
