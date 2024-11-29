using ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;

namespace ShoppingOrganizer.Mobile.Domain.Items.ContentPages;

public partial class Ingredients : ContentPage
{
    public Ingredients(IngredientsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var viewModel = BindingContext as IngredientsViewModel;
        await viewModel.GetIngredientsFromDb();
    }
}