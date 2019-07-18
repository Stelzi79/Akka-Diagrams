using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Akka.Event;
using static AkkaDiagram.DiagramLoggerActor;

namespace AkkaDiagram.Actors.Messages
{
    internal class SubscibeToChannel : HandleMessageBase<SubscibeToChannel>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^subscribing \[(?'IActorRefInstance'.*)\] to channel (?'cannel'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly string _IActorRef;
        private readonly string _Cannel;

        private SubscibeToChannel(Debug origin, string actorRef, string cannel) : base(origin)
        {
            _IActorRef = actorRef;
            _Cannel = cannel;
        }

        public string Tag => nameof(SubscibeToChannel);

        public static SubscibeToChannel? TryCreateMessage(Debug debugMsg, IList<string> config)
            => TryCreateMessage((group)
                => new SubscibeToChannel(debugMsg, group["IActorRefInstance"].Value, group["cannel"].Value),
                debugMsg.Message.ToString(),
                _Regex,
                config);

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{Origin.Timestamp}] - {_IActorRef} => {_Cannel}", ConsoleColor.Green, ConsoleColor.Black);
            return handled;
        }
    }

}
