using ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;

namespace ShoppingOrganizer.Mobile.Domain.Items.ContentPages;

public partial class RecipeDetail : ContentPage
{
	public RecipeDetail(RecipeDetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

		var vm = (RecipeDetailViewModel)BindingContext;
		await vm.GetRecipeParts();
    }
}