/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Slalom.Stacks.AzureServiceBus.Components.Batching;
using Slalom.Stacks.AzureServiceBus.Settings;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.AzureServiceBus.Components
{
    /// <summary>
    /// Publishes events to an Azure Topic.
    /// </summary>
    public class TopicEventPublisher : PeriodicBatcher<EventMessage>, IEventPublisher
    {
        private readonly Lazy<TopicClient> _topicClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicEventPublisher"/> class.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="settings"/> argument is null.</exception>
        public TopicEventPublisher(AzureServiceBusSettings settings)
            : base(settings.EventPublisher.BatchSize, settings.EventPublisher.Interval)
        {
            Argument.NotNull(settings, nameof(settings));

            _topicClient = new Lazy<TopicClient>(() => TopicClient.CreateFromConnectionString(settings.ConnectionString, settings.EventPublisher.TopicName));
        }

        /// <inheritdoc />
        public Task Publish(params EventMessage[] events)
        {
            foreach (var instance in events)
            {
                this.Emit(instance);
            }
            return Task.FromResult(0);
        }

        /// <inheritdoc />
        protected override Task EmitBatchAsync(IEnumerable<EventMessage> events)
        {
            return _topicClient.Value.SendBatchAsync(events.Select(message =>
            {
                var content = JsonConvert.SerializeObject(message, DefaultSerializationSettings.Instance);
                return new BrokeredMessage(content) { MessageId = message.Id, ContentType = message.MessageType.FullName };
            }));
        }
    }
}