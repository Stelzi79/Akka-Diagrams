using System;
using System.Collections.Generic;
using System.Text;
using AkkaDiagram.Actors.Messages;

namespace AkkaDiagram.Actors.Handlers
{
    public interface IOutputHandler
    {
        void Handle(UnsubscribeFromAll msg);
    }
}
