using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingOrganizer.Mobile.Domain.Items.ContentPages;
using static ShoppingOrganizer.Mobile.Shared.Constants;
using System.Collections.ObjectModel;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;
using ShoppingOrganizer.Models.Items;
using ShoppingOrganizer.Mobile.Core;

namespace ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;
public partial class IngredientsViewModel : ObservableObject
{
    #region pragma
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
    public IngredientsViewModel()
    {
        _ingredientRepository = PlatformServiceProvider.GetService<IIngredientRepository>();
    }

    private readonly IIngredientRepository _ingredientRepository;

    public async Task GetIngredientsFromDb()
    {
        IEnumerable<Ingredient> ingredients = await _ingredientRepository.GetAll();

        Ingredients = new ObservableCollection<Ingredient>(ingredients);
    }

    [ObservableProperty]
    ObservableCollection<Ingredient> ingredients;

    [RelayCommand]
    async Task NavigateToDetail(Ingredient ingredient)
    {
        var ingredientToPass = ingredient is null ? new Ingredient() : ingredient;

        var navigationParameter = new Dictionary<string, object>()
        {
            {PropertyKeys.Ingredient, ingredientToPass }
        };

        await Shell.Current.GoToAsync($"{nameof(IngredientDetail)}", navigationParameter);
    }
}

