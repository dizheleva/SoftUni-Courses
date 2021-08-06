using System.Linq;
using System.Text.RegularExpressions;
using SharedTrip.ViewModels.Trips;
using SharedTrip.ViewModels.Users;

namespace SharedTrip.Services
{
    public class Validator : IValidator
    {
        public bool ValidateUser(RegisterUserFormModel user)
        {
            if (user.Username == null || user.Username.Length is < 5 or > 20)
            {
                return false;
            }

            var userEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            if (user.Email == null || !Regex.IsMatch(user.Email, userEmailRegularExpression))
            {
                return false;
            }

            if (user.Password == null || user.Password.Length is < 6 or > 20)
            {
                return false;
            }

            if (user.Password != null && user.Password.Any(x => x == ' '))
            {
                return false;
            }

            if (user.Password != user.ConfirmPassword)
            {
                return false;
            }

            return true;
        }

        public bool ValidateTrip(AddTripFormModel trip)
        {
            return true;
        }
    }
}
