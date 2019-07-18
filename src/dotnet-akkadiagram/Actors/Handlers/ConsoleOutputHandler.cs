using System;
using System.Collections.Generic;
using System.Text;
using AkkaDiagram.Actors.Messages;

namespace AkkaDiagram.Actors.Handlers
{
    public class ConsoleOutputHandler : IOutputHandler
    {
        public void Handle(UnsubscibeFromAll msg)
            => DiagramLoggerActor.WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - [{msg.ActorPath}]", ConsoleColor.Green, ConsoleColor.Black);
    }
}
