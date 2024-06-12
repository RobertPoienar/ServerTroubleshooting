using AltServerWebSocketSharp;
using AltServerWebSocketSharp.Net.WebSockets;
using AltServerWebSocketSharp.Server;
public class Server
{
    readonly WebSocketServer _wsServer;
    private string _host;
    private int _port;
    public Server(string host = "0.0.0.0", int port = 13000)
    {
        _host = host;
        _port = port;
        _wsServer = new WebSocketServer(GetServerUri(host, port).ToString());
        _wsServer.AddWebSocketService<SocketHandler>("/app", Init);

    }
    private void Init(WebSocketContext context, SocketHandler appWebSocketHandler)
    {
        appWebSocketHandler.Init(this);
    }

    public static Uri GetServerUri(string host, int port)
    {
        if (!Uri.TryCreate(string.Format("ws://{0}:{1}/", host, port), UriKind.Absolute, out Uri uri))
        {
            throw new Exception($"Invalid host or port {host}:{port}.");
        }

        return uri;
    }

    internal void Start()
    {
        _wsServer.Start();
        Console.WriteLine($"Server started listening on {_host}:{_port}/app");
    }
}

internal class SocketHandler : WebSocketBehavior
{
    internal void Init(Server server)
    {
        Console.WriteLine("Socket connection initiated");
    }
    protected override void OnOpen()
    {
        Console.WriteLine("Socket connection Connected");
    }

    protected override void OnClose(CloseEventArgs e)
    {
        Console.WriteLine($"Socket closed with code {e.Code} because: {e.Reason}");
    }
    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"I received the following message {e.Data}");
    }
}