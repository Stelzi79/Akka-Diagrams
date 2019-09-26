# Akka-Diagrams

A Nuget Library to a dotnet global tool to generate diagrams from a [Akka.Net](https://getakka.net/index.html) Actor system.

If you have the need to visualize certain aspects of your running Akka system, this Nuget package is the solution to your problem.

Inspired by the issue https://github.com/csharpfritz/Quiltoni.PixelBot/issues/24

## Status

[![CodeFactor](https://www.codefactor.io/repository/github/stelzi79/akka-diagrams/badge/develop)](https://www.codefactor.io/repository/github/stelzi79/akka-diagrams/overview/develop)

## Features

* lets you inject a Akka ILogReceive that detects debug messages to generate information about the actor system.
* it can write for debugging the found messages in console.
* can be configured to output a JsonFile and dotFile.
* the output file formats can be extended by custom handlers.
* configuration with in Akka builtin HOCON provider.
* gathers information to use for xUnit unit tests.
