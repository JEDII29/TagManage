using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Text.Json;
using TagManage.Data.Entities;

namespace TagManage.Domain.ExternalApp
{
    public class ExternalApiService : IExternalApi
    {
        private string Url = "https://api.stackexchange.com/2.3";

        ExternalApiService()
        {
        }

        public async Task<List<TagEntity>> SendRequest()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(Url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<TagEntity> result = new List<TagEntity>();
                        JsonSerializer.Deserialize<TagEntity>(responseBody);
                    return result;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Wystąpił błąd podczas wysyłania żądania: {e.Message}");
                }
                return null;
            }
        }
    }
}
