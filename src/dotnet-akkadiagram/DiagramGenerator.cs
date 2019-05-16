using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace AkkaDiagram
{
    internal partial class DiagramGenerator
    {
        private readonly string _SolutionPath;


        private DiagramGenerator(string solutionPath) => _SolutionPath = solutionPath;

        public async Task GenerateDiagramAsync()
        {
            _ = MSBuildLocator.RegisterDefaults();
            using MSBuildWorkspace msWorkspace = MSBuildWorkspace.Create();
            Solution solution = await msWorkspace.OpenSolutionAsync(_SolutionPath);


            Console.WriteLine($" ## Projects in Solution '{solution.FilePath}':");
            foreach (Project project in solution.Projects)
            {
                Console.Write($"   - {project.Name} ");

                if (project.MetadataReferences.Where(r => r.Display.Contains(@"\.nuget\packages\akka\")).Count() >= 1)
                {
                    Console.WriteLine("[included - Akka present]");
                }
                else
                {
                    Console.WriteLine("[excluded - no Akka present]");
                }

                //C:\Users\proficoncept\.nuget\packages\akka\1.3.13\lib\netstandard1.6\Akka.dll


                //if (project.AllProjectReferences)
                //{

                //}
            }

        }
    }
}