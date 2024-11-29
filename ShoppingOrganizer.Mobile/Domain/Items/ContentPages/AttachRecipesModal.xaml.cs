using ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;

namespace ShoppingOrganizer.Mobile.Domain.Items.ContentPages;

public partial class AttachRecipesModal : ContentPage
{
	public AttachRecipesModal(AttachRecipesModalViewModel  vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();

		var vm = BindingContext as AttachRecipesModalViewModel;

		await vm.InitializeScreen();
	}
}