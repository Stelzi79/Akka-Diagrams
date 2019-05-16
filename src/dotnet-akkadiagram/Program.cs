using System;
using System.CommandLine.DragonFruit;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AkkaDiagram
{
    class Program
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="args" type="arg">File Path to *.sln</param>
        static void Main(string[] args)
        {
            List<string> slns = DiagramGenerator.ResolveSlns(args);
            Console.WriteLine($"Generate AkkaDiagramms for these Solutions:\n * {string.Join("\n * ", slns)}");
            DiagramGenerator.Generate();
            _ = Console.ReadKey();
        }
    }
}
