
using ScrabbleScoreboard.Producer;

//await SimpleSendReceive.Demo1();
await FanOut.Demo1();
await Choreography.Demo1();
Console.WriteLine("Producer Demo Run Complete.");
Console.ReadLine();

return;