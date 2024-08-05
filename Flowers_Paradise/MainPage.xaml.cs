using Flowers_Paradise;
namespace Flowers_Paradise;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void OnInicioClicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new Views.ProductListPage());
        var page = new NavigationPage(new Views.ProductListPage());
    }

    private void OnCarritoClicked(object sender, EventArgs e)
    {

    }

    private void OnPerfilClicked(object sender, EventArgs e)
    {

    }
}