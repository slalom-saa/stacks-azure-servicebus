using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slalom.Stacks;
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

    static class Program
    {
        public static void Main()
        {
            using (var stack = new Stack())
            {
                stack.UseAzureServiceBus();

                stack.Publish(new SomeEvent("name"));

                Console.ReadKey();
            }
        }
    }
}
