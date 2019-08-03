using System;

using AkkaDiagram.Actors.Messages;

using static AkkaDiagram.DiagramLoggerActor;

namespace AkkaDiagram.Actors.Handlers
{
    public class Console : IOutputHandler
    {
        public void Handle(UnsubscribeFromAll msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - [{msg.ActorPath}]", ConsoleColor.Yellow, ConsoleColor.Black);

        public void Handle(NowSupervising msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - {msg.ActorSuperviser} supervises {msg.ActorSupervised}", ConsoleColor.Yellow, ConsoleColor.Black);

        public void Handle(SubscribeToChannel msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - {msg.IActorRef} => {msg.Cannel}", ConsoleColor.Green, ConsoleColor.Black);
    }
}
