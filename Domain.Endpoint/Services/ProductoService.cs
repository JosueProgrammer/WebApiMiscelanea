using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System.Collections.Generic;
using System;
using Domain.Endpoint.Interfaces.Services;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository ProductoRepository)
        {
            _productoRepository = ProductoRepository;
        }

        public Producto CreateProducto(Producto nuevoProducto)
        {
            Producto newProducto = new Producto()
            {
                Id = Guid.NewGuid(),
                NombreProducto = nuevoProducto.NombreProducto,
                DescripcionProduct = nuevoProducto.DescripcionProduct,
                Expiracion= nuevoProducto.Expiracion,
                UnidadMedida=nuevoProducto.UnidadMedida

            };
            _productoRepository.CreateProducto(newProducto);
            return newProducto;
        }


        public void DeleteProducto(Guid Id)
        {
            _productoRepository.DeleteProducto(Id);
        }

        public Task<List<Producto>> GetAll()
        {
            return _productoRepository.Get();
        }

        public void UpdateProducto(Guid Id, Producto nuevoRegistros)
        {
            _productoRepository.UpdateProducto(Id, nuevoRegistros);
        }
    }
}
