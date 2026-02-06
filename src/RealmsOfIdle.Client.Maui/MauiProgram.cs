using Microsoft.Extensions.Logging;
using RealmsOfIdle.Client.Shared.DependencyInjection;

namespace RealmsOfIdle.Client.Maui;

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

        // Register shared client services
        builder.Services.AddMauiClient();

        builder.Services.AddLogging(logging =>
        {
#if DEBUG
            logging.AddDebug();
#endif
        });

        return builder.Build();
    }
}
