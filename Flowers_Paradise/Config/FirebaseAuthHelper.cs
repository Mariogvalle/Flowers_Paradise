using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

//namespace Flowers_Paradise.Config
public static class FirebaseAuthHelper
{
    private const string FirebaseApiKey = "AIzaSyBKL59zZjOVLKhyvHQMY7_L_LUS2UGfpRA";

    public static async Task<bool> IsEmailVerifiedAsync(string idToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUri = $"https://identitytoolkit.googleapis.com/v1/accounts:lookup?key={FirebaseApiKey}";

            var content = new StringContent($"{{\"idToken\":\"{idToken}\"}}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(requestUri, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseBody);

            // Verifica el estado de verificación del correo electrónico
            var emailVerified = json["users"]?[0]?["emailVerified"]?.Value<bool>() ?? false;

            return emailVerified;
        }
    }
}