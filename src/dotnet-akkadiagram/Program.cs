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
        /// <param name="args" type="arg">asdfasfFile Path to *.sln</param>
        static void Main(string[] args)
        {
            List<string> slns = new List<string>();
            if (args.Count() <= 0)
            {
                slns = FindSlns(Directory.GetCurrentDirectory()).ToList();
            }
            foreach (string sln in args)
            {
                if (sln.EndsWith(".sln") && File.Exists(sln))
                {
                    slns.Add(sln);
                }
                else if (Directory.Exists(sln))
                {
                    slns.AddRange(FindSlns(sln));
                }
            }

            if (slns == null || slns.Count() <= 0)
            {
                throw new ArgumentException("No solution files found!");
            }

            Console.WriteLine($"Generate AkkaDiagramms for these Solutions:\n * {string.Join("\n * ", slns)}");
            Console.ReadKey();

        }
        private static string[] FindSlns(string curdir)
        {
            if (Directory.GetDirectoryRoot(curdir) == curdir)
            {
                return null;
            }
            string[] slns = Directory.GetFiles(curdir).Where(file => file.EndsWith(".sln")).ToArray();
            if (slns.Count() <= 0)
            {
                return FindSlns(Directory.GetParent(curdir).FullName);
            }
            return slns;
        }
    }
}
