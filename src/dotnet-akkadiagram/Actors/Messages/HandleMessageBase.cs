using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

namespace AkkaDiagram.Actors.Messages
{
    public abstract class HandleMessageBase<T>
        where T : IHandleMessage
    {
        private static IList<string> _Config;
        private static List<OutputHandlerInfo> _OutputHandlers;

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
            foreach (var handler in _OutputHandlers)
            {
                handler.Handle(handledMessage);
            }

            return true;
        }

        private protected static T TryCreateMessage(Func<GroupCollection, T> initialzierFunc, string msg, Regex regex, IList<string> config)
        {
            _Config = config;
            InitOutputHandlers();
            var match = regex.Match(msg);
            return match.Success ? initialzierFunc(match.Groups) : default;
        }

        protected static void WriteOutputToConsole(
            string debugMsg,
            ConsoleColor backgroundColor = ConsoleColor.Black,
            ConsoleColor forgroundColor = ConsoleColor.White) =>
                DiagramLoggerActor.WriteOutputToConsole(debugMsg, backgroundColor, forgroundColor);
    }
}
