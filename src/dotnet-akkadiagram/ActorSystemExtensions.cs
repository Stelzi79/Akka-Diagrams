using System;
using System.IO;
using Akka.Configuration;
using AkkaDiagram.Actors.Handlers;
using static AkkaDiagram.SettingsLitterals;

namespace AkkaDiagram
{
    public static class ActorSystemExtensions
    {
        private const string DEFAULT_CONFIG = "AkkaDiagram.defaults.hocon";

        public static Config InjectAkkaDiagrams(
            this Config config,
            string? diagramConfig = null,
            string? fileName = DEFAULT_CONFIG)
        {
            if (config is null)
                throw new ArgumentNullException(nameof(config));

            var loggerType = typeof(DiagramLoggerActor);
            var handlerType = typeof(ConsoleOutputHandler);
            var loggers = $"akka.loggers = [\"{loggerType.FullName}, {loggerType.Assembly.GetName().Name}\"]";
            var diagramTypes = $"akka.diagram.{DIAGRAM_TYPES} = [{handlerType.FullName}]";

            if (string.IsNullOrWhiteSpace(diagramConfig) && string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName), $"{nameof(diagramConfig)} and {nameof(fileName)} are purposefully are set to invalid strings! At least one or none (using defaults) parameter must be used.");
            }
            else if (string.IsNullOrWhiteSpace(diagramConfig))
            {
                diagramConfig = File.ReadAllText(fileName);
            }

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
