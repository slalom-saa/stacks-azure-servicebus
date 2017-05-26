using System;
using Slalom.Stacks;
using Slalom.Stacks.AzureServiceBus;
using Slalom.Stacks.Services;
using Slalom.Stacks.Text;

namespace EventConsumer
{
    public class SomeEvent
    {
        public SomeEvent(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
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
            Console.Title = "Event Consumer";

            using (var stack = new Stack())
            {
                stack.UseAzureServiceBus();

                Console.WriteLine("Waiting for events...");
                Console.ReadKey();
            }
        }
    }
}