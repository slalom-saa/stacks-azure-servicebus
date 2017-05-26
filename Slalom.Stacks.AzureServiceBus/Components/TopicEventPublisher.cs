using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Slalom.Stacks.AzureServiceBus.Settings;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.AzureServiceBus.Components
{
    public class TopicEventPublisher : IEventPublisher
    {
        private readonly Lazy<TopicClient> _topicClient;

        public TopicEventPublisher(AzureServiceBusSettings settings)
        {
            _topicClient = new Lazy<TopicClient>(() => TopicClient.CreateFromConnectionString(settings.ConnectionString, settings.EventPublisher.TopicName));
        }

        public async Task Publish(params EventMessage[] events)
        {
            await _topicClient.Value.SendBatchAsync(events.Select(message =>
            {
                var content = JsonConvert.SerializeObject(message, DefaultSerializationSettings.Instance);
                return new BrokeredMessage(content) { MessageId = message.Id, ContentType = message.MessageType.FullName };
            }));
        }
    }
}