using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    /// <summary>
    ///
    /// </summary>
    public class SubscribeToChannel : HandleMessageBase<SubscribeToChannel>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^subscribing \[(?'IActorRefInstance'.*)\] to channel (?'cannel'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Gets the IActorRef
        /// </summary>
        public string IActorRef { get; }

        /// <summary>
        /// Gets the Channel
        /// </summary>
        public string Cannel { get; }

        private SubscribeToChannel(Debug origin, string actorRef, string cannel)
            : base(origin)
        {
            IActorRef = actorRef;
            Cannel = cannel;
        }

        /// <inheritdoc/>
        public string Tag => nameof(SubscribeToChannel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="debugMsg"></param>
        /// <param name="handlers"></param>
        /// <returns>SubscribeToChannel?</returns>
        public static SubscribeToChannel? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new SubscribeToChannel(debugMsg, group["IActorRefInstance"].Value, group["cannel"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);

        /// <inheritdoc/>
        public bool Handle() =>
            Handle(this);
    }
}
