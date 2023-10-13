using Parkomatic.Models;

namespace Parkomatic.Data
{
    public class VehicleRepository : IRepository<Vehicle>
    {
        private readonly ParkomaticContext _context;

        public VehicleRepository(ParkomaticContext context)
        {
            _context = context;
        }

        public Vehicle Get(int id)
        {
            return _context.Vehicles.Find(id);
        }

        public ICollection<Vehicle> GetAll()
        {
            return _context.Vehicles.ToList();
        }

        public Vehicle Create(Vehicle entity)
        {
            _context.Vehicles.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Vehicle Update(Vehicle entity)
        {
            _context.Vehicles.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Vehicle entity)
        {
            _context.Vehicles.Remove(entity);
            _context.SaveChanges();
        }
    }
}
