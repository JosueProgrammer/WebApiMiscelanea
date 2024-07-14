using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface ICategoria
    {

        Task<List<Categoria>> GetAll();

        Categoria CreateCategoria(Categoria nuevaCategoria);

        void DeleteCategoria(Guid Id);

        void UpdateCategoria(Guid Id, Categoria nuevoRegistros);
    }
}
