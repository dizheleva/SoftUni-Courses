using SharedTrip.ViewModels.Trips;
using SharedTrip.ViewModels.Users;

namespace SharedTrip.Services
{
    public interface IValidator
    {
        bool ValidateUser(RegisterUserFormModel model);

        bool ValidateTrip(AddTripFormModel model);
    }
}
