namespace ExternalCommunication;

public interface ISignOrderRepository
{
    SignOrder Create();
    SignOrder GetById(int orderId);
    List<SignOrder> GetSignOrders();
    void SaveChanges();
}