using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntApp.Domain.Enums
{
    public enum DeviceStatus
    {
        Disconnected,
        Connecting,
        Connected,
        Reconnecting,
        Error
    }
}
