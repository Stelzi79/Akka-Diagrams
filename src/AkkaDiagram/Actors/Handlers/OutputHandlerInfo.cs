using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Akka.Configuration;

using AkkaDiagram.Actors.Messages;

using static AkkaDiagram.SettingsLiterals;

namespace AkkaDiagram.Actors.Handlers
{
    /// <summary>
    ///
    /// </summary>
    public class OutputHandlerInfo
    {
        /// <summary>
        ///
        /// </summary>
        public const string NO_INSTANCE = "Not able to instantiate with default constructor";

        /// <summary>
        ///
        /// </summary>
        public const string NO_CONSTRUCTOR = "No default constructor present";
        private readonly Dictionary<Type, MethodInfo> _Handlers = new Dictionary<Type, MethodInfo>();
        private readonly object _Instance;
        private readonly Config _Config;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputHandlerInfo"/> class.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        public OutputHandlerInfo(Type type, Config config)
        {
            _Config = config ?? new Config();

            try
            {
                Name = type.Name;
                var constructor = type!.GetConstructor(Type.EmptyTypes)
                    ?? throw new Exception(NO_CONSTRUCTOR);
                _Instance = constructor.Invoke(new object[] { })
                    ?? throw new Exception(NO_INSTANCE);
                var handleMethods = type.GetMethods().Where(m => m.Name == "Handle" && m.ReturnType == typeof(void));
                foreach (var method in handleMethods)
                {
                    _Handlers.Add(method.GetParameters()[0].ParameterType, method);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException($"{type.FullName} is no valid OutputHandler", nameof(type), e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="handledMessage"></param>
        /// <returns>Boolean if it was handled</returns>
        public bool ShouldHandle(string handledMessage) => _Config.IsEmpty || _Config.GetStringList(MESSAGE_HANDLERS).Contains(handledMessage);

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputHandlerInfo"/> class.
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="config"></param>
        public OutputHandlerInfo(string typeName, Config config)
            : this(Type.GetType(typeName, true)!, config)
        {
        }

        /// <summary>
        /// Gets the Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="param"></param>
        public void Handle(IHandleMessage param)
            => _Handlers[param.GetType()].Invoke(_Instance, new object[] { param });
    }
}
