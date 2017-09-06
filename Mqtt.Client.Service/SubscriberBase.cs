using System;
using System.Linq;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Mqtt.Client.Service
{
    public abstract class SubscriberBase
    {
        public string BrokerHostName { get; }
        public string Topic { get; }
        public string Username { get; }
        public string Password { get; }

        public SubscriberBase(string brokerHostName, string topic, string username = null, string password = null)
        {
            if (string.IsNullOrWhiteSpace(brokerHostName))
            {
                throw new ArgumentException("BrokerHostName cannot be empty", nameof(brokerHostName));
            }

            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Topic cannot be empty", nameof(topic));
            }

            BrokerHostName = brokerHostName;
            Topic = topic;
            Username = username;
            Password = password;
        }

        public void Subscribe()
        {
            // create client instance 
            MqttClient client = new MqttClient(BrokerHostName);

            // register to message received 
            client.MqttMsgPublishReceived += MqttMsgPublishReceived;

            client.Connect(Guid.NewGuid().ToString());

            client.Subscribe(new[] { Topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        protected abstract void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e);
    }
}
