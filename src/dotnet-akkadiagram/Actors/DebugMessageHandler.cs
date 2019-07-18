using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using Akka.Actor;
using Akka.Event;
using AkkaDiagram.Actors.Messages;
using static AkkaDiagram.DiagramLoggerActor;
using static AkkaDiagram.SettingsLitterals;

namespace AkkaDiagram.Actors
{

    public class DebugMessageHandler : ReceiveActor
    {
        private const string UNHANDLED_TEMPLATE = "[UNHANDLED] new Debug(\"{LogSource}\", Type.GetType(\"{LogClass}\"), \"{Message}\")";

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
                var outString = UNHANDLED_TEMPLATE.Replace("{LogSource}", debugMsg.LogSource);
                outString = outString.Replace("{LogClass}", debugMsg.LogClass.AssemblyQualifiedName);
                outString = outString.Replace("{Message}", debugMsg.Message.ToString());


                WriteOutputToConsole(outString, ConsoleColor.Yellow, ConsoleColor.DarkBlue);
                WriteOutputToConsole($"[NOTAG]{nameof(Debug)}: '{debugMsg}'", ConsoleColor.Yellow, ConsoleColor.DarkBlue);
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
            var messageHandlers = Context.System.Settings.Config.GetStringList($"akka.diagram.{MESSAGE_HANDLERS}");
            var funcs = new List<Func<Debug, IHandleMessage?>>();

            foreach (var handler in messageHandlers)
            {
                var t = Type.GetType(handler, true, true).GetMethods();

                var tryCreateMessage = Type.GetType(handler, true, true).GetMethod("TryCreateMessage");
                funcs.Add(msg => (IHandleMessage?)tryCreateMessage.Invoke(null, new object[] { msg, Context.System.Settings.Config.GetStringList($"akka.diagram.{DIAGRAM_TYPES}") }));
            }
            return funcs;
        }
    }
}
