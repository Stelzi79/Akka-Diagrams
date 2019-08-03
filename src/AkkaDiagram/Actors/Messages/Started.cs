using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    public class Started : HandleMessageBase<Started>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"Started \((?'actorType'([a-zA-Z0-9.+]*))\)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public string StartedActorPath => Origin.LogSource;

        private Type ActorType => Origin.LogClass;

        public string Tag => nameof(Started);

        public bool Handle() =>
         Handle(this);

        private Started(Debug origin)
            : base(origin)
        {
        }

        public static Started? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new Started(debugMsg),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
