using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Akka.Event;

namespace AkkaDiagram.Actors.Messages
{
    internal class RecievedHandledMessage : HandleMessageBase<RecievedHandledMessage>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^received handled message (?'message'.*) from (?'fromActor'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly string _Message;
        private readonly string _FromActor;
        private string ReceivedActor => _Origin.LogSource;

        public string Tag => nameof(RecievedHandledMessage);

        public RecievedHandledMessage(Debug origin, string message, string fromActor) : base(origin)
        {
            _Message = message;
            _FromActor = fromActor;
        }

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{_Origin.Timestamp}] - [{ReceivedActor}] handled message '{_Message}' from [{_FromActor}]", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }

        public static RecievedHandledMessage? TryCreateMessage(Debug debugMsg)
            => TryCreateMessage((group)
                => new RecievedHandledMessage(debugMsg, group["message"].Value, group["fromActor"].Value),
                debugMsg.Message.ToString(),
                _Regex);
    }
}
