﻿using Eventos.IO.Domain.Core.Events;
using System;

namespace Eventos.IO.Domain.Eventos.Events
{
    public class EventoEventHandler :
        IHandler<EventoRegistradoEvent>,
        IHandler<EventoAtualizadoEvent>,
        IHandler<EventoExcluidoEvent>
    {
        public void Handle(EventoRegistradoEvent message)
        {
            // Enviar um e-mail
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento Registrado com Sucesso");
        }

        public void Handle(EventoAtualizadoEvent message)
        {
            // Enviar um e-mail
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento Atualizado com Sucesso");
        }

        public void Handle(EventoExcluidoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento Excluido com Sucesso");
        }
    }
}