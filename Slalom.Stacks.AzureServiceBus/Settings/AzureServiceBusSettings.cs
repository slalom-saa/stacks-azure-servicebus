/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;

namespace Slalom.Stacks.AzureServiceBus.Settings
{
    /// <summary>
    /// Settings for Stacks Azure Service Bus.
    /// </summary>
    /// <seealso href="https://azure.microsoft.com/en-us/services/service-bus/"/>
    public class AzureServiceBusSettings
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the topic event publisher settings.
        /// </summary>
        /// <value>
        /// The topic event publisher settings.
        /// </value>
        public TopicEventPublisherSettings EventPublisher { get; set; } = new TopicEventPublisherSettings();
    }
}