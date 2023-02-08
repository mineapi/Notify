using CommunityToolkit.Maui;

namespace tidewater_comp_2023;

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
				fonts.AddFont("Segoe UI.ttf", "SegoeUIRegular");
				fonts.AddFont("Segoe UI Bold.ttf", "SegoeUIBold");
				fonts.AddFont("Segoe UI Italic.ttf", "SegoeUIItalic");
				fonts.AddFont("Segoe UI Bold Italic.ttf", "SegoeUIBoldItalic");
                fonts.AddFont("Segoe Fluent Icons.ttf", "Icons");
                fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                fonts.AddFont("MaterialIcons_Rounded.ttf", "MaterialIconsRounded");
            });

        builder.UseMauiApp<App>().UseMauiCommunityToolkit();

        return builder.Build();
	}
}
