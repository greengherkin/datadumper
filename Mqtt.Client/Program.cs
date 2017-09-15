using System;
using System.IO;
using Mqtt.Client.Service;
using Newtonsoft.Json;

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

            var configFileContents = File.ReadAllText(@"D:\development\mqtt.relayclient.config");
            dynamic configuration = Newtonsoft.Json.Linq.JObject.Parse(configFileContents);
            var relaySubscriber = new RelaySubscriber(configuration.RelayServer.Value, Guid.NewGuid().ToString(), configuration.Topic.Value, configuration.Username.Value, configuration.Password.Value.ToString(), "localhost", "cellar/dht", null, null);
            relaySubscriber.Subscribe();
        }
    }
}
