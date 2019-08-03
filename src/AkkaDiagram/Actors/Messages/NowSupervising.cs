using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    /// <summary>
    ///
    /// </summary>
    public class NowSupervising : HandleMessageBase<NowSupervising>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^now supervising (?'actorSupervised'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Gets the Actor which is supervised by ActorSupervisor
        /// </summary>
        public string ActorSupervised { get; }

        /// <summary>
        /// Gets the Actor which supervises the ActorSupervised
        /// </summary>
        public string ActorSupervisor { get; }

        private NowSupervising(Debug origin, string actorSupervised)
            : base(origin)
        {
            ActorSupervised = actorSupervised;
            ActorSupervisor = Origin.LogSource;
        }

        /// <inheritdoc/>
        public string Tag => nameof(NowSupervising);

        /// <inheritdoc/>
        public bool Handle() =>
            Handle(this);

        /// <summary>
        ///
        /// </summary>
        /// <param name="debugMsg"></param>
        /// <param name="handlers"></param>
        /// <returns>NowSupervising?</returns>
        public static NowSupervising? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new NowSupervising(debugMsg, group["actorSupervised"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
