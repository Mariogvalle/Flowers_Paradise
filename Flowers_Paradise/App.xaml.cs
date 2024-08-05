using Firebase.Auth;

namespace Flowers_Paradise
{
    public partial class App : Application
    {
        public App(FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent();
            // para iniciar solicitando autenticaci�n
            MainPage = new NavigationPage(new Views.IniciarSesion(firebaseAuthClient));
            // MainPage = new NavigationPage(new Views.ProductListPage());
            //MainPage = new NavigationPage(new MainPage());
        }

    }
}