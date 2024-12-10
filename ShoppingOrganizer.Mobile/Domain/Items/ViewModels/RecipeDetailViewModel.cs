using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingOrganizer.Mobile.Domain.Items.ContentPages;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;
using ShoppingOrganizer.Mobile.Infrastructure;
using ShoppingOrganizer.Models.Items;
using System.Collections.ObjectModel;
using static ShoppingOrganizer.Mobile.Domain.Items.Constants;

namespace ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;

[QueryProperty(nameof(Recipe), PropertyKeys.Recipe)]
public partial class RecipeDetailViewModel : ObservableObject
{
    public static string PlaceholderTitle = "New Recipe";

    private readonly IRecipeRepository _recipeRepository;
    private readonly IRecipePartRepository _recipePartRepository;

    #region Pragma
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
    public RecipeDetailViewModel()
    {
        _recipeRepository = PlatformServiceProvider.GetService<IRecipeRepository>();
        _recipePartRepository = PlatformServiceProvider.GetService<IRecipePartRepository>();
    }

    [ObservableProperty]
    string headerTitle;

    [ObservableProperty]
    public Recipe recipe;

    [ObservableProperty]
    ObservableCollection<RecipePart> recipeParts; 

    [ObservableProperty]
    ObservableCollection<Ingredient> ingredients;

    [RelayCommand]
    public async Task DeleteRecipe()
    {
        await _recipeRepository.Delete(r => r.Id == Recipe.Id);

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task OpenAttachRecipesPopup()
    {
        var navigationParameter = new Dictionary<string, object>() // chcem to disablnut a hodid popup ze changes budu lost
        {
            {PropertyKeys.Recipe, Recipe }
        };

        await Shell.Current.GoToAsync($"{nameof(AttachRecipesModal)}", navigationParameter);
    }

    public async Task GetRecipeParts()
    {
        List<RecipePart> fetchedRecipes = await _recipePartRepository.GetByFilter(x => x.OwnerRecipeId == Recipe.Id);
        RecipeParts = new ObservableCollection<RecipePart>(fetchedRecipes);
    }

    partial void OnRecipeChanging(Recipe value)
    {
        HeaderTitle = string.IsNullOrEmpty(value.Title) ? PlaceholderTitle : value.Title;
    }
}