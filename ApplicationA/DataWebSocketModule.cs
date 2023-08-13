using EmbedIO.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationA
{
    public class DataWebSocketModule : WebSocketModule
    {
        private static bool _isStreaming = false;

        public DataWebSocketModule(string urlPath)
            : base(urlPath, true)
        {

        }

        public static void StartStreaming()
        {
            _isStreaming = true;
        }

        public static void StopStreaming()
        {
            _isStreaming = false;
        }

        protected override Task OnMessageReceivedAsync(
            IWebSocketContext context,
            byte[] rxBuffer,
            IWebSocketReceiveResult rxResult)
        {
            // Echo the message back to the sender to test.

            var receivedMessage = Encoding.UTF8.GetString(rxBuffer);
            Console.WriteLine($"Received message from client: {receivedMessage}");

            return SendAsync(context, receivedMessage); // Echo back
        }

        protected override async Task OnClientConnectedAsync(IWebSocketContext context)
        {
            // Send a welcome message
            await SendAsync(context, "Connected to data stream!");

            try
            {
                // Start sending data if streaming is enabled
                while (_isStreaming)
                {
                    // Generate or retrieve the data to be sent
                    var data = GenerateData();

                    // Send the data to the client
                    await SendAsync(context, data);

                    // Optional delay to control the rate of data transmission
                    await Task.Delay(1000);
                }
            }
            catch (WebSocketException ex)
            {
                // Log or handle the exception as needed, e.g., if the connection is closed.
                Console.WriteLine($"WebSocket exception: {ex.Message}");
            }
        }

        protected override Task OnClientDisconnectedAsync(IWebSocketContext context)
        {
            return SendToOthersAsync(context, "A client has disconnected.");
        }

        private Task SendToOthersAsync(IWebSocketContext context, string payload)
        {
            return BroadcastAsync(payload, c => c != context);
        }

        private string GenerateData()
        {
            // Timestamp. Hopefully this is where I can send actual location data
            return DateTime.UtcNow.ToString("O");
        }
    }
}