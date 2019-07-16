﻿// <copyright file="EventWithTime.cs" company="OpenTelemetry Authors">
// Copyright 2018, OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

namespace OpenTelemetry.Trace
{
    using System;
    using OpenTelemetry.Internal;
    using OpenTelemetry.Trace.Export;

    internal class EventWithTime<T>
    {
        private readonly DateTime nanoTime;
        private readonly T @event;

        public EventWithTime(DateTime nanoTime, T @event)
        {
            this.nanoTime = nanoTime;
            this.@event = @event;
        }

        internal ITimedEvent<T> ToSpanDataTimedEvent()
        {
            return TimedEvent<T>.Create(this.nanoTime, this.@event);
        }
    }
}
