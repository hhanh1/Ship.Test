using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ship.Common.BaseEntities;

namespace Ship.Bussiness.Entities
{
    public class Ship:BaseEntity
    {
        public string Name { get; set; }
        public double Longitude { get; set; }   
        public double Latitude { get; set; }
        public double Velocity { get; set; }
    }
}
