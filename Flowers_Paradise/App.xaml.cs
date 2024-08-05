using Firebase.Auth;

namespace Flowers_Paradise
{
    public partial class App : Application
    {
        public App(FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent();
            // para iniciar solicitando autenticación
            MainPage = new NavigationPage(new Views.IniciarSesion(firebaseAuthClient));
            // MainPage = new NavigationPage(new Views.ProductListPage());
            //MainPage = new NavigationPage(new MainPage());
        }

    }
}