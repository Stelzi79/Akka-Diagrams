using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

namespace AkkaDiagram.Actors.Messages
{
    public class UnsubscribeFromAll : HandleMessageBase<UnsubscribeFromAll>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"unsubscribing \[(?'actor'.*)\] from all channels$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public string ActorPath { get; }

        public string Tag => nameof(UnsubscribeFromAll);

        private UnsubscribeFromAll(Debug origin, string actorPath)
            : base(origin)
        {
            ActorPath = actorPath;
        }

        public bool Handle() =>

            // var handled = true;

            // WriteOutputToConsole($"[{Tag}][{Origin.Timestamp}] - [{_ActorPath}]", ConsoleColor.Green, ConsoleColor.Black);

            // return handled;
            Handle(this);

        public static UnsubscribeFromAll? TryCreateMessage(Debug debugMsg, IList<string> config)
            => TryCreateMessage(
                (group)
                => new UnsubscribeFromAll(debugMsg, group["actor"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                config);
    }
}
