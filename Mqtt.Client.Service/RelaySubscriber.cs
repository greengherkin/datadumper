using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Mqtt.Client.Service
{
    public class RelaySubscriber : SubscriberBase
    {
        public string UpstreamBrokerHostName { get; }
        public string UpstreamClientName { get; }
        public string UpstreamTopic { get; }
        public string UpstreamUsername { get; }
        public string UpstreamPassword { get; }

        public RelaySubscriber(string upstreamBrokerHostName, string upstreamClientName, string upstreamTopic, string upstreamUsername, string upstreamPassword, string brokerHostName, string topic, string username = null, string password = null) : base(brokerHostName, topic, username, password)
        {
            if (string.IsNullOrEmpty(upstreamBrokerHostName))
            {
                throw new ArgumentException("UpstreamBrokerHostName cannot be empty", nameof(upstreamBrokerHostName));
            }

            if (string.IsNullOrEmpty(upstreamClientName))
            {
                throw new ArgumentException("UpstreamClientName cannot be empty", nameof(upstreamClientName));
            }

            if (string.IsNullOrEmpty(upstreamTopic))
            {
                throw new ArgumentException("UpstreamTopic cannot be empty", nameof(upstreamTopic));
            }

            if (string.IsNullOrEmpty(upstreamUsername))
            {
                throw new ArgumentException("UpstreamUsername cannot be empty", nameof(upstreamUsername));
            }

            if (string.IsNullOrEmpty(upstreamPassword))
            {
                throw new ArgumentException("UpstreamPassword cannot be empty", nameof(upstreamPassword));
            }

            UpstreamBrokerHostName = upstreamBrokerHostName;
            UpstreamClientName = upstreamClientName;
            UpstreamTopic = upstreamTopic;
            UpstreamUsername = upstreamUsername;
            UpstreamPassword = upstreamPassword;
        }

        protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // create client instance 
            MqttClient client = new MqttClient(UpstreamBrokerHostName);
            client.Connect(UpstreamClientName, UpstreamUsername, UpstreamPassword);
            client.Publish(UpstreamTopic, e.Message);
        }
    }
}
