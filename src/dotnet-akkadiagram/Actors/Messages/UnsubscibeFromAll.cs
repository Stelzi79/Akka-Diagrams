using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Akka.Event;

namespace AkkaDiagram.Actors.Messages
{
    internal class UnsubscibeFromAll : HandleMessageBase<UnsubscibeFromAll>, IHandleMessage
    {

        private static readonly Regex _Regex = new Regex(@"unsubscribing \[(?'actor'.*)\] from all channels$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly string _ActorPath;

        public string Tag => nameof(UnsubscibeFromAll);

        private UnsubscibeFromAll(Debug origin, string actorPath) : base(origin)
        {
            _ActorPath = actorPath;
        }

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{_Origin.Timestamp}] - [{_ActorPath}]", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }
        public static UnsubscibeFromAll? TryCreateMessage(Debug debugMsg)
            => TryCreateMessage((group)
                => new UnsubscibeFromAll(debugMsg, group["actor"].Value),
                debugMsg.Message.ToString(),
                _Regex);

    }
}
