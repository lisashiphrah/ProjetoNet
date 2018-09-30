using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public SelectList Categorias()
        {
            return new SelectList(ListarCategorias(), "Id", "Nome");
        }

        public List<CategoriaViewModel> ListarCategorias()
        {
            var categoriasList = new List<CategoriaViewModel>()
            {
                new CategoriaViewModel(){ Id = new Guid("a458cfca-2c1f-4504-95af-441689ccfadb"), Nome = "Congresso"},
                new CategoriaViewModel(){ Id = new Guid("c20493f9-14ba-45db-919e-97a75aa1df71"), Nome = "Meetup"},
                new CategoriaViewModel(){ Id = new Guid("4710c722-2aa1-4f92-a57a-3d2d3b418bec"), Nome = "Workshop"}
            };

            return categoriasList;
        }
    }
}
