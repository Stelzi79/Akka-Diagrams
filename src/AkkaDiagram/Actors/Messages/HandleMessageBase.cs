using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T">IHandleMessage</typeparam>
    public abstract class HandleMessageBase<T>
        where T : IHandleMessage
    {
        private static IList<OutputHandlerInfo> _OutputHandlers = new List<OutputHandlerInfo>();

        /// <summary>
        /// Gets the Debug Message
        /// </summary>
        public Debug Origin { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleMessageBase{T}"/> class.
        /// </summary>
        /// <param name="origin"></param>
        protected HandleMessageBase(Debug origin)
        {
            Origin = origin;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="handledMessage"></param>
        /// <returns>Returns if it could be handled</returns>
        protected bool Handle(T handledMessage)
        {
            var outputhandlers = _OutputHandlers.Where(msg => msg.ShouldHandle(handledMessage.Tag));

            foreach (var handler in outputhandlers)
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="debugMsg"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="forgroundColor"></param>
        protected static void WriteOutputToConsole(
            string debugMsg,
            ConsoleColor backgroundColor = ConsoleColor.Black,
            ConsoleColor forgroundColor = ConsoleColor.White) =>
                DiagramLoggerActor.WriteOutputToConsole(debugMsg, backgroundColor, forgroundColor);
    }
}
