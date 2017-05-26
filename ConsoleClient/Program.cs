using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slalom.Stacks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Logging;

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
    public class Handler : EndPoint
    {
        public override void Receive()
        {
            Console.WriteLine("SSS");
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
            }
        }
    }
}
