using Akka.Actor;

namespace SomeActorSystem.Actors
{
    internal class SomeUserActor : UntypedActor
    {
        public SomeUserActor()
        {
            // Create more Actors
            _ = Context.ActorOf<SubActor>(nameof(SubActor) + "1");
            _ = Context.ActorOf<SubActor>(nameof(SubActor) + "2");
            _ = Context.ActorOf<SubActor>(nameof(SubActor) + "3");
            _ = Context.ActorOf<NestedSubActor>(nameof(NestedSubActor));
        }
        protected override void OnReceive(object message) => System.Console.WriteLine($"Got some message: '{message}'!");
    }
}
