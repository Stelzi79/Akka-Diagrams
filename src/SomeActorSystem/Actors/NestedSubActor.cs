using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace SomeActorSystem.Actors
{
    class NestedSubActor : SomeUserActor
    {
        public NestedSubActor()
        {
            Context.ActorOf<SubActor>(nameof(SubActor) + "Nested1");
            Context.ActorOf<SubActor>(nameof(SubActor) + "Nested2");
            Context.ActorOf<SubActor>(nameof(SubActor) + "Nested3");
        }
    }
}
