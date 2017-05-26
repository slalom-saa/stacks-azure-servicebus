using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Slalom.Stacks.AzureServiceBus.Settings;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.AzureServiceBus.Components
{
    public class TopicSubscription : IDisposable
    {
        private readonly IMessageGateway _messages;
        private readonly List<SubscriptionClient> _clients = new List<SubscriptionClient>();

        public TopicSubscription(IMessageGateway messages, AzureServiceBusSettings settings)
        {
            _messages = messages;

            foreach (var subscription in settings.Subscriptions)
            {
                this.CreateClient(settings.ConnectionString, subscription.TopicName, subscription.SubscriptionName);
            }
        }

        private void CreateClient(string connectionString, string topic, string subscription)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.SubscriptionExists(topic, subscription))
            {
                namespaceManager.CreateSubscription(topic, subscription);
            }

            var client = SubscriptionClient.CreateFromConnectionString(connectionString, topic, subscription);

            client.OnMessage(this.Consume);

            _clients.Add(client);
        }

        private void Consume(BrokeredMessage message)
        {
            var content = message.GetBody<string>();
            var instance = JsonConvert.DeserializeObject<JObject>(content, DefaultSerializationSettings.Instance);
            _messages.Publish(instance["name"].ToString(), content);
            message.Complete();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clients.ForEach(e => e.Close());
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
