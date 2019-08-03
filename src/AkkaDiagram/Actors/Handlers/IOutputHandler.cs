using AkkaDiagram.Actors.Messages;

namespace AkkaDiagram.Actors.Handlers
{
    public interface IOutputHandler
    {
        void Handle(UnsubscribeFromAll msg);

        void Handle(NowSupervising msg);

        void Handle(SubscribeToChannel msg);
    }
}
