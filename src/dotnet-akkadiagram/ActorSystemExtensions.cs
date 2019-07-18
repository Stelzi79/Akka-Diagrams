using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.Configuration;
using AkkaDiagram;
using AkkaDiagram.Actors.Handlers;
using static AkkaDiagram.SettingsLitterals;

namespace AkkaDiagram
{
    public static class ActorSystemExtensions
    {

        private const string DEFAULT_CONFIG = @"akka {                
        # Options: OFF, ERROR, WARNING, INFO, DEBUG
        stdout-loglevel = OFF
        loglevel = DEBUG
        log-config-on-start = off
        # loggers = [<logger>]
        diagram {
            message-handlers = [AkkaDiagram.Actors.Messages.DefaultLoggersStarted, 
                                AkkaDiagram.Actors.Messages.LoggerStarted,
                                AkkaDiagram.Actors.Messages.NowSupervising,
                                AkkaDiagram.Actors.Messages.RecievedHandledMessage, 
                                AkkaDiagram.Actors.Messages.RegisteringUnsubscriber, 
                                AkkaDiagram.Actors.Messages.Removed, 
                                AkkaDiagram.Actors.Messages.Started, 
                                AkkaDiagram.Actors.Messages.SubscibeToChannel, 
                                AkkaDiagram.Actors.Messages.UnsubscibeFromAll]
            # Types that handle the output in the format '""{Type.FullName}, Assembly.GetName().Name""'. Defaults to 'ConsoleOutputHandler'
            # types = [""AkkaDiagram.Actors.Handlers.ConsoleOutputHandler, dotnet-akkadiagram"",
            #          ""AkkaDiagram.Actors.Handlers.JsonOutputHandler, dotnet-akkadiagram"",
            #          ""AkkaDiagram.Actors.Handlers.DotFileOutputHandler, dotnet-akkadiagram""]
            # Here are now all the configuration section of the individual handlers
            # console-output-handler {
            # }
            # json-output-handler {
            # }
            # dot-file-output-handler {
            # }
        }
        actor {
          debug {
            receive = on # log any received message
            autoreceive = on # log automatically received messages, e.g. PoisonPill
            lifecycle = on # log actor lifecycle changes
            event-stream = on # log subscription changes for Akka.NET event stream
            unhandled = on # log unhandled messages sent to actors
          }
    }
}";
        public static Config InjectAkkaDiagrams(this Config config,
                                                string diagramConfig = DEFAULT_CONFIG)
        {
            if (config is null)
                throw new ArgumentNullException(nameof(config));

            var loggerType = typeof(DiagramLoggerActor);
            var handlerType = typeof(ConsoleOutputHandler);
            var loggers = $"akka.loggers = [\"{loggerType.FullName}, {loggerType.Assembly.GetName().Name}\"]";
            var diagramTypes = $"akka.diagram.{DIAGRAM_TYPES} = [\"{handlerType.FullName}, {handlerType.Assembly.GetName().Name}\"]";
            config = config.WithFallback(ConfigurationFactory.ParseString(diagramConfig));
            config = config.WithFallback(ConfigurationFactory.ParseString(loggers));
            if (config.GetValue($"akka.diagram.{DIAGRAM_TYPES}") == null)
            {
                config = config.WithFallback(ConfigurationFactory.ParseString(diagramTypes));
            }

            return config;
        }
    }
}
