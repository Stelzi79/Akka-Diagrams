using AkkaDiagram.Actors.Messages;

namespace AkkaDiagram.Actors.Handlers
{
    /// <summary>
    ///
    /// </summary>
    public interface IOutputHandler
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        void Handle(UnsubscribeFromAll msg);

        void Handle(SubscribeToChannel msg);

        void Handle(Started msg);

        void Handle(Removed msg);

        void Handle(RegisteringUnsubscriber msg);

        void Handle(ReceivedHandledMessage msg);

        void Handle(NowSupervising msg);

        void Handle(LoggerStarted msg);

        void Handle(DefaultLoggersStarted msg);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
