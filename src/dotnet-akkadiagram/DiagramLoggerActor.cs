using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.Event;
using Akka.Pattern;
using AkkaDiagram.Actors;

namespace AkkaDiagram
{
    public class DiagramLoggerActor : UntypedActor, ILogReceive
    {
        private IActorRef _DebugHandler;

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case InitializeLogger initMsg:
                    _DebugHandler = Context.ActorOf<DebugMessageHandler>();
                    WriteOutputToConsole($"{nameof(DiagramLoggerActor)} initiated!", ConsoleColor.Yellow, ConsoleColor.Black);
                    Sender.Tell(new LoggerInitialized());
                    break;
                case Debug debugMsg:
                    // Only process Debug messages that is not about us
                    if (debugMsg.Message is String strMsg && !strMsg.Contains(Self.Path.ToStringWithUid()))
                    {
                        _DebugHandler.Forward(debugMsg);
                    }
                    break;
                case Error errorMsg:
                    break;
                case Warning warningMsg:
                    break;
                case Info infoMsg:
                    break;
                default:
                    WriteOutputToConsole($"{message.GetType().Name} Message: '{message}'", ConsoleColor.White, ConsoleColor.Black);
                    break;
            }


        }

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
