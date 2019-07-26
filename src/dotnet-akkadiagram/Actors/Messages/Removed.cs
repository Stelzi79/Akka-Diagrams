﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    internal class Removed : HandleMessageBase<Removed>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"(?'actorType'([a-zA-Z0-9.]*)) being removed", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly string _ActorType;

        private Removed(Debug origin, string actorType)
            : base(origin)
        {
            _ActorType = actorType;
        }

        public string Tag => nameof(Removed);

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{Origin.Timestamp}] - {_ActorType}", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }

        public static Removed? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new Removed(debugMsg, group["actorType"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}