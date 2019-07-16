using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
                "[NowSupervising][{time}] - akka://SomeActorCluster/system supervises akka://SomeActorCluster/system/UnhandledMessageForwarder",
                new TimeSpan(0, 0, 0,0, 20)};
            yield return new object[] {
                new Debug("EventStream", typeof(Akka.Event.EventStream),
                          "subscribing [akka://SomeActorCluster/system/log1-DiagramLoggerActor#1851496843] to channel Akka.Event.Debug"),
                "[SubscibeToChannel][{time}] - akka://SomeActorCluster/system/log1-DiagramLoggerActor#1851496843 => Akka.Event.Debug",
                new TimeSpan(0, 0, 0, 0, 100)};
            yield return new object[] {
                new Debug("EventStream(SomeActorCluster)", typeof(Akka.Event.EventStream),
                          "Logger log1-DiagramLoggerActor [DiagramLoggerActor] started"),
                "[LoggerStarted][{time}] - log1-DiagramLoggerActor started [DiagramLoggerActor]",
                new TimeSpan(0, 0, 0, 0, 100)};
            yield return new object[] {
                new Debug("EventStream(SomeActorCluster)", typeof(Akka.Event.EventStream),
                          "StandardOutLogger being removed"),
                "[Removed][{time}] - StandardOutLogger",
                new TimeSpan(0, 0, 0, 0, 100)};
            yield return new object[] {
                new Debug("EventStream", typeof(Akka.Event.EventStream),
                          "subscribing [akka://SomeActorCluster/system/log1-DiagramLoggerActor#1880692300] to channel Akka.Event.Debug"),
                "[SubscibeToChannel][{time}] - akka://SomeActorCluster/system/log1-DiagramLoggerActor#1880692300 => Akka.Event.Debug",
                new TimeSpan(0, 0, 0, 0, 100) };
            yield return new object[] {
                new Debug("UnhandledLogSource", typeof(Object),
                          "This is an Unhandled Debug Message"),
                "[UNHANDLED] new Debug(\"UnhandledLogSource\", Type.GetType(\"System.Object, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\"), \"This is an Unhandled Debug Message\")\r\n[NOTAG]Debug: '[DEBUG][{time}][Thread 0004][UnhandledLogSource] This is an Unhandled Debug Message'",
                new TimeSpan(0, 0, 0, 0, 100)};
            yield return new object[] {
                new Debug("akka://SomeActorCluster/system/UnhandledMessageForwarder", Type.GetType("Akka.Event.LoggingBus+UnhandledMessageForwarder, Akka, Version=1.3.13.0, Culture=neutral, PublicKeyToken=null", true), "Started (Akka.Event.LoggingBus+UnhandledMessageForwarder)"),
                "[Started][{time}] - Akka.Event.LoggingBus+UnhandledMessageForwarder",
                new TimeSpan(0, 0, 0, 0, 10)};

            yield return new object[] {
                new Debug("EventStream", Type.GetType("Akka.Event.EventStream, Akka, Version=1.3.13.0, Culture=neutral, PublicKeyToken=null"), "unsubscribing [akka://all-systems/] from all channels"),
                "",
                new TimeSpan(0, 0, 0, 0, 10), true};

            yield return new object[] {
                new Debug("EventStream(SomeActorCluster)", Type.GetType("Akka.Event.EventStream, Akka, Version=1.3.13.0, Culture=neutral, PublicKeyToken=null"), "Default Loggers started"),
                "",
                new TimeSpan(0, 0, 0, 0, 10), true};

            //yield return new object[] {
            //    new Debug("", typeof(Akka.Event.EventStream),
            //              ""),
            //    "",
            //    new TimeSpan(0, 0, 0, 0, 10)};

            //yield return new object[] {
            //    new Debug("", typeof(Akka.Event.EventStream),
            //              ""),
            //    "",
            //    new TimeSpan(0, 0, 0, 0, 10)};

        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
