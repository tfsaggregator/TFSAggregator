﻿namespace Aggregator.ConsoleApp
{
    using Aggregator.Core;
    using Aggregator.Core.Monitoring;

    class ConsoleEventLogger : TextLogComposer
    {
        public ConsoleEventLogger(LogLevel level)
            : base(new ConsoleTextLogger(level))
        { }
    }
}
