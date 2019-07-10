using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Akka.Actor;
using Akka.Event;
using static AkkaDiagram.DiagramLoggerActor;

namespace AkkaDiagram.Actors
{
    class DebugMessageHandler : ReceiveActor
    {
        public DebugMessageHandler()
        {
            Receive<Debug>(debugMsg => Handle(debugMsg));
        }

        private void Handle(Debug debugMsg)
        {

            WriteOutputToConsole($"{nameof(Debug)}: '{debugMsg}'", ConsoleColor.Green, ConsoleColor.DarkBlue);

        }
    }
}
