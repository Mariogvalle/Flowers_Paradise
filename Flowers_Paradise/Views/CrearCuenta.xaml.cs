using Firebase.Auth;
using System.Net.Http.Headers;

namespace Flowers_Paradise.Views;


    public partial class CrearCuenta : ContentPage
    {
        private readonly FirebaseAuthClient _client;
        private const string FirebaseApiKey = "AIzaSyBKL59zZjOVLKhyvHQMY7_L_LUS2UGfpRA";
        private const string RequestUri = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + FirebaseApiKey;

        public CrearCuenta(FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent();
            _client = firebaseAuthClient;
        }

        private async void btnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtVerificarPassword.Text)
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Las contraseñas ingresadas no coinciden", "Ok");
                return;
            }

            try
            {
                var user = await _client.CreateUserWithEmailAndPasswordAsync(txtEmail.Text, txtPassword.Text);
                var idToken = await user.User.GetIdTokenAsync();
                await SendVerificationEmailAsync(idToken);
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Cuenta creada exitosamente!", "Ok");
            }
            catch (FirebaseAuthException ex)
            {
                // Handle FirebaseAuthException specifically
                await Application.Current.MainPage.DisplayAlert("Error", ex.Reason.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrió un error inesperado: " + ex.Message, "Ok");
            }
        }

        private static async Task SendVerificationEmailAsync(string idToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestBody = new
                {
                    requestType = "VERIFY_EMAIL",
                    idToken
                };

                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, content);
                response.EnsureSuccessStatusCode();
            }
        }
}