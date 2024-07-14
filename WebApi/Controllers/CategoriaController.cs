using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using Domain.Endpoint.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class CategoriaController : ApiController
    {
        private readonly ICategoria _CategoriaService;

        public CategoriaController(ICategoria CategoriaService)
        {
            _CategoriaService = CategoriaService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCategoria()
        {
            List<Categoria> cliente = await _CategoriaService.GetAll();

            return Ok(cliente);
        }

        [HttpPost]
        public IHttpActionResult PostCategoria(Categoria nuevaCategoria)
        {
            Categoria newCategoria = _CategoriaService.CreateCategoria(nuevaCategoria);

            return Ok(newCategoria);
        }


        [HttpDelete]
        public IHttpActionResult DeleteCategoria(Guid Id)
        {
            _CategoriaService.DeleteCategoria(Id);

            return Ok("La categoria seleccionado ha sido eliminado");
        }

        [HttpPut]
        public IHttpActionResult UpdateCategoria(Guid Id, Categoria nuevosRegistros)
        {
            _CategoriaService.UpdateCategoria(Id, nuevosRegistros);

            return Ok("La categoria  seleccinado ha sido modificado");
        }

    }
}
