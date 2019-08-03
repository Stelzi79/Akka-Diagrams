using System;

using Akka.Actor;
using Akka.Event;

using AkkaDiagram.Actors;

namespace AkkaDiagram
{
    /// <summary>
    ///
    /// </summary>
    public class DiagramLoggerActor : UntypedActor, ILogReceive
    {
        private IActorRef? _DebugHandler;

        /// <inheritdoc/>
        protected override void OnReceive(object message)
        {
            var unhandled = "[1] " + message;

            switch (message)
            {
                case InitializeLogger initMsg:
                    _DebugHandler = Context.ActorOf<DebugMessageHandler>();
                    WriteOutputToConsole($"{nameof(DiagramLoggerActor)} initiated!", ConsoleColor.Yellow, ConsoleColor.Black);
                    Sender.Tell(new LoggerInitialized());
                    unhandled = string.Empty;
                    break;
                case Debug debugMsg:

                    _DebugHandler.Forward(debugMsg);
                    unhandled = string.Empty;

                    break;
                case Error errorMsg:
                // break;
                case Warning warningMsg:
                // break;
                case Info infoMsg:
                // break;
                default:
                    WriteOutputToConsole($"{message.GetType().Name} Message: '{message}'", ConsoleColor.White, ConsoleColor.Black);
                    unhandled = string.Empty;
                    break;
            }

            if (!string.IsNullOrEmpty(unhandled))
                Console.WriteLine("[1][Unhandled] " + unhandled);
        }

        //private bool IsMessageForSelf(string strMsg) =>
        //       strMsg.Contains(Self.Path.ToStringWithUid())
        //    || strMsg.Contains($"[{nameof(DiagramLoggerActor)}]");

        /// <summary>
        ///
        /// </summary>
        /// <param name="debugMsg"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="forgroundColor"></param>
        public static void WriteOutputToConsole(string debugMsg,
            ConsoleColor backgroundColor = ConsoleColor.Black,
            ConsoleColor forgroundColor = ConsoleColor.White)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = forgroundColor;
            Console.WriteLine(debugMsg);
            Console.ResetColor();
        }
    }
}
