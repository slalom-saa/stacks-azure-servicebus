using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.AzureServiceBus.Modules;
using Slalom.Stacks.AzureServiceBus.Settings;

namespace Slalom.Stacks
{
    public static class Configuration
    {
        public static Stack UseTopicEventPublishing(this Stack instance, AzureServiceBusSettings settings = null)
        {
            settings = settings ?? new AzureServiceBusSettings();
            instance.Configuration.GetSection("stacks:azureServiceBus")?.Bind(settings);

            instance.Use(e => e.RegisterModule(new TopicEventPublishingModule(settings)));

            return instance;
        }

        public static Stack UseAzureServiceBus(this Stack instance)
        {
            var settings = new AzureServiceBusSettings();
            instance.Configuration.GetSection("stacks:azureServiceBus")?.Bind(settings);

            instance.UseTopicEventPublishing();

            return instance;
        }
    }
}
