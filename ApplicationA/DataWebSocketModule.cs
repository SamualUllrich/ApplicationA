﻿using EmbedIO.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ApplicationA
{
    public class DataWebSocketModule : WebSocketModule
    {
        private static bool _isStreaming = false;

        public DataWebSocketModule(string urlPath)
            : base(urlPath, true) {}

        public static void StartStreaming()
        {
            _isStreaming = true;
            Debug.WriteLine("Streaming started");

        }

        public static void StopStreaming()
        {
            _isStreaming = false;
            Debug.WriteLine("Streaming stopped");
        }

        protected override Task OnMessageReceivedAsync(
            IWebSocketContext context,
            byte[] rxBuffer,
            IWebSocketReceiveResult rxResult)
        {
            var receivedMessage = Encoding.UTF8.GetString(rxBuffer);
            Console.WriteLine($"Received message from client: {receivedMessage}");

            return SendAsync(context, receivedMessage); // Echo back
        }

        protected override async Task OnClientConnectedAsync(IWebSocketContext context)
        {
            await SendAsync(context, "Connected to data stream");
            Debug.WriteLine("Client connected");

            try
            {
                while (_isStreaming)
                {
                    var data = GenerateData();
                    Debug.WriteLine($"Streaming data: {data}");

                    await SendAsync(context, data);
                    await Task.Delay(1000);
                }
            }
            catch (WebSocketException ex)
            {
                Debug.WriteLine($"WebSocket exception: {ex.Message}");
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