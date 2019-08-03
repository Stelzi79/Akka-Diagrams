using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Akka.Event;

using AkkaDiagram.Actors.Handlers;

namespace AkkaDiagram.Actors.Messages
{
    public class RegisteringUnsubscriber : HandleMessageBase<RegisteringUnsubscriber>, IHandleMessage
    {
        private static readonly Regex _Regex = new Regex(@"^registering unsubscriber with (?'EventStreamType'.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public Type UnsubscriberType => Origin.LogClass;

        private RegisteringUnsubscriber(Debug origin)
            : base(origin)
        {
        }

        public string Tag => nameof(RegisteringUnsubscriber);

        public bool Handle() =>
            Handle(this);

        public static RegisteringUnsubscriber? TryCreateMessage(Debug debugMsg, IList<OutputHandlerInfo> handlers)
            => TryCreateMessage(
                (group)
                => new RegisteringUnsubscriber(debugMsg),
                debugMsg.Message.ToString() ?? string.Empty,
                _Regex,
                handlers);
    }
}
