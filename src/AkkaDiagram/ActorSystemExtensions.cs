using System;
using System.IO;

using Akka.Configuration;

using static AkkaDiagram.SettingsLiterals;

namespace AkkaDiagram
{
    /// <summary>
    /// Extension Methods to inject the AkkaDiagram logger and set it up with default configurations
    /// </summary>
    public static class ActorSystemExtensions
    {
        /// <summary>
        ///
        /// </summary>
        public const string DEFAULT_CONFIG = "AkkaDiagram.defaults.hocon";

        /// <summary>
        /// This injects the needed debug-logging configuration and adds the diagram actor
        ///    Be aware of stuff not working if you change debug and logging in config before you inject AkkaDiagrams!
        /// </summary>
        /// <param name="config">Akka.Configuration.Config</param>
        /// <param name="diagramConfig">HOCON string of configurations</param>
        /// <param name="fileName">Filename of HOCON configurations</param>
        /// <returns><paramref name="config"/></returns>
        public static Config InjectAkkaDiagrams(
            this Config config,
            string? diagramConfig = null,
            string? fileName = DEFAULT_CONFIG)
        {
            if (config is null)
                throw new ArgumentNullException(nameof(config));

            var loggerType = typeof(DiagramLoggerActor);
            var handlerType = typeof(Actors.Handlers.Console);
            var loggers = $"akka.loggers = [\"{loggerType.FullName}, {loggerType.Assembly.GetName().Name}\"]";
            var diagramTypes = $"akka.diagram.{OUTPUT_HANDLERS} = [{handlerType.FullName}]";

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
            if (config.GetValue($"akka.diagram.{OUTPUT_HANDLERS}") == null)
            {
                config = config.WithFallback(ConfigurationFactory.ParseString(diagramTypes));
            }

            return config;
        }
    }
}
