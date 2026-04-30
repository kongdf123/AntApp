using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntApp.Domain.Entities
{
    public class Telemetry
    {
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
    }
}
