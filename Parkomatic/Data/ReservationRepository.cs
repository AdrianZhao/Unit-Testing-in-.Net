using Parkomatic.Models;

namespace Parkomatic.Data
{
    public class ReservationRepository : IRepository<Reservation>
    {
        private readonly ParkomaticContext _context;

        public ReservationRepository(ParkomaticContext context)
        {
            _context = context;
        }

        public Reservation Get(int id)
        {
            return _context.Reservations.Find(id);
        }

        public ICollection<Reservation> GetAll()
        {
            return _context.Reservations.ToList();
        }

        public Reservation Create(Reservation entity)
        {
            _context.Reservations.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Reservation Update(Reservation entity)
        {
            _context.Reservations.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Reservation entity)
        {
            _context.Reservations.Remove(entity);
            _context.SaveChanges();
        }
    }
}
