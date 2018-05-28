using Eventos.IO.Domain.Core.Events;
using System;

namespace Eventos.IO.Domain.Eventos.Events
{
    public class EventoEventHandler :
        IHandler<EventoRegistradoEvent>,
        IHandler<EventoAtualizadoEvent>,
        IHandler<EventoExcluidoEvent>,
        IHandler<EnderecoEventoAdicionadoEvent>,
        IHandler<EnderecoEventoAtualizadoEvent>
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

        public void Handle(EnderecoEventoAdicionadoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Endereço do evento adicionado com Sucesso");
        }

        public void Handle(EnderecoEventoAtualizadoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Endereço do evento atualizado com Sucesso");
        }
    }
}
