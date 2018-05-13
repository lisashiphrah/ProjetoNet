using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Eventos.Commands;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new FakeBus();

            // Registro com sucesso
            var cmd = new RegistrarEventoCommand("DevX", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), true, 0, true, "Empresa");
            Inicio(cmd);
            bus.SendCommands(cmd);
            Fim(cmd);

            // Registro com erros
            cmd = new RegistrarEventoCommand("", DateTime.Now.AddDays(2), DateTime.Now.AddDays(1), false, 0, false, "");
            Inicio(cmd);
            bus.SendCommands(cmd);
            Fim(cmd);

            // Atualizar Eventos
            var cmd2 = new AtualizarEventoCommand(Guid.NewGuid(), "DevX","","", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), false, 50, true, "Empresa");
            Inicio(cmd2);
            bus.SendCommands(cmd2);
            Fim(cmd2);

            // Excluir Evento
            var cmd3 = new ExcluirEventoCommand(Guid.NewGuid());
            Inicio(cmd3);
            bus.SendCommands(cmd3);
            Fim(cmd3);

            Console.ReadKey();
        }

        private static void Inicio(Message message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Inicio do commando " + message.MessageType);
        }

        private static void Fim(Message message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Fim do commando " + message.MessageType);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*******************");
            Console.WriteLine("");
        }
    }
}
