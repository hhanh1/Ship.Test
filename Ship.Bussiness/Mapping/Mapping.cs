using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ship.Bussiness.Entities;
using Ship.Bussiness.Models;
using Ship.Common.Models;

namespace Ship.Bussiness
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<ShipViewModel, Entities.Ship>();
            CreateMap< Entities.Ship, ShipReponses>();
            CreateMap<PageList<Entities.Ship>,PageList<ShipReponses>>();
        }
    }
}
