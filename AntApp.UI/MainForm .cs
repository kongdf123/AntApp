using AntApp.Biz.Services;
using AntApp.Domain.Entities;
using AntApp.Domain.Enums;
using AntApp.Infra.Communication;
using System.Collections.Concurrent;
using System.Data;

namespace AntApp.UI
{
    public partial class MainForm : Form
    {
        private TcpDeviceClient _client;

        private CancellationTokenSource _uiCts;
        private BlockingCollection<Telemetry> _queue = new(1000); // 有界队列（防止内存爆）

        public MainForm()
        {
            InitializeComponent();

            lblTemperature.Text = "--";
            lblPressure.Text = "--";
            lblStatus.Text = DeviceStatus.Disconnected.ToString();
        }

        #region 按钮事件

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                _uiCts = new CancellationTokenSource();
                Task.Run(() => UiConsumeLoop(_uiCts.Token));

                _client = new TcpDeviceClient("127.0.0.1", 5000);

                // 订阅事件
                _client.OnDataReceived += OnDataReceived;
                _client.OnStatusChanged += OnStateChanged;

                await _client.StartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动失败: {ex.Message}");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _client?.Stop();

            _uiCts?.Cancel();
            _queue?.CompleteAdding();
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 接收设备数据
        /// </summary>
        private void OnDataReceived(string raw)
        {
            try
            {
                double temp = double.Parse(raw);

                var telemetry = new Telemetry
                {
                    Timestamp = DateTime.Now,
                    Temperature = temp,
                    Pressure = temp / 2
                };

                // 放入队列（非阻塞）
                if (!_queue.TryAdd(telemetry))
                {
                    // 队列满 → 丢弃（工业系统常见策略）
                }
            }
            catch
            {
                // 忽略解析错误（工业场景常见）
            }
        }

        /// <summary>
        /// 状态变化
        /// </summary>
        private void OnStateChanged(DeviceStatus state)
        {
            if (InvokeRequired)
            {
                Invoke(() => UpdateStatus(state));
                return;
            }

            UpdateStatus(state);
        }

        #endregion

        #region UI更新

        private async Task UiConsumeLoop(CancellationToken token)
        {
            foreach (var item in _queue.GetConsumingEnumerable(token))
            {
                // 控制刷新频率（关键！）
                await Task.Delay(200, token); // 5 FPS

                if (IsDisposed) return;

                if (InvokeRequired)
                {
                    Invoke(() => UpdateUI(item));
                }
                else
                {
                    UpdateUI(item);
                }
            }
        }

        private void UpdateUI(Telemetry t)
        {
            if (InvokeRequired)
            {
                Invoke(() => UpdateUI(t));
                return;
            }

            lblTemperature.Text = t.Temperature.ToString("F2");
            lblPressure.Text = t.Pressure.ToString("F2");
        }

        private void UpdateStatus(DeviceStatus state)
        {
            lblStatus.Text = state.ToString();

            // 可选：状态颜色（更像工业软件）
            switch (state)
            {
                case DeviceStatus.Connected:
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    break;
                case DeviceStatus.Connecting:
                case DeviceStatus.Reconnecting:
                    lblStatus.ForeColor = System.Drawing.Color.Orange;
                    break;
                case DeviceStatus.Error:
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    break;
                default:
                    lblStatus.ForeColor = System.Drawing.Color.Gray;
                    break;
            }
        }

        #endregion

        #region 窗体关闭

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _client?.Stop();
            base.OnFormClosing(e);
        }

        #endregion
    }
}
