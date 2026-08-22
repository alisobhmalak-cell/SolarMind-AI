using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Logging;

namespace Solar_Mind_AI
{
    public static class MauiProgram
    {
        // Firebase Authentication Client
        public static FirebaseAuthClient AuthClient { get; private set; }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Firebase Configuration
            var firebaseConfig = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyD-eZc9CEPzGaK8HuJtCLfZQJVBLtmksPI",
                AuthDomain = "solarmind-5bcf1.firebaseapp.com",

                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };

            // إنشاء Firebase Authentication Client
            AuthClient = new FirebaseAuthClient(firebaseConfig);

            // تسجيله في Dependency Injection
            builder.Services.AddSingleton(AuthClient);

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

            return builder.Build();
        }
    }
}