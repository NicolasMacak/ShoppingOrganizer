using ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;

namespace ShoppingOrganizer.Mobile.Domain.Items.ContentPages;

public partial class Recipes : ContentPage
{
    public Recipes(RecipesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as RecipesViewModel;

        await vm.GetRecipesFromDb();
    }
}