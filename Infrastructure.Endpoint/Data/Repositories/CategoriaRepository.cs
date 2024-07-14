using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class CategoriaRepository : ICategoria
    {
        private readonly ISqlDbConnection _sqlDbConnection;
        public CategoriaRepository(ISqlDbConnection sqlDbConnection)
        {
            _sqlDbConnection = sqlDbConnection;
        }

        public void CreateCategoria(Categoria nuevaCategoria)
        {

            const string sqlQuery = "INSERT INTO Categoria (idCategoria, DescripcionCategoria" + ") Values (@idCategoria, @DescripcionCategoria)";

            SqlCommand cmd = _sqlDbConnection.TraerConsulta(sqlQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@idCategoria",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = nuevaCategoria.Id
                },
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@DescripcionCategoria",
                    SqlDbType =SqlDbType.VarChar,
                    Value =nuevaCategoria.DescripcionCategoria
                }
            };

            cmd.Parameters.AddRange(parameters);
            cmd.ExecuteNonQuery();
        }

        public void DeleteCategoria(Guid Id)
        {
            
            string delec = "DELETE FROM  Categoria WHERE idCategoria = @idCategoria";
            SqlCommand sqlCommand = _sqlDbConnection.TraerConsulta(delec);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@idCategoria",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        //Ver
        public async Task<List<Categoria>> Get()
        {

            string query = "SELECT * FROM Categoria;";
            DataTable dataTable = await _sqlDbConnection.ExecuteQueryCommandAsync(query);
            List<Categoria> Ct = dataTable.AsEnumerable().Select(MapEntityFromDataRow).ToList();

            return Ct;

        }

        public Categoria MapEntityFromDataRow(DataRow row)
        {
            return new Categoria()
            {
                Id = _sqlDbConnection.GetDataRowValue<Guid>(row, "idCategoria"),
                DescripcionCategoria = _sqlDbConnection.GetDataRowValue<string>(row, "DescripcionCategoria")
            };

        }

        public void UpdateCategoria(Guid Id, Categoria nuevosRegistros)
        {
            const string sqlQuery = "UPDATE Categoria SET  DescripcionCategoria=@DescripcionCategoria WHERE idCategoria = @idCategoria";

            SqlCommand cmd = _sqlDbConnection.TraerConsulta(sqlQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@idCategoria",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = nuevosRegistros.Id
                },
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@DescripcionCategoria",
                    SqlDbType =SqlDbType.NVarChar,
                    Value =nuevosRegistros.DescripcionCategoria
                }
            };

            cmd.Parameters.AddRange(parameters);
            cmd.ExecuteNonQuery();
        }
    }
}
