using System;
using Akka.Actor;
using SomeActorSystem.Actors;

namespace SomeActorSystem
{
    internal class Program
    {
        /// <summary>
        /// Creates a Actor system like:
        /// akka://SomeActorCluster
        ///     akka://SomeActorCluster/user/SomeUserActor
        ///         akka://SomeActorCluster/user/SomeUserActor/SubActor1
        ///         akka://SomeActorCluster/user/SomeUserActor/SubActor2
        ///         akka://SomeActorCluster/user/SomeUserActor/SubActor3
        ///         akka://SomeActorCluster/user/SomeUserActor/NestedSubActor
        ///             akka://SomeActorCluster/user/SomeUserActor/NestedSubActor/SubActorNested1
        ///             akka://SomeActorCluster/user/SomeUserActor/NestedSubActor/SubActorNested2
        ///             akka://SomeActorCluster/user/SomeUserActor/NestedSubActor/SubActorNested3
        /// </summary>
        /// <param name="args"></param>
        private static void Main()
        {
            Console.WriteLine("Create Akka System ...");
            //string seedNodeConfig = File.ReadAllText("akkanode.conf");
            //Config config = ConfigurationFactory.ParseString(seedNodeConfig);

            ActorSystem system = ActorSystem.Create("SomeActorCluster");
          
            _ = system.ActorOf<SomeUserActor>(nameof(SomeUserActor));
            //localecho.Tell("Actor system started and EchoActor added!");
            _ = Console.ReadLine();
        }
    }
}
