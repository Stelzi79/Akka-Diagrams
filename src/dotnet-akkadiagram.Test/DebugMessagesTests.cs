using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Akka.Actor;
using Akka.Event;
using Akka.TestKit.Xunit2;
using AkkaDiagram.Actors;
using Xunit;
using Xunit.Sdk;

namespace AkkaDiagram.Test
{
    public class DebugMessagesTests : TestKit
    {
        public DebugMessagesTests()
        {
            using var standardOut = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOut);
        }

        [Theory]
        [ClassData(typeof(TestMessages))]
        public void ShouldDedectAndWriteDebugMessages(Debug debugMsg, String expected, TimeSpan timeout)
        {
            //arrange
            using StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            var props = Props.Create<DebugMessageHandler>();

            var subject = Sys.ActorOf(props);
            var probe = CreateTestProbe();

            //act
            subject.Tell(debugMsg);

            //assert
            probe.AwaitConditionNoThrow(() => expected == sw.ToString(), timeout);
        }
    }
}
