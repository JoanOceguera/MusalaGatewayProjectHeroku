using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace MusalaGatewayProject.Models
{
    public class Gateway
    {
        [Key, StringLength(256)]
        public string GatewayId { get; set; }

        [Required, StringLength(256)]
        public string Name { get; set; }

        [Required, StringLength(15)]
        public string Address { get; set; }

        [JsonIgnore]
        public virtual IList<PeripheralDevice> Devices { get; set; }



    }
}
