using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    internal class SubscribeToChannel : HandleMessageBase<SubscribeToChannel>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^subscribing \[(?'IActorRefInstance'.*)\] to channel (?'cannel'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly string _IActorRef;
        private readonly string _Cannel;

        private SubscribeToChannel(Debug origin, string actorRef, string cannel)
            : base(origin)
        {
            _IActorRef = actorRef;
            _Cannel = cannel;
        }

        public string Tag => nameof(SubscribeToChannel);

        public static SubscribeToChannel? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new SubscribeToChannel(debugMsg, group["IActorRefInstance"].Value, group["cannel"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{Origin.Timestamp}] - {_IActorRef} => {_Cannel}", ConsoleColor.Green, ConsoleColor.Black);
            return handled;
        }
    }
}
