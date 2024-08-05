using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Logging;
namespace Flowers_Paradise
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton(new FirebaseAuthClient(new
                FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyBKL59zZjOVLKhyvHQMY7_L_LUS2UGfpRA",
                AuthDomain = "flowersparadise-7ba1b.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            }));

            return builder.Build();
        }
    }
}
