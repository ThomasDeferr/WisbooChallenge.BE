using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace WisbooChallenge.Data.Core
{
    public class DBManager : IDBManager
    {
        private readonly DatabaseOptions _databaseOptions;

        public DBManager(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions.Value;
        }


        #region Public methods
        public async Task<IEnumerable<DataRow>> Get(string storedProcedure, List<SqlParameter> sqlParameters = null)
        {
            DataTable result = null;

            #region SQL Execution
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters.ToArray());
                    }

                    await connection.OpenAsync();

                    DataTable dataResult = new DataTable();

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataResult);
                    }

                    await CloseConnection(connection);

                    result = dataResult;
                }
            }
            #endregion

            return result.AsEnumerable().ToList();
        }

        public async Task<DataRow> GetSingle(string storedProcedure, List<SqlParameter> sqlParameters = null)
        {
            DataTable result = null;

            #region SQL Execution
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters.ToArray());
                    }

                    await connection.OpenAsync();

                    DataTable dataResult = new DataTable();

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataResult);
                    }

                    await CloseConnection(connection);

                    result = dataResult;
                }
            }
            #endregion

            return result.AsEnumerable().SingleOrDefault();
        }

        public async Task Insert(string storedProcedure, List<SqlParameter> sqlParameters = null)
        {
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters.ToArray());
                    }

                    await connection.OpenAsync();
                    await command.ExecuteScalarAsync();

                    await CloseConnection(connection);
                }
            }
        }

        public async Task Update(string storedProcedure, List<SqlParameter> sqlParameters = null)
        {
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters.ToArray());
                    }

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    await CloseConnection(connection);
                }
            }
        }

        public async Task Delete(string storedProcedure, List<SqlParameter> sqlParameters = null)
        {
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters.ToArray());
                    }

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    await CloseConnection(connection);
                }
            }
        }

        #region Parameters
        public SqlParameter CreateOutputParameter(string parameterName, SqlDbType sqlDbType)
        {
            return this.CreateParameter(ParameterDirection.Output, parameterName, sqlDbType);
        }
        public SqlParameter CreateOutputParameter(string parameterName, SqlDbType sqlDbType, int size)
        {
            return this.CreateParameter(ParameterDirection.Output, parameterName, sqlDbType, size);
        }

        public SqlParameter CreateInputParameter(string parameterName, object value)
        {
            return this.CreateParameter(ParameterDirection.Input, parameterName, value);
        }
        public SqlParameter CreateInputParameter(string parameterName, SqlDbType sqlDbType)
        {
            return this.CreateParameter(ParameterDirection.Input, parameterName, sqlDbType);
        }
        public SqlParameter CreateInputParameter(string parameterName, object value, SqlDbType sqlDbType)
        {
            return this.CreateParameter(ParameterDirection.Input, parameterName, value, sqlDbType);
        }
        #endregion
        #endregion


        #region Private methods
        private SqlParameter CreateParameter(ParameterDirection direction, string parameterName, SqlDbType sqlDbType)
        {
            SqlParameter parameter = new SqlParameter(parameterName, sqlDbType);
            parameter.IsNullable = true;
            parameter.Direction = direction;
            return parameter;
        }
        private SqlParameter CreateParameter(ParameterDirection direction, string parameterName, SqlDbType sqlDbType, int size)
        {
            SqlParameter parameter = this.CreateParameter(direction, parameterName, sqlDbType);
            parameter.Size = size;
            return parameter;
        }

        private SqlParameter CreateParameter(ParameterDirection direction, string parameterName, object value)
        {
            SqlParameter parameter = new SqlParameter(parameterName, value ?? DBNull.Value);
            parameter.IsNullable = true;
            parameter.Direction = direction;
            return parameter;
        }
        private SqlParameter CreateParameter(ParameterDirection direction, string parameterName, object value, SqlDbType sqlDbType)
        {
            SqlParameter parameter = this.CreateParameter(direction, parameterName, value);
            parameter.SqlDbType = sqlDbType;
            return parameter;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_databaseOptions.ConnectionString);
        }

        private async Task CloseConnection(SqlConnection sqlConnection)
        {
            await sqlConnection.CloseAsync();
            await sqlConnection.DisposeAsync();
        }
        #endregion
    }
}