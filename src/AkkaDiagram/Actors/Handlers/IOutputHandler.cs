using AkkaDiagram.Actors.Messages;

namespace AkkaDiagram.Actors.Handlers
{
    public interface IOutputHandler
    {
        void Handle(UnsubscribeFromAll msg);

        void Handle(SubscribeToChannel msg);

        void Handle(Started msg);

        void Handle(Removed msg);

        void Handle(RegisteringUnsubscriber msg);

        void Handle(ReceivedHandledMessage msg);

        void Handle(NowSupervising msg);

        void Handle(LoggerStarted msg);

        void Handle(DefaultLoggersStarted msg);
    }
}
