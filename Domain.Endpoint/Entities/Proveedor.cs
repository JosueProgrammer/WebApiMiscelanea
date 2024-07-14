using System;
using System.Data;

namespace Domain.Endpoint.Entities
{

    public class Proveedor : BaseEntity
    {
        public string NombreEmpresa { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string correo { get; set; }
        
        public DateTime FechaRegistro { get; set; }
    }
}