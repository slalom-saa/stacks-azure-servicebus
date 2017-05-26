using System;
using System.Collections.Generic;
using Slalom.Stacks;
using Slalom.Stacks.AzureServiceBus;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Text;

namespace EventProducer
{
    public class SomeEvent : Event
    {
        public string Name { get; }

        public SomeEvent(string name)
        {
            this.Name = name;
        }
    }

    static class Program
    {
        public static void Main()
        {
            Console.Title = "Event Producer";

            using (var stack = new Stack())
            {
                stack.UseAzureServiceBus();

                Console.WriteLine("Press Ctrl + C to exit or Enter to continue...");

                for (int i = 0; ; i++)
                {
                    Console.ReadKey();

                    stack.Publish(new SomeEvent("Event " + i));
                    Console.WriteLine("Event {0} published.", i);
                }
            }
        }
    }
}
