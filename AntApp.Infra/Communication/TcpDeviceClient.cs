using AntApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AntApp.Infra.Communication
{
    public class TcpDeviceClient
    {
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;

        private CancellationTokenSource _cts;
        public DeviceStatus State { get; private set; } = DeviceStatus.Disconnected;


        public event Action<string> OnDataReceived;

        public event Action<DeviceStatus> OnStatusChanged;

        private readonly string _ip;
        private readonly int _port;
        public TcpDeviceClient(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public async Task StartAsync()
        {
            _cts = new CancellationTokenSource();
           
            _ = Task.Run(() => ConnectionLoopAsync(_cts.Token));
        }

        public void Stop()
        {
            _cts?.Cancel();
            SetState(DeviceStatus.Disconnected);
        }

        private async Task ConnectionLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    SetState(DeviceStatus.Connecting);

                    _tcpClient = new TcpClient();
                    await _tcpClient.ConnectAsync(_ip, _port);

                    _networkStream = _tcpClient.GetStream();

                    SetState(DeviceStatus.Connected);

                    var receiveTask = ReceiveLoopAsync(token);
                    var heartbeatTask = HeartbeatLoopAsync(token);

                    await Task.WhenAny(receiveTask, heartbeatTask);

                    throw new Exception("Connection lost");
                }
                catch
                {
                    SetState(DeviceStatus.Reconnecting);
                    await Task.Delay(3000, token); // 等待5秒后重试连接
                }
            }
        }

        private void SetState(DeviceStatus state)
        {
            State = state;
            OnStatusChanged?.Invoke(state);
        }

        //public async Task ConnectAsync(string ipAddress, int port)
        //{
        //    _tcpClient = new TcpClient();
        //    await _tcpClient.ConnectAsync(ipAddress, port);
        //    _networkStream = _tcpClient.GetStream();

        //    _ = Task.Run(ReceiveLoopAsync);
        //}

        private async Task HeartbeatLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(5000, token); // 每5秒发送一次心跳

                    if (_tcpClient?.Connected != true)
                    {
                        throw new Exception("Heartbeat failed");
                    }
                    else
                    {
                        byte[] heartbeatMsg = Encoding.UTF8.GetBytes("HEARTBEAT");
                        await _networkStream.WriteAsync(heartbeatMsg, 0, heartbeatMsg.Length, token);
                    }
                }
                catch
                {
                    // 发送心跳失败，可能连接已断开
                    SetState(DeviceStatus.Error);
                }
            }
        }

        private async Task ReceiveLoopAsync(CancellationToken token)
        {
            byte[] buffer = new byte[1024];
            while (!token.IsCancellationRequested)
            {
                int len = await _networkStream.ReadAsync(buffer, 0, buffer.Length);
                if (len == 0)
                {
                    throw new Exception("Connection closed by remote host");
                }

                string msg = Encoding.UTF8.GetString(buffer, 0, len);
                OnDataReceived?.Invoke(msg);
            }
        }
    }
}
