using System;

using AkkaDiagram.Actors.Messages;

namespace AkkaDiagram.Actors.Handlers
{
    public class Console : IOutputHandler
    {
        public void Handle(UnsubscribeFromAll msg)
            => DiagramLoggerActor.WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - [{msg.ActorPath}]", ConsoleColor.Green, ConsoleColor.Black);
    }
}
