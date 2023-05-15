using Stylelabs.M.Sdk.WebClient;
using Stylelabs.M.Sdk.WebClient.Authentication;
using System.Diagnostics.CodeAnalysis;

namespace MauiContentHub
{
    public static class MConnector
    {
        [RequiresAssemblyFiles()]
        public static IWebMClient Client()
        {
            // Enter your credentials here
            OAuthPasswordGrant oauth = new OAuthPasswordGrant
            {
                ClientId = AppSettings.ClientId,
                ClientSecret = AppSettings.ClientSecret,
                UserName = AppSettings.Username,
                Password = AppSettings.Password
            };

            // Create the Web SDK client
            return MClientFactory.CreateMClient(AppSettings.Host, oauth);
        }

        [RequiresAssemblyFiles()]
        public static async Task<bool> CheckConnection()
        {
            try
            {
                await MConnector.Client().TestConnectionAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
