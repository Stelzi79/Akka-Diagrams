using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

namespace AkkaDiagram.Actors.Messages
{
    internal class RegisteringUnsubscriber : HandleMessageBase<RegisteringUnsubscriber>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^registering unsubscriber with (?'EventStreamType'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        private Type UnsubscriberType => Origin.LogClass;

        private RegisteringUnsubscriber(Debug origin)
            : base(origin)
        {
        }

        public string Tag => nameof(RegisteringUnsubscriber);

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{Origin.Timestamp}] - with {UnsubscriberType.FullName}", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }

        public static RegisteringUnsubscriber? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new RegisteringUnsubscriber(debugMsg),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
