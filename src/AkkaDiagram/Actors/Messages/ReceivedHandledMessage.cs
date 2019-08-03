using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    public class ReceivedHandledMessage : HandleMessageBase<ReceivedHandledMessage>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^received handled message (?'message'.*) from (?'fromActor'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public string Message { get; }

        public string FromActor { get; }

        public string ReceivedActor => Origin.LogSource;

        public string Tag => nameof(ReceivedHandledMessage);

        public ReceivedHandledMessage(Debug origin, string message, string fromActor)
            : base(origin)
        {
            Message = message;
            FromActor = fromActor;
        }

        public bool Handle() =>
            Handle(this);

        public static ReceivedHandledMessage? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new ReceivedHandledMessage(debugMsg, group["message"].Value, group["fromActor"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
