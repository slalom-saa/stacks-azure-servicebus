/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.AzureServiceBus.Modules;
using Slalom.Stacks.AzureServiceBus.Settings;

namespace Slalom.Stacks
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

            instance.UseTopicEventPublishing();

            return instance;
        }

        /// <summary>
        /// Configures Azure Topic event publishing.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="settings">The settings to use.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public static Stack UseTopicEventPublishing(this Stack instance, AzureServiceBusSettings settings = null)
        {
            settings = settings ?? new AzureServiceBusSettings();
            instance.Configuration.GetSection("stacks:azureServiceBus")?.Bind(settings);

            instance.Use(e => e.RegisterModule(new TopicEventPublishingModule(settings)));

            return instance;
        }
    }
}