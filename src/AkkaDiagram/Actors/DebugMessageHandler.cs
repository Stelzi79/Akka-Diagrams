using System;
using System.Collections.Generic;
using System.Linq;

using Akka.Actor;
using Akka.Configuration;
using Akka.Event;

using AkkaDiagram.Actors.Handlers;
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

            // Console.WriteLine("[2][DebugHandler] " + debugMsg);
            if (!(handle != null && handle.Handle()))
            {
                var outString = UNHANDLED_TEMPLATE.Replace("{LogSource}", debugMsg.LogSource);
                outString = outString.Replace("{LogClass}", debugMsg.LogClass.AssemblyQualifiedName);
                outString = outString.Replace("{Message}", debugMsg.Message.ToString());

                WriteOutputToConsole(outString, ConsoleColor.Yellow, ConsoleColor.DarkBlue);
                WriteOutputToConsole($"[NOTAG]{nameof(Debug)}: '{debugMsg}'", ConsoleColor.Yellow, ConsoleColor.DarkBlue);
            }
        }

        private IEnumerable<Func<Debug, IHandleMessage?>>? _Funcs;

        private IHandleMessage? GetMessage(Debug debugMsg)
        {
            IHandleMessage? msg = null;
            if (_Funcs == null)
            {
                _Funcs = GetActions();
            }

            foreach (var action in _Funcs)
            {
                msg = action(debugMsg);
                if (msg != null)
                    break;
            }

            return msg;
        }

        private readonly IDictionary<string, Type> _DefinedTypes = Defaults.BuiltInTypes();

        private readonly Config _Config = Context.System.Settings.Config;
        private readonly IList<OutputHandlerInfo> _OutputHandlers = new List<OutputHandlerInfo>();
        private List<string> _MessageHandlers = new List<string>();

        protected override void PreStart()
        {
            _MessageHandlers = _Config.GetStringList($"akka.diagram.{MESSAGE_HANDLERS}").ToList();
            _MessageHandlers.AddRange(_Config.GetStringList($"akka.diagram.{CUSTOM_MESSAGE_HANDLERS}"));
            var definedTypes = _Config.GetStringList($"akka.diagram.{CUSTOM_TYPES}");
            foreach (var typeString in definedTypes)
            {
                var type = Type.GetType(typeString, true)!;
                if (_DefinedTypes.ContainsKey(type.Name))
                    _DefinedTypes[type.Name] = type;
                else
                    _DefinedTypes.Add(type.Name, type);
            }

            foreach (var outputHandler in _Config.GetStringList($"akka.diagram.{OUTPUT_HANDLERS}"))
            {
                _OutputHandlers.Add(new OutputHandlerInfo(_DefinedTypes[outputHandler], _Config.GetConfig($"akka.diagram.{OUTPUT_HANDLER_CONFIGURATIONS}.{outputHandler.ToLower()}")));
            }
        }

        private IEnumerable<Func<Debug, IHandleMessage?>> GetActions()
        {
            var funcs = new List<Func<Debug, IHandleMessage?>>();

            foreach (var handler in _MessageHandlers)
            {
                var handlerType = _DefinedTypes[handler];
                if (handlerType != null)
                {
                    // var t = handlerType.GetMethods();
                    var tryCreateMessage = handlerType.GetMethod("TryCreateMessage");
                    funcs.Add(msg => (IHandleMessage?)tryCreateMessage?.Invoke(null, new object[] { msg, _OutputHandlers }));
                }
            }

            return funcs;
        }
    }
}
