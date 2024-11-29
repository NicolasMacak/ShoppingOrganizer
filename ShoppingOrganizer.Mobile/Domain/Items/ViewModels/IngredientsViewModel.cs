using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingOrganizer.Mobile.Domain.Items.ContentPages;
using static ShoppingOrganizer.Mobile.Shared.Constants;
using System.Collections.ObjectModel;
using ShoppingOrganizer.Mobile.Shared.Helpers;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;
using ShoppingOrganizer.Models.Items;

namespace ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;
public partial class IngredientsViewModel : ObservableObject
{
    public IngredientsViewModel()
    {
        _ingredientRepository = ServiceHelper.GetService<IIngredientRepository>();
    }

    private readonly IIngredientRepository _ingredientRepository;

    public async Task GetIngredientsFromDb()
    {
        var ingredients = await _ingredientRepository.GetCollection();

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

