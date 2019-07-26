using System;
using System.IO;
using System.Threading;

using Akka.Actor;
using Akka.Event;
using Akka.TestKit.Xunit2;

using AkkaDiagram.Actors;

using Xunit;
using Xunit.Abstractions;

namespace AkkaDiagram.Test
{
    public class DebugMessagesTests : TestKit
    {
        private readonly ITestOutputHelper _Output;


        private const string CONF = @"akka{

diagram{
output-handlers = [Console]
message-handlers = [
            DefaultLoggersStarted,
            LoggerStarted,
            NowSupervising,
            ReceivedHandledMessage,
            RegisteringUnsubscriber,
            Removed,
            Started,
            SubscribeToChannel,
            UnsubscribeFromAll]
}
}
";

        public DebugMessagesTests(ITestOutputHelper output) : base(CONF)
        {
            using var standardOut = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOut);
            _Output = output;
        }

        [Theory]
        [ClassData(typeof(TestMessages))]
        public void ShouldDedectAndWriteDebugMessages(Debug debugMsg,
                                                      string expected,
                                                      TimeSpan timeout,
                                                      bool ignore = false)
        {
            if (ignore)
            {
                _Output.WriteLine($"Ignored: {debugMsg}");
                return;
            }
            //arrange
            using StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            var props = Props.Create<DebugMessageHandler>();

            var subject = Sys.ActorOf(props);
            var probe = CreateTestProbe();

            //act
            subject.Tell(debugMsg);
            Thread.Sleep(timeout);
            expected = expected.Replace("{time}", debugMsg.Timestamp.ToString());
            expected = expected.Replace("{msg}", debugMsg.ToString());

            //assert
            //Assert.Equal(expected, sw.ToString(), ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);

            probe.AwaitCondition(() =>
            {
                var condition = (expected == sw.ToString().Trim());
                if (!condition)
                {
                    _Output.WriteLine($"Expected: '{expected}'");
                    _Output.WriteLine($"Actual: '{sw.ToString().Trim()}'");
                }
                return condition;
            }, timeout);
        }
    }
}
