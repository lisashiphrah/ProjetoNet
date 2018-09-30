using Eventos.IO.Domain.Core.Commands;
using Eventos.IO.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Core.Bus
{
    public interface IBus
    {
        void SendCommands<T>(T theCommand) where T : Command;

        void RaiseEvent<T>(T theEvent) where T : Event;
    }
}
