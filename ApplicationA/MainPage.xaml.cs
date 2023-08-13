using EmbedIO;
using EmbedIO.WebApi;

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
                var url = "http://localhost:9696/";
                server = CreateWebServer(url);
                server.RunAsync();
                StartServerBtn.Text = "Server Started";
            }
        }

        private void OnStartStreamingClicked(object sender, EventArgs e)
        {
            if (server != null)
            {
                DataWebSocketModule.StartStreaming();
                StartStreamingBtn.Text = "Streaming Started";

                TimestampLabel.Text = "Timestamp: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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