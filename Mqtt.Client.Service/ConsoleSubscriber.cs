using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Mqtt.Client.Service
{
    public class ConsoleSubscriber : SubscriberBase
    {
        public ConsoleSubscriber(string brokerHostName, string topic, string username = null, string password = null) : base(brokerHostName, topic, username, password)
        {
        }

        protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                var message = Encoding.Default.GetString(e.Message);
                var payload = JsonConvert.DeserializeObject(message);
                var data = new { DateTime = DateTime.Now, Topic = e.Topic, Payload = payload };
                Console.WriteLine(JsonConvert.SerializeObject(data));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
