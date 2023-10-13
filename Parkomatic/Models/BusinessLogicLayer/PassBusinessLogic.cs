using Parkomatic.Data;

namespace Parkomatic.Models.BusinessLogicLayer
{
    public class PassBusinessLogic
    {
        private readonly PassRepository _passRepository;

        public PassBusinessLogic(PassRepository passRepository)
        {
            _passRepository = passRepository;
        }

        public bool BuyPass(int passId, int vehicleId)
        {
            Pass pass = _passRepository.Get(passId);
            Vehicle vehicle = _passRepository._context.Vehicles.Find(vehicleId);

            if (pass == null || vehicle == null)
                return false;

            if (pass.Vehicles.Count < pass.Capacity)
            {
                if (!pass.Vehicles.Contains(vehicle))
                {
                    pass.Vehicles.Add(vehicle);
                    _passRepository.Update(pass);
                    return true;
                }
            }

            return false;
        }
    }
}
