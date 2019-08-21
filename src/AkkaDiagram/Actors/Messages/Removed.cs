using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    /// <summary>
    ///
    /// </summary>
    public class Removed : HandleMessageBase<Removed>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"(?'actorType'([a-zA-Z0-9.]*)) being removed", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Gets the ActorType
        /// </summary>
        public string ActorType { get; }

        private Removed(Debug origin, string actorType)
            : base(origin)
        {
            ActorType = actorType;
        }

        /// <inheritdoc/>
        public string Tag => nameof(Removed);

        /// <inheritdoc/>
        public bool Handle() =>
           Handle(this);

        /// <summary>
        ///
        /// </summary>
        /// <param name="debugMsg"></param>
        /// <param name="handlers"></param>
        /// <returns>Removed?</returns>
        public static Removed? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new Removed(debugMsg, group["actorType"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
