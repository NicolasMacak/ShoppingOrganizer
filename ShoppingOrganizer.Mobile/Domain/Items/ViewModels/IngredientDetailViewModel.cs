using CommunityToolkit.Mvvm.ComponentModel;
using static ShoppingOrganizer.Mobile.Domain.Items.Constants;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;
using ShoppingOrganizer.Models.Items;
using ShoppingOrganizer.Mobile.Infrastructure;

namespace ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;
[QueryProperty(nameof(Ingredient), PropertyKeys.Ingredient)]
public partial class IngredientDetailViewModel : ObservableObject
{
    #region pragma
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
    public IngredientDetailViewModel()
    {
        _ingredientRepository = PlatformServiceProvider.GetService<IIngredientRepository>();

        UnitOptions = new ObservableCollection<string>(new string[] {"Placeholder", "DruhyPlace"});
    }

    private readonly IIngredientRepository _ingredientRepository;

    private const string PlaceholderTitle = "New Ingredient";


    [RelayCommand]
    public async Task SaveItem(Ingredient ingredient)
    {
        //await _ingredientRepository.Update(new List<Ingredient> { ingredient });
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