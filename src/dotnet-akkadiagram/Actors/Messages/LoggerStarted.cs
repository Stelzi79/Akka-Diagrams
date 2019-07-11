using System;
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
        private static readonly Regex _Regex = new Regex(@"", RegexOptions.ExplicitCapture | RegexOptions.Compiled);


        private LoggerStarted(Debug origin)
        {
            _Origin = origin;
        }

        public string Tag => nameof(LoggerStarted);

        public bool Handle()
        {
            var handled = true;

            //WriteOutputToConsole($"[{Tag}][{_Origin.Timestamp}] - {_IActorRef} => {_Cannel}", ConsoleColor.Green, ConsoleColor.Black);

            return handled;
        }


        public static LoggerStarted? TryCreateMessage(Debug debugMsg)
            => TryCreateMessage((group)
                => new LoggerStarted(debugMsg),
                debugMsg.Message.ToString(),
                _Regex);
    }
}
