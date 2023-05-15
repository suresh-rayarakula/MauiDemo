using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MauiContentHub
{
    public static class AppSettings
    {
        private static IConfiguration _config;
        public static IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder();
                    var a = Assembly.GetExecutingAssembly();
                    using var stream = a.GetManifestResourceStream("MauiContentHub.appsettings.json");
                    var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
                    builder.AddConfiguration(config);
                    _config = builder.Build();
                }

                return _config;
            }
        }
        public static Uri Host { get { return new Uri($"{Configuration["M:Host"]}"); } }
        public static string ClientId { get { return $"{Configuration["M:ClientId"]}"; } }
        public static string ClientSecret { get { return $"{Configuration["M:ClientSecret"]}"; } }
        public static string Username { get { return $"{Configuration["M:Username"]}"; } }
        public static string Password { get { return $"{Configuration["M:Password"]}"; } }
    }
}
