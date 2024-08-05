using Flowers_Paradise.Models;
using Flowers_Paradise.ViewModels;

namespace Flowers_Paradise.Views;

public partial class DetalleProducto : ContentPage
{
    public DetalleProducto(Product product)
    {
        InitializeComponent();
        BindingContext = product;
    }
}