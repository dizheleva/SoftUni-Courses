namespace Exam.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
