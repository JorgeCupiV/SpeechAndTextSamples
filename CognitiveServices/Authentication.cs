using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CognitiveServices
{
    public class Authentication
    {
        string tokenFetchUri;

        async Task<string> FetchTokenAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Services.cognitiveServicesSubscriptionKey);
                UriBuilder uriBuilder = new UriBuilder(tokenFetchUri);

                var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null).ConfigureAwait(false);
                return await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        public async void GetAccessToken()
        {
            tokenFetchUri = string.Format(Services.textToSpeechhost, Services.cognitiveServicesAzureRegion);

            try
            {
                Services.cognitiveServicesAccessToken = await FetchTokenAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to obtain an access token.");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }
    }
}