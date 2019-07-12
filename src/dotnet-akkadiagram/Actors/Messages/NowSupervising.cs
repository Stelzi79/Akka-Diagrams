using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Akka.Event;
using static AkkaDiagram.DiagramLoggerActor;

namespace AkkaDiagram.Actors.Messages
{
    internal class NowSupervising : HandleMessageBase<NowSupervising>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"now supervising (?'actorSupervised'([a-zA-Z0-9/-:]*))$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly string _ActorSupervised;
        private readonly string _ActorSuperviser;

        private NowSupervising(Debug origin, string actorSupervised)
        {
            //if (string.IsNullOrWhiteSpace(actorSupervised))
            //throw new ArgumentException("is not allowed to be empty! Possible problem with Regex?", nameof(actorSupervised));
            _Origin = origin;
            _ActorSupervised = actorSupervised;
            _ActorSuperviser = _Origin.LogSource;
        }

        public string Tag => nameof(NowSupervising);

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{_Origin.Timestamp}] - {_ActorSuperviser} supervises {_ActorSupervised}", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }


        public static NowSupervising? TryCreateMessage(Debug debugMsg)
            => TryCreateMessage((group)
                => new NowSupervising(debugMsg, group["actorSupervised"].Value),
                debugMsg.Message.ToString(),
                _Regex);
    }
}
