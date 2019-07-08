using System.Diagnostics;
using System.Threading.Tasks;

namespace POLib.Http
{
    public class RateLimitedHttpClient : IHttpClient
    {
        public RateLimitedHttpClient(System.Net.Http.HttpClient client)
        {
            _client = client;
        }
        public async Task<string> ReadAsync(string url)
        {
            if (!_sw.IsRunning)
                _sw.Start();

            await Delay();

            using var response = await _client.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        private async Task Delay()
        {
            var totalElapsed = GetTimeElapsedSinceLastRequest();

            while (totalElapsed < MinTimeBetweenRequests)
            {
                await Task.Delay(MinTimeBetweenRequests - totalElapsed);
                totalElapsed = GetTimeElapsedSinceLastRequest();
            };

            _timeElapsedOfLastHttpRequest = (int)_sw.Elapsed.TotalMilliseconds;
        }

        private int GetTimeElapsedSinceLastRequest()
        {
            return (int)_sw.Elapsed.TotalMilliseconds - _timeElapsedOfLastHttpRequest;
        }

        private readonly System.Net.Http.HttpClient _client;
        private readonly Stopwatch _sw = new Stopwatch();
        private int _timeElapsedOfLastHttpRequest;
        private const int MinTimeBetweenRequests = 100;
    }
}
