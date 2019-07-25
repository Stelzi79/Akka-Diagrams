using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

namespace AkkaDiagram.Actors.Messages
{
    internal class ReceivedHandledMessage : HandleMessageBase<ReceivedHandledMessage>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^received handled message (?'message'.*) from (?'fromActor'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly string _Message;
        private readonly string _FromActor;

        private string ReceivedActor => Origin.LogSource;

        public string Tag => nameof(ReceivedHandledMessage);

        public ReceivedHandledMessage(Debug origin, string message, string fromActor)
            : base(origin)
        {
            _Message = message;
            _FromActor = fromActor;
        }

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{Origin.Timestamp}] - [{ReceivedActor}] handled message '{_Message}' from [{_FromActor}]", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }

        public static ReceivedHandledMessage? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new ReceivedHandledMessage(debugMsg, group["message"].Value, group["fromActor"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
