using Firebase.Auth;
using Flowers_Paradise.ViewModels;
namespace Flowers_Paradise.Views;

public partial class ProductListPage : ContentPage
{
    public ProductListPage()
    {
        InitializeComponent();
        BindingContext = new ProductListModels();
    }

      private void OnLoginClicked(object sender, EventArgs e)
    {
        //        var page = new Views.IniciarSesion(firebaseAuthClient);
        //        Navigation.PushAsync(page);
    }

    private void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

    private void OnCarritoClicked(object sender, EventArgs e)
    {
                var page = new Views.Carrito();
                Navigation.PushAsync(page);
    }

    private void OnPerfilClicked(object sender, EventArgs e)
    {
                //var page = new Views.IniciarSesion(Firebase firebaseAuthClient);
                //Navigation.PushAsync(page);
    }

    private void OnMenuClicked(object sender, EventArgs e)
    {

    }


    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = BindingContext as ProductListModels;
        if (viewModel != null)
        {
            // Filter the Products collection based on the search text
            // For simplicity, you can filter products with a Contains on the Name property
            var filteredProducts = viewModel.Products.Where(p => p.Nombre.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
            viewModel.Products.Clear();
            foreach (var product in filteredProducts)
            {
                viewModel.Products.Add(product);
            }
        }
    }
}