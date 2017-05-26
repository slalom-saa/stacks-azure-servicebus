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
    /// Settings for the topic event publisher.
    /// </summary>
    public class TopicEventPublisherSettings
    {
        /// <summary>
        /// Gets or sets the size of the batch.
        /// </summary>
        /// <value>
        /// The size of the batch.
        /// </value>
        public int BatchSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the interval duration.
        /// </summary>
        /// <value>
        /// The interval duration.
        /// </value>
        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Gets or sets the name of the topic.
        /// </summary>
        /// <value>
        /// The name of the topic.
        /// </value>
        public string TopicName { get; set; } = "Events";
    }
}