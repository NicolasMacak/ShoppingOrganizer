using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingOrganizer.Mobile.Domain.Items.ContentPages;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;
using ShoppingOrganizer.Mobile.Shared.Helpers;
using ShoppingOrganizer.Models.Items;
using System.Collections.ObjectModel;
using static ShoppingOrganizer.Mobile.Shared.Constants;

namespace ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;

[QueryProperty(nameof(Recipe), PropertyKeys.Recipe)]
public partial class RecipeDetailViewModel : ObservableObject
{
    public static string PlaceholderTitle = "New Recipe";

    private readonly IRecipeRepository _recipeRepository;
    private readonly IIngredientRepository _ingredientRepository;

    public RecipeDetailViewModel()
    {
        _recipeRepository = ServiceHelper.GetService<IRecipeRepository>();
        _ingredientRepository = ServiceHelper.GetService<IIngredientRepository>();
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
        var fetchedRecipes = await _recipeRepository.GetRecipeParts(Recipe.Id);
        RecipeParts = new ObservableCollection<RecipePart>(fetchedRecipes);
    }

    partial void OnRecipeChanging(Recipe value)
    {
        HeaderTitle = string.IsNullOrEmpty(value.Title) ? PlaceholderTitle : value.Title;
    }
}