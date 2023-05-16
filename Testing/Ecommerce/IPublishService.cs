namespace Ecommerce
{
    public interface IPublishService
    {
        void Publish(OrderPlacedMessage orderPlacedMessage);
    }

    public class PublishService : IPublishService
    {
        public void Publish(OrderPlacedMessage orderPlacedMessage)
        {
            MessageBus.Publish(orderPlacedMessage);
        }
    }
}