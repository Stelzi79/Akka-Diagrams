﻿using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Akka.Event;
using static AkkaDiagram.DiagramLoggerActor;

namespace AkkaDiagram.Actors.Messages
{
    internal class LoggerStarted : HandleMessageBase<LoggerStarted>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"Logger (?'logActor'log[0-9]*-(?'actorType'([a-zA-Z0-9.]*))) \[\k<actorType>\] started", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private readonly string _LogActor;
        private readonly string _ActorType;

        private LoggerStarted(Debug origin, string logActor, string actorType) : base(origin)
        {
            _LogActor = logActor;
            _ActorType = actorType;
        }

        public string Tag => nameof(LoggerStarted);

        public bool Handle()
        {
            var handled = true;

            WriteOutputToConsole($"[{Tag}][{_Origin.Timestamp}] - {_LogActor} started [{_ActorType}]", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }


        public static LoggerStarted? TryCreateMessage(Debug debugMsg)
            => TryCreateMessage((group)
                => new LoggerStarted(debugMsg, group["logActor"].Value, group["actorType"].Value),
                debugMsg.Message.ToString(),
                _Regex);
    }
}
