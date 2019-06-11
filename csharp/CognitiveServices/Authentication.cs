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
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Services.cognitiveServicesSubscriptionKey);
                    var result = await client.PostAsync(Services.textToSpeechAuthTokenUri, null);
                    return await result.Content.ReadAsStringAsync();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }
    }
}