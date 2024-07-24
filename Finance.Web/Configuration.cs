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
            
            PaletteLight = new PaletteLight
            {
                Primary = new MudColor("#004aad"),
                Secondary = new MudColor("#FFFFFF"),
                Background = new MudColor("#fefefe"),
                AppbarBackground = new MudColor("#004aad"),
                AppbarText = new MudColor("#FFFFFF"),
                TextPrimary = new MudColor("#000000"),
                PrimaryContrastText = new MudColor("#FFFFFF"),
                DrawerText = new MudColor("#FFFFFF"),
                DrawerBackground = new MudColor("#004aad"),
               



            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.LightGreen.Accent3,
                Secondary = Colors.LightGreen.Darken3,
                AppbarBackground = Colors.DeepPurple.Accent3,
                AppbarText = Colors.Shades.Black,
                PrimaryContrastText = new MudColor("#000000"),
            }

        };
    }
}
