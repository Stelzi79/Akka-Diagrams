# Akka-Diagrams

A Nuget Library to a dotnet global tool to generate diagrams from a [Akka.Net](https://getakka.net/index.html) Actor system.

Inspired by the issue https://github.com/csharpfritz/Quiltoni.PixelBot/issues/24

## Status
[![Board Status](https://dev.azure.com/Stelzi79/86ee283c-8bae-4522-88a3-a2ae9df3b9e3/a66164b5-f82c-425a-bb13-805393780a58/_apis/work/boardbadge/eb1b314e-7541-42f7-b5f5-ea0b912c906b)](https://dev.azure.com/Stelzi79/86ee283c-8bae-4522-88a3-a2ae9df3b9e3/_boards/board/t/a66164b5-f82c-425a-bb13-805393780a58/Microsoft.RequirementCategory)
[![CodeFactor](https://www.codefactor.io/repository/github/stelzi79/akka-diagrams/badge/develop)](https://www.codefactor.io/repository/github/stelzi79/akka-diagrams/overview/develop)

## Abilities

* Global dotnet tool which lets you generate a diagram in CLI with ````dotnet akka-diagram Assembly.dll````
* BuildProps of a Nuget package that lets you integrate this as a build step after a successful build.
* Searches for Actors in an .Net assembly and put it in an Json file
* Uses the Json file to generate a diagram.
