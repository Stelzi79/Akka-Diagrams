# Akka-Diagrams

A Nuget Library to a dotnet global tool to generate diagrams from a [Akka.Net](https://getakka.net/index.html) Actor system.

If you have the need to visualize certain aspects of your running Akka system, this Nuget package is the solution to your problem.

Inspired by the issue https://github.com/csharpfritz/Quiltoni.PixelBot/issues/24

## Status
[![Board Status](https://dev.azure.com/Stelzi79/86ee283c-8bae-4522-88a3-a2ae9df3b9e3/a66164b5-f82c-425a-bb13-805393780a58/_apis/work/boardbadge/eb1b314e-7541-42f7-b5f5-ea0b912c906b)](https://dev.azure.com/Stelzi79/86ee283c-8bae-4522-88a3-a2ae9df3b9e3/_boards/board/t/a66164b5-f82c-425a-bb13-805393780a58/Microsoft.RequirementCategory)
[![CodeFactor](https://www.codefactor.io/repository/github/stelzi79/akka-diagrams/badge/develop)](https://www.codefactor.io/repository/github/stelzi79/akka-diagrams/overview/develop)

## Abilities

* lets you inject a Akka ILogReceive that detects debug messages to generate information about the actor system.
* for debugging it can write the found messages in console.
* can be configured to output a JsonFile and dotFile.
* the output file format can be extended by custom handlers.
* configuration with in Akka builtin HOCON provider.
* can gather information to use for xUnit unit tests.
