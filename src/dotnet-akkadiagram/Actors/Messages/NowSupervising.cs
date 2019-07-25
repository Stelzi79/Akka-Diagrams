using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

namespace AkkaDiagram.Actors.Messages
{
    internal class NowSupervising : HandleMessageBase<NowSupervising>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^now supervising (?'actorSupervised'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly string _ActorSupervised;
        private readonly string _ActorSuperviser;

        private NowSupervising(Debug origin, string actorSupervised)
            : base(origin)
        {
            _ActorSupervised = actorSupervised;
            _ActorSuperviser = Origin.LogSource;
        }

        public string Tag => nameof(NowSupervising);

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{Origin.Timestamp}] - {_ActorSuperviser} supervises {_ActorSupervised}", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }

        public static NowSupervising? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new NowSupervising(debugMsg, group["actorSupervised"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
