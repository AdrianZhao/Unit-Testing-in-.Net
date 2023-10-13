using Parkomatic.Data;

namespace Parkomatic.Models.BusinessLogicLayer
{
    public class VehicleBusinessLogic
    {
        private IRepository<Vehicle> _vehicleRepository;
        public VehicleBusinessLogic(VehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public bool BookParkingSpace(int vehicleId, ParkingSpotRepository parkingSpotRepository, ReservationRepository reservationRepository)
        {
            Vehicle vehicle = _vehicleRepository.Get(vehicleId);

            if (vehicle == null || vehicle.Pass == null)
                return false;

            ParkingSpot parkingSpot = parkingSpotRepository.GetAll().FirstOrDefault(s => !s.Reservations.Any(r => r.VehicleID == vehicleId && r.IsCurrent));

            if (parkingSpot == null)
                return false;

            Reservation currentReservation = reservationRepository.GetAll().FirstOrDefault(r => r.VehicleID == vehicleId && r.ParkingSpotID == parkingSpot.ID && r.IsCurrent);

            if (currentReservation == null)
            {
                var newReservation = new Reservation
                {
                    VehicleID = vehicleId,
                    ParkingSpotID = parkingSpot.ID,
                    Expiry = DateTime.UtcNow.AddHours(2),
                    IsCurrent = true
                };

                reservationRepository.Create(newReservation);

                vehicle.Parked = true;
                parkingSpot.Occupied = true;

                _vehicleRepository.Update(vehicle);
                parkingSpotRepository.Update(parkingSpot);

                return true;
            }

            return false;
        }

        public void LeaveParkingSpace(int vehicleId, ParkingSpotRepository parkingSpotRepository, ReservationRepository reservationRepository)
        {
            Vehicle vehicle = _vehicleRepository.Get(vehicleId);

            if (vehicle == null || vehicle.Pass == null)
                return;

            Reservation currentReservation = reservationRepository.GetAll().FirstOrDefault(r => r.VehicleID == vehicleId && r.IsCurrent);

            if (currentReservation != null)
            {
                vehicle.Parked = false;
                ParkingSpot parkingSpot = parkingSpotRepository.Get(currentReservation.ParkingSpotID);
                parkingSpot.Occupied = false;

                _vehicleRepository.Update(vehicle);
                parkingSpotRepository.Update(parkingSpot);

                currentReservation.IsCurrent = false;
                reservationRepository.Update(currentReservation);
            }
        }
    }
}
