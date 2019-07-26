namespace AkkaDiagram.Actors.Messages
{
    public interface IHandleMessage
    {
        bool Handle();

        string Tag { get; }
    }
}