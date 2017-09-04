using System;

namespace Mqtt.RelayClient.Service
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
                throw new ArgumentException("BrokerHostName cannot be null", nameof(brokerHostName));
            }

            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Topic cannot be null", nameof(topic));
            }

            BrokerHostName = brokerHostName;
            Topic = topic;
            Username = username;
            Password = password;
        }

        public void Subscribe()
        {
        }
    }
}
