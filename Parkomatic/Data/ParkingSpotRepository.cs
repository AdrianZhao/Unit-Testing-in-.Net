using Parkomatic.Models;

namespace Parkomatic.Data
{
    public class ParkingSpotRepository : IRepository<ParkingSpot>
    {
        private readonly ParkomaticContext _context;

        public ParkingSpotRepository(ParkomaticContext context)
        {
            _context = context;
        }

        public ParkingSpot Get(int id)
        {
            return _context.ParkingSpots.Find(id);
        }

        public ICollection<ParkingSpot> GetAll()
        {
            return _context.ParkingSpots.ToList();
        }

        public ParkingSpot Create(ParkingSpot entity)
        {
            _context.ParkingSpots.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public ParkingSpot Update(ParkingSpot entity)
        {
            _context.ParkingSpots.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(ParkingSpot entity)
        {
            _context.ParkingSpots.Remove(entity);
            _context.SaveChanges();
        }
    }
}
