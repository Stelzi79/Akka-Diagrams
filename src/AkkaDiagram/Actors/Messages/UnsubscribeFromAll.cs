using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    /// <summary>
    ///
    /// </summary>
    public class UnsubscribeFromAll : HandleMessageBase<UnsubscribeFromAll>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"unsubscribing \[(?'actor'.*)\] from all channels$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Gets the ActorPath
        /// </summary>
        public string ActorPath { get; }

        /// <inheritdoc/>
        public string Tag => nameof(UnsubscribeFromAll);

        private UnsubscribeFromAll(Debug origin, string actorPath)
            : base(origin)
        {
            ActorPath = actorPath;
        }

        /// <inheritdoc/>
        public bool Handle() =>
            Handle(this);

        /// <summary>
        ///
        /// </summary>
        /// <param name="debugMsg"></param>
        /// <param name="handlers"></param>
        /// <returns>UnsubscribeFromAll?</returns>
        public static UnsubscribeFromAll? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new UnsubscribeFromAll(debugMsg, group["actor"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
