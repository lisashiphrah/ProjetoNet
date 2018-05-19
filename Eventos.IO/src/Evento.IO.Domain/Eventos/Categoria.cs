using Eventos.IO.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Eventos.IO.Domain.Eventos
{
    public class Categoria : Entity<Categoria>
    {
        public Categoria(Guid id)
        {
            Id = id;
        }

        public string Nome { get; private set; }

        // EF Propriedades de Navegação
        public virtual ICollection<Evento> Eventos { get; private set; }

        // Contrutor para o EF
        protected Categoria() { }

        public override bool EhValido()
        {
            return true;
        }
    }
}