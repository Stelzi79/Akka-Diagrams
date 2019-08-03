using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    public class NowSupervising : HandleMessageBase<NowSupervising>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^now supervising (?'actorSupervised'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public string ActorSupervised { get; }

        public string ActorSuperviser { get; }

        private NowSupervising(Debug origin, string actorSupervised)
            : base(origin)
        {
            ActorSupervised = actorSupervised;
            ActorSuperviser = Origin.LogSource;
        }

        public string Tag => nameof(NowSupervising);

        public bool Handle() =>
            Handle(this);

        public static NowSupervising? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new NowSupervising(debugMsg, group["actorSupervised"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
