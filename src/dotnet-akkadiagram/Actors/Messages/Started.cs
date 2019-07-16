using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Akka.Event;

namespace AkkaDiagram.Actors.Messages
{
    internal class Started : HandleMessageBase<Started>, IHandleMessage
    {

        private static readonly Regex _Regex = new Regex(@"Started \((?'actorType'([a-zA-Z0-9.+]*))\)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly string _StartedActorPath;
        private readonly Type _ActorType;

        public string Tag => nameof(Started);

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{_Origin.Timestamp}] - {_ActorType}", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }
        public Started(Debug origin, string startedActorPath, Type actorType)
        {
            _Origin = origin;
            _StartedActorPath = startedActorPath;
            _ActorType = actorType;
        }
        public static Started? TryCreateMessage(Debug debugMsg)
            => TryCreateMessage((group)
                => new Started(debugMsg, debugMsg.LogSource, debugMsg.LogClass),
                debugMsg.Message.ToString(),
                _Regex);
    }
}
