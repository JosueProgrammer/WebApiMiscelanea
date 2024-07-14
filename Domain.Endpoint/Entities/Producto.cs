using System;

namespace Domain.Endpoint.Entities
{
    public class Producto : BaseEntity
    {
        public string NombreProducto { get; set; }
        public string DescripcionProduct { get; set; }
        public DateTime  Expiracion { get; set; }   
        public float UnidadMedida { get;  set; }

    }
}


