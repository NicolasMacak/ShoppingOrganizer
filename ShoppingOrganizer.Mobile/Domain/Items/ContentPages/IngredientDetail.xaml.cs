using ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;

namespace ShoppingOrganizer.Mobile.Domain.Items.ContentPages;

public partial class IngredientDetail : ContentPage
{
    public IngredientDetail(IngredientDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}