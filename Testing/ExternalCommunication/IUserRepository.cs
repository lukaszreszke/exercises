namespace ExternalCommunication;

public interface IUserRepository
{
    User FindById(int signeeId);
    void SaveChanges();
}