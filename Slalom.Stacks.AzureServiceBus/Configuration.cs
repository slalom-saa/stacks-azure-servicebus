/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using System.Linq;
using Autofac;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.AzureServiceBus.Components;
using Slalom.Stacks.AzureServiceBus.Modules;
using Slalom.Stacks.AzureServiceBus.Settings;

namespace Slalom.Stacks.AzureServiceBus
{
    /// <summary>
    /// Configures Azure Service Bus components.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Configures all the Azure Service Bus components.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public static Stack UseAzureServiceBus(this Stack instance)
        {
            var settings = new AzureServiceBusSettings();
            instance.Configuration.GetSection("stacks:azureServiceBus")?.Bind(settings);

            instance.Use(builder =>
            {
                if (settings.EventPublisher?.TopicName != null)
                {
                    builder.RegisterModule(new TopicEventPublishingModule(settings));
                }
                if (settings.Subscriptions.Any())
                {
                    builder.RegisterModule(new TopicSubscriptionModule(settings));
                }
            });

            if (settings.Subscriptions.Any())
            {
                instance.Container.Resolve<TopicSubscription>();
            }

            return instance;
        }
    }
}