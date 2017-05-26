using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.AzureServiceBus.Components;
using Slalom.Stacks.AzureServiceBus.Settings;

namespace Slalom.Stacks.AzureServiceBus.Modules
{
    public class TopicEventPublishingModule : Module
    {
        private readonly AzureServiceBusSettings _settings;

        public TopicEventPublishingModule(AzureServiceBusSettings settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TopicEventPublisher>()
                .WithParameter(new TypedParameter(typeof(AzureServiceBusSettings), _settings))
                .AsSelf().AsImplementedInterfaces().SingleInstance();
        }
    }
}
