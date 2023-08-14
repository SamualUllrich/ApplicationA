using EmbedIO.Routing;
using EmbedIO.WebApi;
using EmbedIO.WebSockets;
using EmbedIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationA
{

    public class StreamingController : WebApiController
    {
        [Route(HttpVerbs.Get, "/start")]
        public object StartStreaming()
        {
            DataWebSocketModule.StartStreaming();
            return new { Message = "Streaming Started" };
        }
    }
}
