using System;
using System.Collections.Generic;
using System.Linq;

namespace MusalaGatewayProject.Models.Dto
{
    public class GatewayDto
    {
        public string GatewayId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual IList<PeripheralDeviceDto> Devices { get; set; }
    }
}
