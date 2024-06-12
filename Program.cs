public class Program
{

    static readonly CancellationTokenSource __cts = new CancellationTokenSource();
    public static async Task Main(string[] args)
    {
        Server server = new Server("0.0.0.0", 13000);
        server.Start();
        await Task.Delay(Timeout.Infinite, __cts.Token).ConfigureAwait(false);
    }
}