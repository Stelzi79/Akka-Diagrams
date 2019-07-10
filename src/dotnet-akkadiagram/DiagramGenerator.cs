using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace AkkaDiagram
{
    internal partial class DiagramGenerator
    {
        private readonly string _SolutionPath;
        private readonly List<Project> _AkkaProject = new List<Project>();

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
                    //_AkkaProject.Add(project);

                    var compilation = await project.GetCompilationAsync();


                    //var emit = compilation.Emit();
                    //compilation.SyntaxTrees.First().GetRoot().SyntaxTree.GetRoot().DescendantNodesAndSelf()
                    var temp = compilation.SyntaxTrees;


                    foreach (SyntaxTree tree in temp)
                    {

                        //var children = tree.GetRoot().DescendantTokens().Where(t => t.ValueText == "ActorOf"));
                        //KindText	"ArgumentList"	string

                        //Key	InvocationExpression	Microsoft.CodeAnalysis.CSharp.SyntaxKind



                        var children = tree.GetRoot()
                            .DescendantNodes()
                            .Where(n => n.DescendantTokens()
                                .Where(t => t.ValueText == "ActorOf")
                                .Count() >= 1 && n.IsKind(SyntaxKind.InvocationExpression))
                            .Select(s => ((InvocationExpressionSyntax)s).ArgumentList.Arguments);

                        foreach (SeparatedSyntaxList<ArgumentSyntax> args in children)
                        {
                            //var visitor = new CSharpSyntaxVisitor();
                            //var value = args.First().Accept(visitor);
                            //var sematic = compilation.GetSemanticModel(tree).GetTypeInfo(args.First());

                        }

                    }


                }
                else
                {
                    Console.WriteLine("[excluded - no Akka present]");
                }
            }

        }


        private object Selector(SyntaxNode arg1, int arg2) => new { kind = arg1.Kind(), text = arg1.GetText(), node = arg1 };
    }
}