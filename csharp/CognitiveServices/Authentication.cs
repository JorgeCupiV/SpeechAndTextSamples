using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CognitiveServices
{
    public class Authentication
    {
        public static async Task<string> GetAccessToken()
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Services.cognitiveServicesSubscriptionKey);
                var result = await client.PostAsync(Services.textToSpeechAuthTokenUri, null);
                return await result.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to obtain an access token.");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                return ex.ToString();
            }
        }
    }
}