using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Mqtt.Client.Service
{
    public class FileWriterSubscriber : SubscriberBase
    {
        public string DataFilePath { get; }

        public FileWriterSubscriber(string dataFilePath, string brokerHostName, string topic, string username = null, string password = null) : base(brokerHostName, topic, username, password)
        {
            if (string.IsNullOrWhiteSpace(dataFilePath))
            {
                throw new ArgumentException("DataFilePath cannot be empty", nameof(dataFilePath));
            }
            DataFilePath = dataFilePath;
        }

        protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var message = Encoding.Default.GetString(e.Message);
            Console.WriteLine(message);
            var path = Path.Combine(DataFilePath, $"{Topic.Replace("/", "-")}_" + DateTime.Now.ToString("yyyyMMdd") + ".dat");
            var payload = JsonConvert.DeserializeObject(message);
            var data = new { DateTime = DateTime.Now, Topic = e.Topic, Payload = payload };
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(JsonConvert.SerializeObject(data));
            }
        }
    }
}
