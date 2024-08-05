namespace Flowers_Paradise.Views;

public partial class ProductPage : ContentPage
{
    public ProductPage()
    {
        InitializeComponent();
        BindingContext = new Flowers_Paradise.ViewModels.ProductViewModel();
    }
}