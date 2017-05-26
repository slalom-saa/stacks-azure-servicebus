﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks;
using Slalom.Stacks.AzureServiceBus;
using Slalom.Stacks.AzureServiceBus.Components;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Text;

namespace ConsoleClient
{
    public class SomeEvent : Event
    {
        public string Name { get; }

        public SomeEvent(string name)
        {
            this.Name = name;
        }
    }

    [Subscribe("SomeEvent")]
    public class Handler : EndPoint<SomeEvent>
    {
        public override void Receive(SomeEvent instance)
        {
            instance.OutputToJson();
        }
    }

    static class Program
    {
        public static void Main()
        {
            using (var stack = new Stack())
            {
                stack.UseAzureServiceBus();

                stack.Publish(new SomeEvent("one"));

                Console.WriteLine("...");
                Console.ReadKey();
            }
        }
    }
}
