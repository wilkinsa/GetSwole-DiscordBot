using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Common.Interfaces;

namespace Infrastructure.MemeGenerator
{
    public class MemeGeneratorService : IMemeGenerator
    {
        private readonly HttpClient  _client;
        public MemeGeneratorService(HttpClient  client)
    {
        _client = client;
    }
        public async Task<string> GetWorkoutMeme()
        {
            var response = await _client.GetAsync(
            "http://meme-api.herokuapp.com/gimme/gymmemes");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var memeResponse = await JsonSerializer.DeserializeAsync
                <MemeResponse>(responseStream);
            
            return memeResponse.url;
        }

    }

    public class MemeResponse
    {
        public  string postLink { get; set; }
        public string subreddit { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public bool nsfw { get; set; }
        public bool spoiler { get; set; }
    }
}