using Domain.Endpoint.Entities;
using System.Collections.Generic;
using System;
using Domain.Endpoint.Interfaces.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class ProductoRepository : IProductoRepository
    {

        private readonly ISqlDbConnection _sqlDbConnection;
        public ProductoRepository(ISqlDbConnection sqlDbConnection)
        {
            _sqlDbConnection = sqlDbConnection;
        }

        public void CreateProducto(Producto nuevoProducto)
        {

            const string sqlQuery = "INSERT INTO Producto (idProducto, NombreProducto, DescripcionProduct, Expiracion, UnidadMedida) " +
                        "VALUES (@idProducto, @NombreProducto, @DescripcionProduct, @Expiracion, @UnidadMedida)";

            SqlCommand cmd = _sqlDbConnection.TraerConsulta(sqlQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@idProducto",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = nuevoProducto.Id
                },
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@NombreProducto",
                    SqlDbType =SqlDbType.VarChar,
                    Value =nuevoProducto.NombreProducto
                },
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@DescripcionProduct",
                    SqlDbType =SqlDbType.VarChar,
                    Value = nuevoProducto.DescripcionProduct
                },
                    new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@Expiracion",
                    SqlDbType =SqlDbType.DateTime,
                    Value = nuevoProducto.Expiracion
                },
                   new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@UnidadMedida",
                    SqlDbType =SqlDbType.Float,
                    Value = nuevoProducto.UnidadMedida

                }

            };

            cmd.Parameters.AddRange(parameters);
            cmd.ExecuteNonQuery();
        }

        public void DeleteProducto(Guid Id)
        {
            string delec = "DELETE FROM Producto WHERE idProducto = @idProducto";
            SqlCommand sqlCommand = _sqlDbConnection.TraerConsulta(delec);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@idProducto",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        //Ver
        public async Task<List<Producto>> Get()
        {

            string query = "SELECT * FROM Producto;";
            DataTable dataTable = await _sqlDbConnection.ExecuteQueryCommandAsync(query);
            List<Producto> Cet = dataTable.AsEnumerable().Select(MapEntityFromDataRow).ToList();

            return Cet;


        }
        public Producto MapEntityFromDataRow(DataRow row)
        {
            Producto producto = new Producto
            {
                Id = _sqlDbConnection.GetDataRowValue<Guid>(row, "idProducto"),
                NombreProducto = _sqlDbConnection.GetDataRowValue<string>(row, "NombreProducto"),
                DescripcionProduct = _sqlDbConnection.GetDataRowValue<string>(row, "DescripcionProduct"),
                Expiracion = _sqlDbConnection.GetDataRowValue<DateTime>(row, "Expiracion"),
                UnidadMedida = Convert.ToSingle(_sqlDbConnection.GetDataRowValue<object>(row, "UnidadMedida")),
            };
            return producto;
        }


        public void UpdateProducto(Guid Id, Producto nuevosRegistros)
        {
            const string sqlQuery = "UPDATE Producto  SET  NombreProducto=@NombreProducto, DescripcionProduct=@DescripcionProduct, Expiracion=@Expiracion," +
            "UnidadMedida=@UnidadMedida WHERE idProducto=@idProducto";

            SqlCommand cmd = _sqlDbConnection.TraerConsulta(sqlQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@idProducto",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = nuevosRegistros.Id
                },
                 new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@NombreProducto",
                    SqlDbType =SqlDbType.VarChar,
                    Value =nuevosRegistros.NombreProducto
                },
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@DescripcionProduct",
                    SqlDbType =SqlDbType.VarChar,
                    Value = nuevosRegistros.DescripcionProduct
                },
                    new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@Expiracion",
                    SqlDbType =SqlDbType.DateTime,
                    Value = nuevosRegistros.Expiracion
                },
                   new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@UnidadMedida",
                    SqlDbType =SqlDbType.Float,
                    Value = nuevosRegistros.UnidadMedida

                }
            };
            cmd.Parameters.AddRange(parameters);
            cmd.ExecuteNonQuery();
        }
    }
}



