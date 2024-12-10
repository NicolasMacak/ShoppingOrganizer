using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingOrganizer.Mobile.Core;
using ShoppingOrganizer.Mobile.Domain.Items.ContentPages;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;
using ShoppingOrganizer.Models.Items;
using System.Collections.ObjectModel;
using static ShoppingOrganizer.Mobile.Shared.Constants;

namespace ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;

public partial class RecipesViewModel : ObservableObject
{
    #region Pragma
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
    public RecipesViewModel()
    {
        _recipeRepository = PlatformServiceProvider.GetService<IRecipeRepository>();
    }

    private readonly IRecipeRepository _recipeRepository;

    [ObservableProperty]
    ObservableCollection<Recipe> recipes;
    public async Task GetRecipesFromDb()
    {
        List<Recipe> dbRecipes = await _recipeRepository.GetAll();
   
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