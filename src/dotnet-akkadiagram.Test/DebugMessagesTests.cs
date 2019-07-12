using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace AkkaDiagram.Test
{
    public class DebugMessagesTests
    {
        public DebugMessagesTests()
        {
            using var standardOut = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOut);
        }



        [Fact]
        public void test()
        {
            Assert.True(true);
        }
    }
}
