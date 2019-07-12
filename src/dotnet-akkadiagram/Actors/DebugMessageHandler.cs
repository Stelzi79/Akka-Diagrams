using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Akka.Actor;
using Akka.Event;
using AkkaDiagram.Actors.Messages;
using static AkkaDiagram.DiagramLoggerActor;

namespace AkkaDiagram.Actors
{

    public class DebugMessageHandler : ReceiveActor
    {

        public DebugMessageHandler()
        {
            Receive<Debug>(debugMsg => Handle(debugMsg));

        }

        private void Handle(Debug debugMsg)
        {
            var handle = GetMessage(debugMsg);

            //Console.WriteLine("[2][DebugHandler] " + debugMsg);
            if (!(handle != null && handle.Handle()))
            {
                WriteOutputToConsole($"[NOTAG]{nameof(Debug)}: '{debugMsg}'", ConsoleColor.Yellow, ConsoleColor.DarkBlue);
            }
            else
            {
                //Console.WriteLine("[!Handled]");
            }
        }

        private IHandleMessage? GetMessage(Debug debugMsg)
        {

            IHandleMessage? msg = null;
            foreach (var action in GetActions())
            {
                msg = action(debugMsg);
                if (msg != null)
                    break;
            }

            return msg;
        }

        private IEnumerable<Func<Debug, IHandleMessage?>> GetActions()
        {
            yield return msg => SubscibeToChannel.TryCreateMessage(msg);
            yield return msg => LoggerStarted.TryCreateMessage(msg);
            yield return msg => Removed.TryCreateMessage(msg);
            yield return msg => NowSupervising.TryCreateMessage(msg);

        }
    }
}
