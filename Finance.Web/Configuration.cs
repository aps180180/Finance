using MudBlazor.Utilities;
using MudBlazor;

namespace Finance.Web
{
    public static class Configuration
    {
        public const string HttpClientName = "finance";
        public static string BackendUrl { get; set; } = "https://localhost:7089";

        public static MudTheme Theme = new MudTheme()
        {
            Typography = new Typography
            {
                Default = new Default()
                {
                    FontFamily = ["League Spartan", "sans-serif"]
                }
            },
            Palette = new PaletteLight
            {
                Primary = new MudColor("#004aad"),
                Secondary = new MudColor("#00347a"),
                Background = new MudColor("#fefefe"),
                AppbarBackground = new MudColor("#004aad"),
                AppbarText = new MudColor("#000000"),
                TextPrimary = new MudColor("#000000"),
                PrimaryContrastText = new MudColor("#fefefe"),
                DrawerText = new MudColor("#000000"),
                DrawerBackground = new MudColor("#004aad")



            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.LightGreen.Accent3,
                Secondary = Colors.LightGreen.Darken3,
                AppbarBackground = Colors.LightGreen.Accent3,
                AppbarText = Colors.Shades.Black
            }

        };
    }
}
