using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ship.Common.Models;

namespace Ship.Bussiness.Models
{
    public class SearchShipViewModel : BaseSearchViewModel
    {
        public string? Name { get; set; }
    }
    public class ShipViewModel
    {
        [Required]
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Velocity { get; set; }
    }
    public class ShipReponses: ShipViewModel
    {
        public Guid Id { get; set; }
     
    }
}
