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
        private string StartedActorPath => _Origin.LogSource;
        private Type ActorType => _Origin.LogClass;

        public string Tag => nameof(Started);

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{_Origin.Timestamp}] - [{StartedActorPath}]", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }
        private Started(Debug origin) : base(origin)
        {

        }
        public static Started? TryCreateMessage(Debug debugMsg)
            => TryCreateMessage((group)
                => new Started(debugMsg),
                debugMsg.Message.ToString(),
                _Regex);
    }
}
