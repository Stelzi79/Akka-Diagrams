using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaDiagram
{
    partial class DiagramGenerator
    {
        private readonly string _SolutionPath;

        private DiagramGenerator(string solutionPath) => _SolutionPath = solutionPath;
        public async Task GenerateAsync()
        {
            await Task.Run(() => Task.Yield());
        }
    }
}