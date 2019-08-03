using Akka.Actor;

namespace SomeActorSystem.Actors
{
    internal class NestedSubActor : SomeUserActor
    {
        public NestedSubActor()
        {
            _ = Context.ActorOf<SubActor>(nameof(SubActor) + "Nested1");
            _ = Context.ActorOf<SubActor>(nameof(SubActor) + "Nested2");
            _ = Context.ActorOf<SubActor>(nameof(SubActor) + "Nested3");
        }
    }
}
