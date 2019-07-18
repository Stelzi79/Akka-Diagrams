using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace AkkaDiagram.Actors.Messages
{
    internal class OutputHandlerInfo
    {
        private readonly Dictionary<Type, MethodInfo> _Handlers = new Dictionary<Type, MethodInfo>();
        private readonly object _Instance;
        public OutputHandlerInfo(string typeName)
        {
            var type = Type.GetType(typeName);

            _Instance = type.GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
            var handleMethods = type.GetMethods().Where(m => m.Name == "Handle" && m.ReturnType == typeof(void));
            foreach (var method in handleMethods)
            {
                _Handlers.Add(method.GetParameters()[0].ParameterType, method);
            }
        }
        public void Handle(IHandleMessage param)
            => _Handlers[param.GetType()].Invoke(_Instance, new object[] { param });


    }
}
