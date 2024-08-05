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

                // Verifica si hay un usuario almacenado y navega a la página principal si existe
                ChequearSiHayUsuarioAlmacenado().ConfigureAwait(false);
            }

            private async Task ChequearSiHayUsuarioAlmacenado()
            {
                if (_userRepository.UserExists())
                {
                    var (userInfo, firebaseCredential) = _userRepository.ReadUser();
                // Navega a la página principal
                //var page = new NavigationPage(new Views.ProductListPage());
                await Navigation.PushAsync(new Views.ProductListPage());
            }
            }

            private async void btnRegistrarse_Clicked(object sender, EventArgs e)
            {
                // Navega a la página de creación de cuenta
                await Navigation.PushAsync(new Views.CrearCuenta(_clientAuth));
            }

            private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
            {
                try
                {
                    // Intenta iniciar sesión con el correo y la contraseña proporcionados
                    var userCredential = await _clientAuth.SignInWithEmailAndPasswordAsync(txtEmail.Text, txtPassword.Text);

                    // Obtiene el ID Token del usuario
                    var idToken = await userCredential.User.GetIdTokenAsync();

                    // Verifica si el correo electrónico del usuario está verificado
                    bool emailVerified = await FirebaseAuthHelper.IsEmailVerifiedAsync(idToken);

                    if (emailVerified)
                    {
                        // Maneja las credenciales del usuario según la opción "Recordar Contraseña"
                        if (chkRecordarContraseña.IsChecked)
                        {
                            _userRepository.SaveUser(userCredential.User);
                        }
                        else
                        {
                            _userRepository.DeleteUser();
                        }

                    // Navega a la aplicación principal
                    //await Navigation.PushAsync(new AppShell());
                    await Navigation.PushAsync(new Views.ProductListPage());
                }
                    else
                    {
                        // Notifica al usuario que debe verificar su correo electrónico
                        await DisplayAlert("Inicio de sesión", "Por favor, verifica tu correo electrónico antes de iniciar sesión.", "Ok");
                    }
                }
                catch (FirebaseAuthException ex)
                {
                    // Muestra un mensaje de error detallado en caso de fallo en la autenticación
                    await DisplayAlert("Inicio de sesión", "Credenciales incorrectas", "Ok");
                }
                catch (Exception ex)
                {
                    // Muestra un mensaje de error general en caso de fallo inesperado
                    await DisplayAlert("Error", $"Ocurrió un error inesperado: {ex.Message}", "Ok");
                }
            }
        }
}