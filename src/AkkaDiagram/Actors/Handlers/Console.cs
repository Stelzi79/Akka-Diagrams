using System;

using AkkaDiagram.Actors.Messages;

using static AkkaDiagram.DiagramLoggerActor;

namespace AkkaDiagram.Actors.Handlers
{
    /// <summary>
    ///
    /// </summary>
    public class Console : IOutputHandler
    {
        /// <inheritdoc/>
        public void Handle(UnsubscribeFromAll msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - [{msg.ActorPath}]", ConsoleColor.Yellow, ConsoleColor.Black);

        /// <inheritdoc/>
        public void Handle(NowSupervising msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - {msg.ActorSupervisor} supervises {msg.ActorSupervised}", ConsoleColor.Yellow, ConsoleColor.Black);

        /// <inheritdoc/>
        public void Handle(SubscribeToChannel msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - {msg.IActorRef} => {msg.Cannel}", ConsoleColor.Green, ConsoleColor.Black);

        /// <inheritdoc/>
        public void Handle(Started msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - [{msg.StartedActorPath}]", ConsoleColor.Green, ConsoleColor.Black);

        /// <inheritdoc/>
        public void Handle(Removed msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - {msg.ActorType}", ConsoleColor.Green, ConsoleColor.Black);

        /// <inheritdoc/>
        public void Handle(RegisteringUnsubscriber msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - with {msg.UnsubscriberType.FullName}", ConsoleColor.Green, ConsoleColor.Black);

        /// <inheritdoc/>
        public void Handle(ReceivedHandledMessage msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - [{msg.ReceivedActor}] handled message '{msg.Message}' from [{msg.FromActor}]", ConsoleColor.Green, ConsoleColor.Black);

        /// <inheritdoc/>
        public void Handle(LoggerStarted msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}] - {msg.LogActor} started [{msg.ActorType}]", ConsoleColor.Green, ConsoleColor.Black);

        /// <inheritdoc/>
        public void Handle(DefaultLoggersStarted msg)
            => WriteOutputToConsole($"[{msg.Tag}][{msg.Origin.Timestamp}]", ConsoleColor.Green, ConsoleColor.Black);
    }
}
