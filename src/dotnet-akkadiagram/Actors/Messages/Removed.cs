using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Akka.Event;


namespace AkkaDiagram.Actors.Messages
{
    internal class Removed : HandleMessageBase<Removed>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"(?'actorType'([a-zA-Z0-9.]*)) being removed", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly string _ActorType;

        private Removed(Debug origin, string actorType)
        {
            _Origin = origin;
            _ActorType = actorType;
        }

        public string Tag => nameof(Removed);

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{_Origin.Timestamp}] - {_ActorType}", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }


        public static Removed? TryCreateMessage(Debug debugMsg)
            => TryCreateMessage((group)
                => new Removed(debugMsg, group["actorType"].Value),
                debugMsg.Message.ToString(),
                _Regex);
    }
}
