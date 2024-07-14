using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface ICategoria
    {
        Task<List<Categoria>> Get();


        void CreateCategoria(Categoria nuevaCategoria);

        void DeleteCategoria(Guid Id);

        void UpdateCategoria(Guid Id, Categoria nuevosRegistros);
    }
}
