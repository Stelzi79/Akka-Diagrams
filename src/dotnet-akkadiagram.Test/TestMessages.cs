using System;
using System.Collections;
using System.Collections.Generic;
using Akka.Event;

namespace AkkaDiagram.Test
{
    internal static class TestMessages
    {
        internal static IEnumerable<Debug> GetDebugMessages()
        {
            yield return new Debug("akka://SomeActorCluster/system",
                                   typeof(Akka.Actor.SystemGuardianActor),
                                   "now supervising akka://SomeActorCluster/system/UnhandledMessageForwarder");

        }

    }
}
