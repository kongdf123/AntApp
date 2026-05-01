using System.Net.Sockets;
using System.Net;
using System.Text;

namespace AntApp.Simulator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "AntApp TCP Simulator";

            var port = 5000;
            var listener = new TcpListener(IPAddress.Any, port);

            listener.Start();

            Console.WriteLine($"[Simulator] Started on port {port}...");
            Console.WriteLine("[Simulator] Press Ctrl+C to stop.");

            var cts = new CancellationTokenSource();

            // Ctrl+C 优雅退出
            Console.CancelKeyPress += (s, e) =>
            {
                Console.WriteLine("Stopping...");
                e.Cancel = true;
                cts.Cancel();
                listener.Stop();
            };

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    var client = await listener.AcceptTcpClientAsync(cts.Token);

                    Console.WriteLine($"[Simulator] Client connected: {client.Client.RemoteEndPoint}");

                    _ = Task.Run(() => HandleClientAsync(client, cts.Token));
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Server stopped.");
            }
        }

        private static async Task HandleClientAsync(TcpClient client, CancellationToken token)
        {
            var rand = Random.Shared; // ✅ 线程安全

            try
            {
                using var stream = client.GetStream();

                while (!token.IsCancellationRequested)
                {
                    double temp = rand.Next(20, 100);
                    double pressure = temp / 2;

                    // ✅ 加换行（解决粘包）
                    var msg = $"{{\"Temperature\":{temp:F2},\"Pressure\":{pressure:F2}}}\n";

                    var data = Encoding.UTF8.GetBytes(msg);

                    await stream.WriteAsync(data, token);

                    Console.WriteLine($"[Send] {msg.Trim()}");

                    await Task.Delay(1000, token);

                    // ✅ 模拟断线
                    if (rand.NextDouble() < 0.05)
                    {
                        Console.WriteLine("[Simulator] Simulated disconnect!");
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 正常退出
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Simulator] Client error: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("[Simulator] Client disconnected");
            }
        }
    }
}
