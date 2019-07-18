using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using Akka.Event;
using AkkaDiagram.Actors.Handlers;
using static AkkaDiagram.DiagramLoggerActor;

namespace AkkaDiagram.Actors.Messages
{
    public abstract class HandleMessageBase<T>
        where T : IHandleMessage
    {
        private static IList<string> _Config;
        private static List<OutputHandlerInfo> _OutputHandlers;

        private static IList<string> Config
        {
            get
            {
                if (_Config == null || _Config.Count <= 0)
                {
                    throw new ApplicationException("No Output Handlers present!");
                }
                return _Config;
            }
            set
            {
                if (_Config == null)
                {
                    _Config = value;
                    InitOutputHandlers();
                }
            }
        }


        private static void InitOutputHandlers()
        {
            _OutputHandlers = new List<OutputHandlerInfo>();

            foreach (var handler in _Config)
            {
                _OutputHandlers.Add(new OutputHandlerInfo(handler));
            }

        }

        public Debug Origin { get; private set; }

        protected HandleMessageBase(Debug origin)
        {
            Origin = origin;
        }

        protected bool Handle(T handledMessage)
        {
            var ret = false;
            foreach (var handler in _OutputHandlers)
            {
                handler.Handle(handledMessage);
            }
            return true;
        }

        private protected static T TryCreateMessage(Func<GroupCollection, T> initialzierFunc, string msg, Regex regex, IList<string> config)
        {
            Config = config;
            var match = regex.Match(msg);
            return match.Success ? initialzierFunc(match.Groups) : (default);
        }
        protected static void WriteOutputToConsole(
            string debugMsg,
            ConsoleColor backgroundColor = ConsoleColor.Black,
            ConsoleColor forgroundColor = ConsoleColor.White) =>
                DiagramLoggerActor.WriteOutputToConsole(debugMsg, backgroundColor, forgroundColor);
    }
}
