using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Linq;
using System.Text;
using Akka.Actor;
using Akka.Event;
using AkkaDiagram.Actors.Messages;
using static AkkaDiagram.DiagramLoggerActor;
using static AkkaDiagram.SettingsLitterals;
using Akka.Configuration;

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

        private readonly Dictionary<string, Type> _DefinedTypes = new Dictionary<string, Type>();
        private readonly Config _Config = Context.System.Settings.Config;
        private IList<string> _MessageHandlers = new List<string>();

        protected override void PreStart()
        {

            _MessageHandlers = _Config.GetStringList($"akka.diagram.{MESSAGE_HANDLERS}").ToList();
            var definedTypes = _Config.GetStringList($"akka.diagram.{DEFAULT_TYPES}").ToList<string>();
            definedTypes.AddRange(_Config.GetStringList($"akka.diagram.{CUSTOM_TYPES}"));

            foreach (var typeString in definedTypes)
            {
                var type = Type.GetType(typeString, true)!;
                _DefinedTypes.Add(type.Name, type);
            }


        }

        private IEnumerable<Func<Debug, IHandleMessage?>> GetActions()
        {
            var funcs = new List<Func<Debug, IHandleMessage?>>();

            foreach (var handler in _MessageHandlers)
            {
                var handlerType = Type.GetType(handler, true, true);
                if (handlerType != null)
                {
                    //var t = handlerType.GetMethods();
                    var tryCreateMessage = handlerType.GetMethod("TryCreateMessage");
                    funcs.Add(msg => (IHandleMessage?)tryCreateMessage?.Invoke(null, new object[] { msg, Context.System.Settings.Config.GetStringList($"akka.diagram.{DIAGRAM_TYPES}") }));
                }
            }
            return funcs;
        }
    }
}
