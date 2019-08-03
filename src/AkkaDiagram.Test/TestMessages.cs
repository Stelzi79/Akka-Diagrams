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
            yield return new object[]
            {
                new Debug("akka://SomeActorCluster/system",
                          typeof(Akka.Actor.SystemGuardianActor),
                          "now supervising akka://SomeActorCluster/system/UnhandledMessageForwarder"),
                "[NowSupervising][{time}] - akka://SomeActorCluster/system supervises akka://SomeActorCluster/system/UnhandledMessageForwarder",
                new TimeSpan(0, 0, 0, 0, 200),
            };
            yield return new object[]
            {
                new Debug("EventStream", typeof(Akka.Event.EventStream),
                          "subscribing [akka://SomeActorCluster/system/log1-DiagramLoggerActor#1851496843] to channel Akka.Event.Debug"),
                "[SubscribeToChannel][{time}] - akka://SomeActorCluster/system/log1-DiagramLoggerActor#1851496843 => Akka.Event.Debug",
                new TimeSpan(0, 0, 0, 0, 200),
            };
            yield return new object[]
            {
                new Debug("EventStream(SomeActorCluster)", typeof(Akka.Event.EventStream),
                          "Logger log1-DiagramLoggerActor [DiagramLoggerActor] started"),
                "[LoggerStarted][{time}] - log1-DiagramLoggerActor started [DiagramLoggerActor]",
                new TimeSpan(0, 0, 0, 0, 200),
            };
            yield return new object[]
            {
                new Debug("EventStream(SomeActorCluster)", typeof(Akka.Event.EventStream),
                          "StandardOutLogger being removed"),
                "[Removed][{time}] - StandardOutLogger",
                new TimeSpan(0, 0, 0, 0, 200),
            };
            yield return new object[]
            {
                new Debug("EventStream", typeof(Akka.Event.EventStream),
                          "subscribing [akka://SomeActorCluster/system/log1-DiagramLoggerActor#1880692300] to channel Akka.Event.Debug"),
                "[SubscribeToChannel][{time}] - akka://SomeActorCluster/system/log1-DiagramLoggerActor#1880692300 => Akka.Event.Debug",
                new TimeSpan(0, 0, 0, 0, 200),
            };
            yield return new object[]
            {
                new Debug("UnhandledLogSource", typeof(object),
                          "This is an Unhandled Debug Message"),
                "[UNHANDLED] new Debug(\"UnhandledLogSource\", Type.GetType(\"System.Object, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\"), \"This is an Unhandled Debug Message\")\r\n[NOTAG]Debug: '{msg}'",
                new TimeSpan(0, 0, 0, 0, 200),
            };
            yield return new object[]
            {
                new Debug("akka://SomeActorCluster/system/UnhandledMessageForwarder", Type.GetType("Akka.Event.LoggingBus+UnhandledMessageForwarder, Akka, Version=1.3.13.0, Culture=neutral, PublicKeyToken=null", true), "Started (Akka.Event.LoggingBus+UnhandledMessageForwarder)"),
                "[Started][{time}] - [akka://SomeActorCluster/system/UnhandledMessageForwarder]",
                new TimeSpan(0, 0, 0, 0, 200),
            };

            yield return new object[]
            {
                new Debug("EventStream", Type.GetType("Akka.Event.EventStream, Akka, Version=1.3.13.0, Culture=neutral, PublicKeyToken=null"), "unsubscribing [akka://all-systems/] from all channels"),
                "[UnsubscribeFromAll][{time}] - [akka://all-systems/]",
                new TimeSpan(0, 0, 0, 0, 200),
            };

            yield return new object[]
            {
                new Debug("EventStream(SomeActorCluster)", Type.GetType("Akka.Event.EventStream, Akka, Version=1.3.13.0, Culture=neutral, PublicKeyToken=null"), "Default Loggers started"),
                "[DefaultLoggersStarted][{time}]",
                new TimeSpan(0, 0, 0, 0, 200),
            };

            yield return new object[]
            {
                new Debug("akka://SomeActorCluster/system", Type.GetType("Akka.Actor.SystemGuardianActor, Akka, Version=1.3.13.0, Culture=neutral, PublicKeyToken=null"), "now supervising akka://SomeActorCluster/system/EventStreamUnsubscriber-1"),
                "[NowSupervising][{time}] - akka://SomeActorCluster/system supervises akka://SomeActorCluster/system/EventStreamUnsubscriber-1",
                new TimeSpan(0, 0, 0, 0, 200),
            };

            yield return new object[]
            {
                new Debug("EventStreamUnsubscriber", Type.GetType("Akka.Event.EventStreamUnsubscriber, Akka, Version=1.3.13.0, Culture=neutral, PublicKeyToken=null"), "registering unsubscriber with Akka.Event.EventStream"),
                "[RegisteringUnsubscriber][{time}] - with Akka.Event.EventStreamUnsubscriber",
                new TimeSpan(0, 0, 0, 0, 200),
            };

            yield return new object[]
            {
                new Debug("akka://SomeActorCluster/system/log1-DiagramLoggerActor", Type.GetType("AkkaDiagram.DiagramLoggerActor, dotnet-akkadiagram, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"), "received handled message [DEBUG][18.07.2019 16:21:18][Thread 0001][EventStream] subscribing [akka://SomeActorCluster/system/log1-DiagramLoggerActor#1952415063] to channel Akka.Event.Debug from akka://SomeActorCluster/deadLetters"),
                "[ReceivedHandledMessage][{time}] - [akka://SomeActorCluster/system/log1-DiagramLoggerActor] handled message '[DEBUG][18.07.2019 16:21:18][Thread 0001][EventStream] subscribing [akka://SomeActorCluster/system/log1-DiagramLoggerActor#1952415063] to channel Akka.Event.Debug' from [akka://SomeActorCluster/deadLetters]",
                new TimeSpan(0, 0, 0, 0, 200),
            };

            //yield return new object[] {
            //    new Debug("", typeof(Akka.Event.EventStream),
            //              ""),
            //    "",
            //    new TimeSpan(0, 0, 0, 0, 200)};

            //yield return new object[] {
            //    new Debug("", typeof(Akka.Event.EventStream),
            //              ""),
            //    "",
            //    new TimeSpan(0, 0, 0, 0, 200)};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
