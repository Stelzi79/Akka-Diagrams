using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    public abstract class HandleMessageBase<T>
        where T : IHandleMessage
    {
        //private static Config _Config;
        private static IList<OutputHandlerInfo> _OutputHandlers;

        //private static void InitOutputHandlers()
        //{
        //    _OutputHandlers = new List<OutputHandlerInfo>();

        //    foreach (var handler in _Config.GetStringList(OUTPUT_HANDLERS))
        //    {
        //        _OutputHandlers.Add(new OutputHandlerInfo(handler));
        //    }
        //}
        public Debug Origin { get; private set; }

        protected HandleMessageBase(Debug origin) => Origin = origin;

        protected bool Handle(T handledMessage)
        {
            var test = _OutputHandlers.Where(msg => msg.ShouldHandle(handledMessage.Tag));

            foreach (var handler in test)
            {
                handler.Handle(handledMessage);
            }

            return true;
        }

        private protected static T TryCreateMessage(Func<GroupCollection, T> initialzierFunc, string msg, Regex regex, IList<OutputHandlerInfo> outputHandlers)
        {
            _OutputHandlers = outputHandlers;

            // InitOutputHandlers();
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
