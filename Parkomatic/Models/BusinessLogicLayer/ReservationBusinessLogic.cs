using Parkomatic.Data;

namespace Parkomatic.Models.BusinessLogicLayer
{
    public class ReservationBusinessLogic
    {
        private readonly ReservationRepository _reservationRepository;

        public ReservationBusinessLogic(ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
    }
}
