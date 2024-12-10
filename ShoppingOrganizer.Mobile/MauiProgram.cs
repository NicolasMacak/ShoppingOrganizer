using Microsoft.Extensions.Logging;
using ShoppingOrganizer.Database;
using ShoppingOrganizer.Mobile.Domain.Items.ContentPages;
using ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;
using AutoMapper;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Models.Items;

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
            AddAutoMapper(builder.Services);

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
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            MapperConfiguration configuration = new (cfg =>
            {
                cfg.CreateMap<IngredientEntity, Ingredient>().ReverseMap();
                cfg.CreateMap<RecipeEntity, Recipe>().ReverseMap();
                cfg.CreateMap<RecipePartEntity, RecipePart>().ReverseMap();
            });

            IMapper mapper = configuration.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
