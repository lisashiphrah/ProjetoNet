﻿using Eventos.IO.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Eventos.IO.Domain.Eventos.Repository
{
    public interface IEventoRepository : IRepository<Evento>
    {
        IEnumerable<Evento> ObterEventoPorOrganizador(Guid organicadorId);
        Endereco ObterEnderecoPorId(Guid id);
        void AdicionarEndereco(Endereco endereco);
        void AtualizarEndereco(Endereco endereco);
        IEnumerable<Categoria> ObterCategorias();
    }
}
