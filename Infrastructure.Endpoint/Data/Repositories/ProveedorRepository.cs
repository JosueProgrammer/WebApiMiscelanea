using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {

        private readonly ISqlDbConnection _sqlDbConnection;
        public ProveedorRepository(ISqlDbConnection sqlDbConnection)
        {
            _sqlDbConnection = sqlDbConnection;
        }

        public void CreateProveedor(Proveedor nuevoProveedor)
        {
            const string sqlQuery = "INSERT INTO Proveedor(idProveedor,NombreEmpresa,Direccion,Telefono,correo,FechaRegistro) Values (@idProveedor, @NombreEmpresa,@Direccion,@Telefono,@correo,@FechaRegistro )";

            SqlCommand cmd = _sqlDbConnection.TraerConsulta(sqlQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@idProveedor",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = nuevoProveedor.Id
                },
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@NombreEmpresa",
                    SqlDbType =SqlDbType.VarChar,
                    Value =nuevoProveedor.NombreEmpresa
                },
                new SqlParameter() 
                {
                    Direction=ParameterDirection.Input, 
                    ParameterName="@Direccion",
                    SqlDbType=SqlDbType.VarChar,
                    Value=nuevoProveedor.Direccion
                },
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@Telefono",
                    SqlDbType =SqlDbType.Int,
                    Value = nuevoProveedor.Telefono
                },
                  new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@correo",
                    SqlDbType =SqlDbType.VarChar,
                    Value = nuevoProveedor.correo
                },
                      new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@FechaRegistro",
                    SqlDbType =SqlDbType.DateTime,
                    Value = nuevoProveedor.FechaRegistro
                }

            };

            cmd.Parameters.AddRange(parameters);
            cmd.ExecuteNonQuery();
        }

        public void DeleteProveedor(Guid Id)
        {
            string delec = "DELETE FROM Proveedor WHERE idProveedor = @idProveedor";
            SqlCommand sqlCommand = _sqlDbConnection.TraerConsulta(delec);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@idProveedor",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task<List<Proveedor>> Get()
        {
            string query = "SELECT * FROM Proveedor;";
            DataTable dataTable = await _sqlDbConnection.ExecuteQueryCommandAsync(query);
            List<Proveedor> Ctec = dataTable.AsEnumerable().Select(MapEntityFromDataRow).ToList();

            return Ctec;
        }

        public Proveedor MapEntityFromDataRow(DataRow row)
        {
            return new Proveedor()
            {
                Id = _sqlDbConnection.GetDataRowValue<Guid>(row, "idProveedor"),
                NombreEmpresa = _sqlDbConnection.GetDataRowValue<string>(row, "NombreEmpresa"),
                Direccion = _sqlDbConnection.GetDataRowValue<string>(row, "Direccion"),
                Telefono = _sqlDbConnection.GetDataRowValue<int>(row, "Telefono"),
                correo = _sqlDbConnection.GetDataRowValue<string>(row, "correo"),
                FechaRegistro = _sqlDbConnection.GetDataRowValue<DateTime>(row, "FechaRegistro"),

            };
        }

        public void UpdateProveedor(Guid Id, Proveedor nuevosRegistros)
        {
            const string sqlQuery = "UPDATE Proveedor SET NombreEmpresa = @NombreEmpresa,Direccion=@Direccion, Correo = @Correo, Telefono = @Telefono, FechaRegistro = @FechaRegistro WHERE idProveedor = @idProveedor";

            SqlCommand cmd = _sqlDbConnection.TraerConsulta(sqlQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@idProveedor",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value =nuevosRegistros.Id
                },
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@NombreEmpresa",
                    SqlDbType =SqlDbType.VarChar,
                    Value =nuevosRegistros.NombreEmpresa
                },
                new SqlParameter()
                {
                    Direction=ParameterDirection.Input,
                    ParameterName="@Direccion",
                    SqlDbType=SqlDbType.VarChar,
                    Value=nuevosRegistros.Direccion
                },
                new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@Telefono",
                    SqlDbType =SqlDbType.Int,
                    Value = nuevosRegistros.Telefono
                },
                  new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@correo",
                    SqlDbType =SqlDbType.VarChar,
                    Value = nuevosRegistros.correo
                },
                      new SqlParameter()
                {
                    Direction = ParameterDirection.Input,
                    ParameterName ="@FechaRegistro",
                    SqlDbType =SqlDbType.DateTime,
                    Value = nuevosRegistros.FechaRegistro
                }


            };

            cmd.Parameters.AddRange(parameters);
            cmd.ExecuteNonQuery();
        }
    }
}




