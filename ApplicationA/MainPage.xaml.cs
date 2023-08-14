using EmbedIO;
using EmbedIO.WebApi;
using System.Diagnostics;


namespace ApplicationA
{
    public partial class MainPage : ContentPage
    {
        private WebServer server;

        public MainPage()
        {
            InitializeComponent();
        }
        private void OnStartServerClicked(object sender, EventArgs e)
        {
            if (server == null)
            {
                string url;

                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    url = "http://10.0.2.2:9696/";
                    server = CreateWebServer(url);
                    server.RunAsync();
                    StartServerBtn.Text = "Server Started";
                }
                else
                {
                    url = "http://localhost:9696/";
                    server = CreateWebServer(url);
                    server.RunAsync();
                    StartServerBtn.Text = "Server Started";
                }
                Debug.WriteLine($"Server started at {url}");
            }
        }

        private void OnStartStreamingClicked(object sender, EventArgs e)
        {
            if (server != null)
            {
                DataWebSocketModule.StartStreaming();
                StartStreamingBtn.Text = "Streaming Started";
                TimestampLabel.Text = "Timestamp: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine("Streaming button clicked.");
            }
        }

        private static WebServer CreateWebServer(string url)
        {
            var server = new WebServer(o => o
            .WithUrlPrefix(url)
            .WithMode(HttpListenerMode.EmbedIO))
            .WithWebApi("/api", m => m
                .WithController<StreamingController>())
            .WithModule(new DataWebSocketModule("/data"));

            return server;
        }
    }
}