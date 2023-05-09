using Singleton;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

MessageBus.Subscribe(message => Console.WriteLine($"Recieved: {message}"));

MessageBus.Publish("Message from the future");
