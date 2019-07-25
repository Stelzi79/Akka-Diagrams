using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AkkaDiagram.Actors.Messages
{
    internal class OutputHandlerInfo
    {
        private const string NO_INSTANCE = "Not able to instantiate with default constructor";
        private const string NO_CONSTRUCTOR = "No default constructor present";
        private readonly Dictionary<Type, MethodInfo> _Handlers = new Dictionary<Type, MethodInfo>();
        private readonly object _Instance;

        public OutputHandlerInfo(string typeName)
        {
            try
            {
                var type = Type.GetType(typeName, true);
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
                throw new ArgumentException($"{typeName} is no valid OutputHandler", nameof(typeName), e);
            }
        }

        public void Handle(IHandleMessage param)
            => _Handlers[param.GetType()].Invoke(_Instance, new object[] { param });
    }
}
