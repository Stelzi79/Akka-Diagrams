# Akka-Diagrams

A Nuget Library to a dotnet global tool to generate diagrams from a [Akka.Net](https://getakka.net/index.html) Actor system.

Inspired by the issue https://github.com/csharpfritz/Quiltoni.PixelBot/issues/24

## Status

[![CodeFactor](https://www.codefactor.io/repository/github/stelzi79/akka-diagrams/badge/develop)](https://www.codefactor.io/repository/github/stelzi79/akka-diagrams/overview/develop)

## Abilities

* Global dotnet tool which lets you generate a diagram in CLI with ````dotnet akka-diagram Assembly.dll````
* BuildProps of a Nuget package that lets you integrate this as a build step after a successful build.
* Searches for Actors in an .Net assembly and put it in an Json file
* Uses the Json file to generate a diagram.
