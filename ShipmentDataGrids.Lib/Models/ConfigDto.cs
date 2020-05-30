using ShipmentDataGrids.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShipmentDataGrids.Lib.Models
{
    class ConfigDto
    {
        [JsonProperty(PropertyName = "Shipment", Required = Required.Default)]
        public Config Config { get; set; }
    }
}
