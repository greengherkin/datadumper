using Mqtt.Client.Service;

namespace MQTT.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongoSubscriber = new MongoDbDataWriterSubscriber("HomeAutomation", "SensorData", "localhost", "cellar/dht");
            mongoSubscriber.Subscribe();
            var consoleSubscriber = new ConsoleSubscriber("localhost", "cellar/dht");
            consoleSubscriber.Subscribe();
        }
    }
}
