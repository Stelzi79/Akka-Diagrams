using System;
using System.Text.RegularExpressions;
using Akka.Event;
using static AkkaDiagram.DiagramLoggerActor;

namespace AkkaDiagram.Actors.Messages
{
    internal class SubscibeToChannel : IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"subscribing \[(?'IActorRefInstance'.*)\] to channel (?'cannel'([a-zA-Z0-9.]*))$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly Debug _Origin;
        private readonly string _IActorRef;
        private readonly string _Cannel;

        public SubscibeToChannel(Debug debugMsg, string actorRef, string cannel)
        {
            _Origin = debugMsg;
            _IActorRef = actorRef;
            _Cannel = cannel;
        }

        public static SubscibeToChannel? TryCreateMessage(Debug debugMsg)
        {
            var match = _Regex.Match(debugMsg.Message.ToString());
            if (match.Success)
            {
                var groups = match.Groups;
                return new SubscibeToChannel(debugMsg, groups["IActorRefInstance"].Value, groups["cannel"].Value);
            }
            return null;

        }

        public bool Handle()
        {
            var hadled = true;
            //Console.WriteLine(_Origin.Message);
            WriteOutputToConsole($"[subscribe][{_Origin.Timestamp}] - {_IActorRef} => {_Cannel}", ConsoleColor.Green, ConsoleColor.Black);
            return hadled;
        }
    }

}
