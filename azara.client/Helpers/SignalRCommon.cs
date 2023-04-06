using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System;

namespace azara.client.Helpers
{
    public class SignalRCommon
    {
        public HubConnection hubConnection;

        public async Task StartHubConnection()
        {
            hubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri($"{ApiEndPointConsts.BaseRouteWithoutVersion}azara_api_notifications", UriKind.Absolute))
            .WithAutomaticReconnect()
            .Build();

            try
            {
                //await hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting SignalR connection: {ex.Message}");
            }

        }

    }
}
