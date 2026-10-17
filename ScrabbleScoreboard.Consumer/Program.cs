using RabbitMQ.Client;
using ScrabbleScoreboad.Consumer;

//await SimpleSendReceive.Demo1();
await FanOut.Demo1();
Console.WriteLine("Producer Demo Run Complete.");
Console.ReadLine();

return;