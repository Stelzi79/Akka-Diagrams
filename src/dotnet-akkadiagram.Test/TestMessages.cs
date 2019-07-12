using System;
using System.Collections;
using System.Collections.Generic;
using Akka.Event;

namespace AkkaDiagram.Test
{

    public class TestMessages : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new Debug("akka://SomeActorCluster/system", typeof(Akka.Actor.SystemGuardianActor),
                          "now supervising akka://SomeActorCluster/system/UnhandledMessageForwarder"),
                "[NowSupervising][12.07.2019 13:25:31] - akka://SomeActorCluster/system supervises akka://SomeActorCluster/system/UnhandledMessageForwarder",
                new TimeSpan(0, 0, 0, 0, 10)};

        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
