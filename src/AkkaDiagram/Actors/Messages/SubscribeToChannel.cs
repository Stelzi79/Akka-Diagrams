using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    public class SubscribeToChannel : HandleMessageBase<SubscribeToChannel>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^subscribing \[(?'IActorRefInstance'.*)\] to channel (?'cannel'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public string IActorRef { get; }

        public string Cannel { get; }

        private SubscribeToChannel(Debug origin, string actorRef, string cannel)
            : base(origin)
        {
            IActorRef = actorRef;
            Cannel = cannel;
        }

        public string Tag => nameof(SubscribeToChannel);

        public static SubscribeToChannel? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new SubscribeToChannel(debugMsg, group["IActorRefInstance"].Value, group["cannel"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);

        public bool Handle() =>
            Handle(this);
    }
}
