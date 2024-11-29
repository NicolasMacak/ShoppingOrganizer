using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingOrganizer.Mobile.Domain.Items.ContentPages;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;
using ShoppingOrganizer.Mobile.Shared.Helpers;
using ShoppingOrganizer.Models.Items;
using System.Collections.ObjectModel;
using static ShoppingOrganizer.Mobile.Shared.Constants;

namespace ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;

public partial class RecipesViewModel : ObservableObject
{
    public RecipesViewModel()
    {
        _recipeRepository = ServiceHelper.GetService<IRecipeRepository>();
    }

    private readonly IRecipeRepository _recipeRepository;

    [ObservableProperty]
    ObservableCollection<Recipe> recipes;
    public async Task GetRecipesFromDb()
    {
        var dbRecipes = await _recipeRepository.GetCollection();
   
        Recipes = new ObservableCollection<Recipe>(dbRecipes);
    }

    [RelayCommand]
    public async Task NavigateToDetail(Recipe recipe)
    {
        var recipeToPass = recipe is null ? new Recipe() : recipe;

        var navigationParameter = new Dictionary<string, object>()
        {
            {PropertyKeys.Recipe, recipeToPass }
        };

        await Shell.Current.GoToAsync($"{nameof(RecipeDetail)}", navigationParameter);
    }

    /// <summary>
    /// Removes item from observable collection and from the database
    /// </summary>
    [RelayCommand]
    public async Task Delete(Recipe recipe)
    {
        Recipes.Remove(recipe);

        await _recipeRepository.Delete(r => r.Id == recipe.Id);
    }
}