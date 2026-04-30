using AntApp.Domain.Entities;
using AntApp.Infra.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntApp.Biz.Services
{
    public class DeviceService
    {
        private readonly TcpDeviceClient _tcpDeviceClient;

        public event Action<Telemetry> OnTelemetry;

        public DeviceService() {
            //_tcpDeviceClient = new TcpDeviceClient("127.0.0.1", 5000);
        }

        //public async Task ConnectAsync()
        //{
        //    _tcpDeviceClient.OnDataReceived += HandleDataReceived;

        //    await _tcpDeviceClient.ConnectAsync("127.0.0.1", 5000);
        //}

        private void HandleDataReceived(string data)
        {
            // 解析数据并创建 Telemetry 对象
            var temperature = double.Parse(data);
            
            var telemetry = new Telemetry
            {
                Timestamp = DateTime.Now,
                Temperature = temperature,
                Pressure = temperature / 2 // 示例压力值
            };

            OnTelemetry?.Invoke(telemetry);
        }
    }
}
