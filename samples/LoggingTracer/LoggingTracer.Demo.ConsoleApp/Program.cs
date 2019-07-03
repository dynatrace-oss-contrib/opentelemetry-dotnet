﻿namespace LoggingTracer.ConsoleApp
{
    using System.Threading;
    using System.Threading.Tasks;
    using OpenTelemetry.Trace;

    public class Program
    {
        private static ITracer tracer = new LoggingTracer();

        public static async Task Main(string[] args)
        {
            var builder = tracer.SpanBuilder("Main (span1)");
            using (var scope = builder.StartScopedSpan())
            {
                await Task.Delay(100);
                Foo();
            }
        }

        private static async Task Foo()
        {
            var builder = tracer.SpanBuilder("Foo (span2)");
            using (var scope = builder.StartScopedSpan())
            {
                tracer.CurrentSpan.SetAttribute("myattribute", "mvalue");
                await Task.Delay(100);
            }
        }
    }
}
