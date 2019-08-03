using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    /// <summary>
    ///
    /// </summary>
    public class ReceivedHandledMessage : HandleMessageBase<ReceivedHandledMessage>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^received handled message (?'message'.*) from (?'fromActor'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Gets the Message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the FromActor
        /// </summary>
        public string FromActor { get; }

        /// <summary>
        /// Gets the ReceivedActor
        /// </summary>
        public string ReceivedActor => Origin.LogSource;

        /// <inheritdoc/>
        public string Tag => nameof(ReceivedHandledMessage);

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedHandledMessage"/> class.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="message"></param>
        /// <param name="fromActor"></param>
        public ReceivedHandledMessage(Debug origin, string message, string fromActor)
            : base(origin)
        {
            Message = message;
            FromActor = fromActor;
        }

        /// <inheritdoc/>
        public bool Handle() =>
            Handle(this);

        /// <summary>
        ///
        /// </summary>
        /// <param name="debugMsg"></param>
        /// <param name="handlers"></param>
        /// <returns>ReceivedHandledMessage</returns>
        public static ReceivedHandledMessage? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new ReceivedHandledMessage(debugMsg, group["message"].Value, group["fromActor"].Value),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
