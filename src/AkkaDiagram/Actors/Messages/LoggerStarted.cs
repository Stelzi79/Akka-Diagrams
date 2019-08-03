using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    /// <summary>
    ///
    /// </summary>
    public class LoggerStarted : HandleMessageBase<LoggerStarted>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"Logger (?'logActor'log[0-9]*-(?'actorType'([a-zA-Z0-9.]*))) \[\k<actorType>\] started", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Gets the LogActor
        /// </summary>
        public string LogActor { get; }

        /// <summary>
        /// Gets the ActorType
        /// </summary>
        public string ActorType { get; }

        private LoggerStarted(Debug origin, string logActor, string actorType)
            : base(origin)
        {
            LogActor = logActor;
            ActorType = actorType;
        }

        /// <inheritdoc/>
        public string Tag => nameof(LoggerStarted);

        /// <inheritdoc/>
        public bool Handle() =>
            Handle(this);

        /// <summary>
        ///
        /// </summary>
        /// <param name="debugMsg"></param>
        /// <param name="handlers"></param>
        /// <returns>LoggerStarted?</returns>
        public static LoggerStarted? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new LoggerStarted(debugMsg, group["logActor"].Value, group["actorType"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
