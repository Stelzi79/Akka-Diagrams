# Akka-Diagrams

A Nuget library to inject an Akka ILogReceive that is able to generates diagrams from a [Akka.Net](https://getakka.net/index.html) Actor system.

If you have the need to visualize certain aspects of your running Akka system, this Nuget package is the solution to your problem.

Inspired by the issue https://github.com/csharpfritz/Quiltoni.PixelBot/issues/24

## Status

[![CodeFactor](https://www.codefactor.io/repository/github/stelzi79/akka-diagrams/badge/develop)](https://www.codefactor.io/repository/github/stelzi79/akka-diagrams/overview/develop)

## Features

* lets you inject an Akka ILogReceive that detects debug messages to generate information about the actor system.
* it can write the found messages in console for debugging purposes.
* can be configured to output a JsonFile and dotFile.
* the output file formats can be extended by custom handlers.
* configuration with in Akka builtin HOCON provider.
* gathers information to use for xUnit unit tests.

## Quick Start

```csharp
            string seedNodeConfig = File.ReadAllText("akka-hocon.conf");

            Config config = ConfigurationFactory.ParseString(seedNodeConfig);

#if DEBUG
            // This injects the needed debug-logging configuration and adds the diagram actor
            // Be aware of stuff not working if you change debug and logging in config before you inject AkkaDiagrams!
            config = config.InjectAkkaDiagrams();
#endif

            using var system = ActorSystem.Create("SomeActorCluster", config);
```

This adds the ILogReceive logger to the configuration of the Akka system you are creating. In this short example it only add the needed configuration when the project is build with the `DEBUG` symbol. This logger is not supposed to log things when it is in production!