namespace Ecommerce
{
    public interface IMailService
    {
        void SendOrderConfirmation(Order order);
    }
}