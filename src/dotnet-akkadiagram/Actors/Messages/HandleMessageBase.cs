using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;
using Akka.Event;
using static AkkaDiagram.DiagramLoggerActor;

namespace AkkaDiagram.Actors.Messages
{
    internal abstract class HandleMessageBase<T>
    {
        private protected Debug _Origin;

        private protected static T TryCreateMessage(Func<GroupCollection, T> initialzierFunc, string msg, Regex regex)
        {
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
