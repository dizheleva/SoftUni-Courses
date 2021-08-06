namespace Exam.Services
{
    using System.Collections.Generic;
    using Exam.Models.Cars;
    using Exam.Models.Users;

    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);

        ICollection<string> ValidateCar(AddCarFormModel model);

        
    }
}
