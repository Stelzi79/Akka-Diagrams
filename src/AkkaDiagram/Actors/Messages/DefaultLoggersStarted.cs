using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    public class DefaultLoggersStarted : HandleMessageBase<DefaultLoggersStarted>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^Default Loggers started$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        private DefaultLoggersStarted(Debug origin)
            : base(origin)
        {
        }

        public string Tag => nameof(DefaultLoggersStarted);

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{Origin.Timestamp}]", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }

        public static DefaultLoggersStarted? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new DefaultLoggersStarted(debugMsg),
                debugMsg?.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
