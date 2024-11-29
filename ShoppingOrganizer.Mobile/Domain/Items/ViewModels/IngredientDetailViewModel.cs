using CommunityToolkit.Mvvm.ComponentModel;
using static ShoppingOrganizer.Mobile.Shared.Constants;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingOrganizer.Mobile.Shared.Helpers;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;
using ShoppingOrganizer.Models.Items;

namespace ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;
[QueryProperty(nameof(Ingredient), PropertyKeys.Ingredient)]
public partial class IngredientDetailViewModel : ObservableObject
{
    #region Initialization and properties
    public IngredientDetailViewModel()
    {
        _ingredientRepository = ServiceHelper.GetService<IIngredientRepository>();

        UnitOptions = new ObservableCollection<string>(new string[] {"Placeholder", "DruhyPlace"});
    }

    private readonly IIngredientRepository _ingredientRepository;

    private const string PlaceholderTitle = "New Ingredient";

    #endregion

    [RelayCommand]
    public async Task SaveItem(Ingredient ingredient)
    {
        await _ingredientRepository.Update(new List<Ingredient> { ingredient });
        await Shell.Current.GoToAsync("..");
    }

    [ObservableProperty]
    Ingredient ingredient;

    [ObservableProperty]
    string headerTitle;

    [ObservableProperty]
    ObservableCollection<string> unitOptions;

    partial void OnIngredientChanging(Ingredient value)
    {
        HeaderTitle = string.IsNullOrEmpty(value.Title) ? PlaceholderTitle : value.Title;
    }
}