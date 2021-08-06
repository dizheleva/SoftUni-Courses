namespace Exam.Services
{
    using Exam.Data;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly ExamDbContext data;

        public UserService(ExamDbContext data) => this.data = data;

        public bool IsMechanic(string userId)
            => this.data
                .Users
                .Any(u => u.Id == userId && u.IsMechanic);

        public bool OwnsCar(string userId, string carId)
            => this.data
                .Cars
                .Any(c => c.Id == carId && c.OwnerId == userId);
    }
}
