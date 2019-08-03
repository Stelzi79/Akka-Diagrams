using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    public class Removed : HandleMessageBase<Removed>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"(?'actorType'([a-zA-Z0-9.]*)) being removed", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public string ActorType { get; }

        private Removed(Debug origin, string actorType)
            : base(origin) => ActorType = actorType;

        public string Tag => nameof(Removed);

        public bool Handle() =>
           Handle(this);

        public static Removed? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new Removed(debugMsg, group["actorType"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
