using Akka.Actor;

namespace SomeActorSystem.Actors
{
    internal class SomeUserActor : UntypedActor
    {
        public SomeUserActor()
        {
            // Create more Actors
            Context.ActorOf<SubActor>(nameof(SubActor) + "1");
            Context.ActorOf<SubActor>(nameof(SubActor) + "2");
            Context.ActorOf<SubActor>(nameof(SubActor) + "3");
            Context.ActorOf<NestedSubActor>(nameof(NestedSubActor));
        }
        protected override void OnReceive(object message) => System.Console.WriteLine($"Got some message: '{message}'!");
    }
}
