using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    /// <summary>
    ///
    /// </summary>
    public class RegisteringUnsubscriber : HandleMessageBase<RegisteringUnsubscriber>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^registering unsubscriber with (?'EventStreamType'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Gets the UnsubscriberType
        /// </summary>
        public Type UnsubscriberType => Origin.LogClass;

        private RegisteringUnsubscriber(Debug origin)
            : base(origin)
        {
        }

        /// <inheritdoc/>
        public string Tag => nameof(RegisteringUnsubscriber);

        /// <inheritdoc/>
        public bool Handle() =>
            Handle(this);

        /// <summary>
        ///
        /// </summary>
        /// <param name="debugMsg"></param>
        /// <param name="handlers"></param>
        /// <returns>RegisteringUnsubscriber?</returns>
        public static RegisteringUnsubscriber? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new RegisteringUnsubscriber(debugMsg),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
