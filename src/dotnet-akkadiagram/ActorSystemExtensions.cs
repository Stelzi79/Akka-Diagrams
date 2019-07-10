using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.Configuration;

namespace AkkaDiagram
{
    public static class ActorSystemExtensions
    {
        private const string DEFAULT_CONFIG = @"akka {                
        # Options: OFF, ERROR, WARNING, INFO, DEBUG
        stdout-loglevel = OFF
        loglevel = DEBUG
        log-config-on-start = off
        loggers = [<logger>]
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

            var type = typeof(DiagramLoggerActor);
            //\"Akka.Event.DefaultLogger\",
            var loggers = $"\"{type.FullName}, {type.Assembly.GetName().Name}\"";
            diagramConfig = diagramConfig.Replace("<logger>", loggers);

            return config.WithFallback(ConfigurationFactory.ParseString(diagramConfig));

        }
    }
}
