using System;
using System.Collections.Generic;

using AkkaDiagram.Actors.Messages;

namespace AkkaDiagram
{
    /// <summary>
    /// Provides some Defaults
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// Returns all the built in types AkkaDiagram uses
        /// </summary>
        /// <returns>BuiltIn Types</returns>
        public static IDictionary<string, Type> BuiltInTypes()
        {
            var ret = new Dictionary<string, Type>
            {
                { nameof(DefaultLoggersStarted), typeof(DefaultLoggersStarted) },
                { nameof(LoggerStarted), typeof(LoggerStarted) },
                { nameof(NowSupervising), typeof(NowSupervising) },
                { nameof(ReceivedHandledMessage), typeof(ReceivedHandledMessage) },
                { nameof(RegisteringUnsubscriber), typeof(RegisteringUnsubscriber) },
                { nameof(Removed), typeof(Removed) },
                { nameof(Started), typeof(Started) },
                { nameof(SubscribeToChannel), typeof(SubscribeToChannel) },
                { nameof(UnsubscribeFromAll), typeof(UnsubscribeFromAll) },
                { nameof(Actors.Handlers.Console), typeof(Actors.Handlers.Console) },

                //{ nameof(Actors.Handlers.Json), typeof(Actors.Handlers.Json) },
                //{ nameof(Actors.Handlers.DotFile), typeof(Actors.Handlers.DotFile) },
            };

            //ret.Add(nameof(), typeof());
            return ret;
        }
    }
}
