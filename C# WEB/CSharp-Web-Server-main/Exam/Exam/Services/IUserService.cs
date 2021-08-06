namespace Exam.Services
{
    public interface IUserService
    {
        bool IsMechanic(string userId);

        bool OwnsCar(string userId, string carId);
    }
}
