using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace MusalaGatewayProject.Models
{

    public enum Status
    {
        Offline = 0,
        Online = 1
    }

    public class PeripheralDevice
    {
        [Key]
        public int UID { get; set; }

        [Required]
        public string Vendor { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [Required]
        public Status StatusOfDevice { get; set; }

        [Required, StringLength(256)]
        public string GatewayId { get; set; }

        [JsonIgnore]
        public Gateway Gateway { get; set; }

    }
}
