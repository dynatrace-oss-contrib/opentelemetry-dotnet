﻿using System.Threading;
using OpenTelemetry.Common;
using OpenTelemetry.Trace;

namespace LoggingTracer
{

    public static class CurrentSpanUtils
    {
        private static AsyncLocal<ISpan> asyncLocalContext = new AsyncLocal<ISpan>();

        public static ISpan CurrentSpan
        {
            get
            {
                return asyncLocalContext.Value;
            }
        }

        public class LoggingScope : IScope
        {
            private readonly ISpan origContext;
            private readonly ISpan span;
            private readonly bool endSpan;

            public LoggingScope(ISpan span, bool endSpan = true)
            {
                this.span = span;
                this.endSpan = endSpan;
                this.origContext = CurrentSpanUtils.asyncLocalContext.Value;
                CurrentSpanUtils.asyncLocalContext.Value = span;
            }

            public void Dispose()
            {
                Logger.Log($"Scope.Dispose");
                var current = asyncLocalContext.Value;
                asyncLocalContext.Value = this.origContext;

                if (current != this.origContext)
                {
                    // Log
                }

                if (this.endSpan)
                {
                    this.span.End();
                }
            }
        }
    }


}


