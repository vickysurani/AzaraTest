using Google.Api.Gax;
using System.Net.Http;

namespace azara.api.Helpers.Schedular
{
    public class SchedularCaller : IHostedService, IDisposable
    {
        private readonly IConfiguration _configuration;
        private Timer _timer;
        HttpClient httpClient = new HttpClient();

        public SchedularCaller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CallAPI, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;  
            //throw new NotImplementedException();
        }


        private async void CallAPI(object state)
        {
            // call your API here

            Console.WriteLine("Schedule Start");
            var response = await httpClient.GetAsync("https://apiazara.azurewebsites.net/api/v1/pos_sub_category/insert_update");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                //var content = response.Result.ToString();
                Console.WriteLine(content);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();

                //var content = response.Result.ToString();
                Console.WriteLine(content);
                Console.WriteLine("Failed to get data");
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;      
            //throw new NotImplementedException();
        }
        public void Dispose()
        {
            _timer?.Dispose();
            //throw new NotImplementedException();
        }
    }
}
