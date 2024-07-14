using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class CategoriaService : Interfaces.Services.ICategoria
    {
        private readonly Interfaces.Repositories.ICategoria _CategoriaRepository;

        public CategoriaService(Interfaces.Repositories.ICategoria CategoriaRepository)
        {
            _CategoriaRepository = CategoriaRepository;
        }


        public Categoria CreateCategoria(Categoria nuevaCategoria)
        {
            Categoria newCategoria = new Categoria()
            {
                Id = Guid.NewGuid(),
                DescripcionCategoria=nuevaCategoria.DescripcionCategoria, 

            };

            _CategoriaRepository.CreateCategoria(newCategoria);
            return newCategoria;
        }



        public void DeleteCategoria(Guid Id)
        {
            _CategoriaRepository.DeleteCategoria(Id);
        }



        public Task<List<Categoria>> GetAll()
        {
            return _CategoriaRepository.Get();
        }

        public void UpdateCategoria(Guid Id, Categoria nuevosRegistros)
        {
            _CategoriaRepository.UpdateCategoria(Id, nuevosRegistros);
        }

    }
}
