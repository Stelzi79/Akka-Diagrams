using System;
using System.IO;

using Akka.Actor;
using Akka.Configuration;

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
        /// <param name="args">Currently not used</param>
        private static void Main(string[] args)
        {
            Console.WriteLine("Create Akka System ...");

            string seedNodeConfig = File.ReadAllText("akka-hocon.conf");

            Config config = ConfigurationFactory.ParseString(seedNodeConfig);

#if DEBUG
            // This injects the needed debug-logging configuration and adds the diagram actor
            // Be aware of stuff not working if you change debug and logging in config before you inject AkkaDiagrams!
            config = config.InjectAkkaDiagrams();
#endif

            using var system = ActorSystem.Create("SomeActorCluster", config);
            var someActor = system.ActorOf<SomeUserActor>(nameof(SomeUserActor));
            someActor.Tell("Actor system started and EchoActor added!");

            Console.ReadLine();
        }
    }
}
