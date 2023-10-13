using Parkomatic.Data;

namespace Parkomatic.Models.BusinessLogicLayer
{
    public class ParkingSpotBusinessLogic
    {
        private readonly ParkingSpotRepository _parkingSpotRepository;

        public ParkingSpotBusinessLogic(ParkingSpotRepository parkingSpotRepository)
        {
            _parkingSpotRepository = parkingSpotRepository;
        }
    }
}
