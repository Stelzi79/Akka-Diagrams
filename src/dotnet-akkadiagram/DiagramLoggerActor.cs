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
                    //Console.WriteLine("[2][Debug] " + unhandled);
                    // Only process Debug messages that is not about us
                    //if (debugMsg.Message is String strMsg/* && !IsMessageForSelf(strMsg)*/)
                    //{
                    _DebugHandler.Forward(debugMsg);
                    unhandled = string.Empty;
                    //}
                    //else
                    //    Console.WriteLine("[2] " + debugMsg);
                    break;
                case Error errorMsg:
                //break;
                case Warning warningMsg:
                //break;
                case Info infoMsg:
                //break;
                default:
                    WriteOutputToConsole($"{message.GetType().Name} Message: '{message}'", ConsoleColor.White, ConsoleColor.Black);
                    unhandled = string.Empty;
                    break;

            }
            if (!string.IsNullOrEmpty(unhandled))
                Console.WriteLine("[1][Unhandled] " + unhandled);
        }

        private bool IsMessageForSelf(string strMsg) =>
               strMsg.Contains(Self.Path.ToStringWithUid())
            || strMsg.Contains($"[{nameof(DiagramLoggerActor)}]");

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
