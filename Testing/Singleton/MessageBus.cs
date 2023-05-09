namespace Singleton
{
    public static class MessageBus
    {
        private static readonly object _lock = new object();
        private static List<Action<string>> _subscribers = new List<Action<string>>();

        public static void Publish(string message)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Invoke(message);
            }
        }

        public static void Subscribe(Action<string> subscriber)
        {
            lock (_lock)
            {
                _subscribers.Add(subscriber);
            }
        }

        public static void Unsubscribe(Action<string> subscriber)
        {
            lock (_lock)
            {
                _subscribers.Remove(subscriber);
            }
        }
    }
}