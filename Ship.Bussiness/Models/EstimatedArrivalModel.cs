using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Bussiness.Models
{
    public class EstimatedArrivalModel
    {
        [Required]
        public Guid ShipId { get; set; }
        [Required]
        public Guid PortId { get; set; }
    }
}
