﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AkkaDiagram
{
    partial class DiagramGenerator
    {
        private static readonly List<DiagramGenerator> Generators = new List<DiagramGenerator>(); internal static List<string> ResolveSlns(string[] args)
        {
            List<string> slns = new List<string>();
            if (args.Count() <= 0)
            {
                slns = DiagramGenerator.FindSlns(Directory.GetCurrentDirectory()).ToList();
            }
            foreach (string sln in args)
            {
                if (sln.EndsWith(".sln") && File.Exists(sln))
                {
                    slns.Add(sln);
                }
                else if (Directory.Exists(sln))
                {
                    slns.AddRange(DiagramGenerator.FindSlns(sln));
                }
            }

            if (slns == null || slns.Count() <= 0)
            {
                throw new ArgumentException("No solution files found!");
            }

            foreach (string sln in slns)
            {
                Generators.Add(new DiagramGenerator(sln));
            }

            return slns;
        }

        internal static void Generate() => throw new NotImplementedException();

        internal static string[] FindSlns(string curdir)
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