using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    /// <summary>
    ///
    /// </summary>
    public class Started : HandleMessageBase<Started>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"Started \((?'actorType'([a-zA-Z0-9.+]*))\)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Gets the StartedActorPath
        /// </summary>
        public string StartedActorPath => Origin.LogSource;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private Type ActorType => Origin.LogClass;

        /// <inheritdoc/>
        public string Tag => nameof(Started);

        /// <inheritdoc/>
        public bool Handle() =>
         Handle(this);

        private Started(Debug origin)
            : base(origin)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="debugMsg"></param>
        /// <param name="handlers"></param>
        /// <returns>Started?</returns>
        public static Started? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new Started(debugMsg),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
