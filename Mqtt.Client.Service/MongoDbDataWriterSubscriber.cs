using System;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Mqtt.Client.Service
{
    public class MongoDbDataWriterSubscriber : SubscriberBase
    {
        public string DatabaseName { get; }
        public string Collection { get; }

        public MongoDbDataWriterSubscriber(string databaseName, string collection, string brokerHostName, string topic, string username = null, string password = null) : base(brokerHostName, topic, username, password)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentException("MongoDbDatabaseName cannot be empty", nameof(databaseName));
            }

            if (string.IsNullOrWhiteSpace(collection))
            {
                throw new ArgumentException("Collection cannot be empty", nameof(collection));
            }

            DatabaseName = databaseName;
            Collection = collection;
        }

        protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                IMongoClient client = new MongoClient();
                IMongoDatabase database = client.GetDatabase(DatabaseName);

                var payload = Encoding.Default.GetString(e.Message);
                var document = new BsonDocument
                {
                    { "DateTime", DateTime.Now },
                    { "Topic", e.Topic },
                    { "Payload", BsonDocument.Parse(payload) }
                };
                var collection = database.GetCollection<BsonDocument>(Collection);
                collection.InsertOne(document);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
