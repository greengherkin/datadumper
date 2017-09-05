using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Mqtt.RelayClient.Service
{
    public class RelaySubscriber : SubscriberBase
    {
        public RelaySubscriber(string brokerHostName, string topic, string username = null, string password = null) : base(brokerHostName, topic, username, password)
        {
        }

        protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
