using System;
using System.Linq;

namespace MusalaGatewayProject.Models.Dto
{
    public class PeripheralDeviceDto
    {
        public int UID { get; set; }

        public string Vendor { get; set; }

        public DateTime DateCreated { get; set; }

        public int StatusOfDevice { get; set; }

        public string GatewayId { get; set; }

        public Gateway Gateway { get; set; }

    }
}
