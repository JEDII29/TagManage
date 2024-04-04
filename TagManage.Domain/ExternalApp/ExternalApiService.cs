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
using System.IO.Compression;
using AutoMapper;

namespace TagManage.Domain.ExternalApp
{
    public class ExternalApiService : IExternalApi
    {
        private readonly IMapper _mapper;
        private int page; 
        private string _url = "https://api.stackexchange.com/2.3/tags?page=1&pagesize=100&order=desc&sort=popular&site=stackoverflow&filter=!21k7s53IC58)r8.Zl89eW";

        public ExternalApiService(IMapper mapper)
        {
            _mapper = mapper;
            page = 1;
        }

        public async Task<List<TagEntity>> GetTagsRequest()
        {
            List<TagEntity> result = new List<TagEntity>();
            for (page = 1; page <= 20; page++)
            {
                _url = $"https://api.stackexchange.com/2.3/tags?page={page}&pagesize=100&order=desc&sort=popular&site=stackoverflow&filter=!21k7s53IC58)r8.Zl89eW";
                result.AddRange(await GetHundredTagsRequest(_url));
            }
            return result;
        }


        public async Task<List<TagEntity>> GetHundredTagsRequest(string url)
        {
            using var httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using Stream responseStream = await response.Content.ReadAsStreamAsync();
                    Stream decompressedStream = responseStream;
                    if (response.Content.Headers.ContentEncoding.Contains("gzip"))
                    {
                        decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
                    }
                    else if (response.Content.Headers.ContentEncoding.Contains("deflate"))
                    {
                        decompressedStream = new DeflateStream(responseStream, CompressionMode.Decompress);
                    }
                    using StreamReader reader = new StreamReader(decompressedStream);

                    string responseBody = await reader.ReadToEndAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    try
                    {
                        StackQueryModel obj = new StackQueryModel();
                        obj = JsonSerializer.Deserialize<StackQueryModel>(responseBody, options);
                        List<TagEntity> result = new List<TagEntity>();
                        foreach (StackTagModel item in obj.Items) {
                            TagEntity tag = _mapper.Map<TagEntity>(item);
                            tag.Id = Guid.NewGuid();
                            result.Add(tag);
                        }
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine($"Wystąpił błąd: {response.StatusCode}");
                    return null;
                }
            }
        }
    }
}
