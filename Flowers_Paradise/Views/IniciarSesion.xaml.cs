using Firebase.Auth;
using Firebase.Auth.Repository;
namespace Flowers_Paradise.Views
{
        public partial class IniciarSesion : ContentPage
        {
            private readonly FirebaseAuthClient _clientAuth;
            private FileUserRepository _userRepository;

            public IniciarSesion(FirebaseAuthClient firebaseAuthClient)
            {
                InitializeComponent();
                _clientAuth = firebaseAuthClient;
                _userRepository = new FileUserRepository("Login");

                // Verifica si hay un usuario almacenado y navega a la p�gina principal si existe
                ChequearSiHayUsuarioAlmacenado().ConfigureAwait(false);
            }

            private async Task ChequearSiHayUsuarioAlmacenado()
            {
                if (_userRepository.UserExists())
                {
                    var (userInfo, firebaseCredential) = _userRepository.ReadUser();
                // Navega a la p�gina principal
                //var page = new NavigationPage(new Views.ProductListPage());
                await Navigation.PushAsync(new Views.ProductListPage());
            }
            }

            private async void btnRegistrarse_Clicked(object sender, EventArgs e)
            {
                // Navega a la p�gina de creaci�n de cuenta
                await Navigation.PushAsync(new Views.CrearCuenta(_clientAuth));
            }

            private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
            {
                try
                {
                    // Intenta iniciar sesi�n con el correo y la contrase�a proporcionados
                    var userCredential = await _clientAuth.SignInWithEmailAndPasswordAsync(txtEmail.Text, txtPassword.Text);

                    // Obtiene el ID Token del usuario
                    var idToken = await userCredential.User.GetIdTokenAsync();

                    // Verifica si el correo electr�nico del usuario est� verificado
                    bool emailVerified = await FirebaseAuthHelper.IsEmailVerifiedAsync(idToken);

                    if (emailVerified)
                    {
                        // Maneja las credenciales del usuario seg�n la opci�n "Recordar Contrase�a"
                        if (chkRecordarContrase�a.IsChecked)
                        {
                            _userRepository.SaveUser(userCredential.User);
                        }
                        else
                        {
                            _userRepository.DeleteUser();
                        }

                    // Navega a la aplicaci�n principal
                    //await Navigation.PushAsync(new AppShell());
                    await Navigation.PushAsync(new Views.ProductListPage());
                }
                    else
                    {
                        // Notifica al usuario que debe verificar su correo electr�nico
                        await DisplayAlert("Inicio de sesi�n", "Por favor, verifica tu correo electr�nico antes de iniciar sesi�n.", "Ok");
                    }
                }
                catch (FirebaseAuthException ex)
                {
                    // Muestra un mensaje de error detallado en caso de fallo en la autenticaci�n
                    await DisplayAlert("Inicio de sesi�n", "Credenciales incorrectas", "Ok");
                }
                catch (Exception ex)
                {
                    // Muestra un mensaje de error general en caso de fallo inesperado
                    await DisplayAlert("Error", $"Ocurri� un error inesperado: {ex.Message}", "Ok");
                }
            }
        }
}