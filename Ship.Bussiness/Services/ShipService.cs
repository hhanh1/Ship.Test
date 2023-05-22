using AutoMapper;

using Ship.Bussiness.Entities;
using Ship.Bussiness.Models;
using Ship.Common.Exceptions;
using Ship.Common.Models;
using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using Ship.Common.Extentions;

namespace Ship.Bussiness.Services
{
    public interface IShipService
    {
        Task Add(ShipViewModel model);
        Task<PageList<ShipReponses>>Search(SearchShipViewModel model);
      
        Task<string> EstimationArrival(EstimatedArrivalModel model);
        Task UpdateVelocity(Guid id, double velocity);
    }
    public class ShipService : IShipService
    {
        private TestContext _context;
        private IMapper _mapper;

        public ShipService(TestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(ShipViewModel model)
        {
            var ship = _mapper.Map<Entities.Ship>(model);
            _context.Ships.Add(ship);
            await _context.SaveChangesAsync();
        }

        public async Task<string> EstimationArrival(EstimatedArrivalModel model)
        {
            var ship = await _context.Ships.FirstOrDefaultAsync(i => i.Id == model.ShipId);
            if (ship == null)
                throw new NotFoundException("Ship Not Found");
            var port = await _context.Ports.FirstOrDefaultAsync(i => i.Id == model.PortId);
            if (port == null)
                throw new NotFoundException("Port Not Found");

            var shipCoord = new GeoCoordinate(ship.Latitude,ship.Longitude);
            var portCoord = new GeoCoordinate(port.Latitude, port.Longitude);
            double distanceBetween = shipCoord.GetDistanceTo(portCoord);
            return $"{distanceBetween / ship.Velocity} hours";
        }

        public async Task<PageList<ShipReponses>> Search(SearchShipViewModel model)
        {
            var query = _context.Ships.AsQueryable();
            if (!string.IsNullOrEmpty(model.Name))
            {
                query = query.Where(x => x.Name.Trim().ToLower().Contains(model.Name.ToLower().Trim()));
            }
            return _mapper.Map<PageList<ShipReponses>>(await query.ToPageList(model));
        }

        public async Task Update(Guid id,ShipViewModel model)
        {
            var ship = await _context.Ships.FirstOrDefaultAsync(i => i.Id == id);
            if (ship == null)
                throw new NotFoundException();
             _mapper.Map(model,ship);
            _context.SaveChanges();
        }

       

        public async Task UpdateVelocity(Guid id, double velocity)
        {
            var ship =await _context.Ships.FirstOrDefaultAsync(i=>i.Id == id);
            if(ship == null)
                throw new NotFoundException();
            ship.Velocity = velocity;
            _context.SaveChanges();
        }
    }
   
}
