using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WisbooChallenge.Data.Core
{
    public interface IDBManager
    {
        Task<IEnumerable<DataRow>> Get(string storedProcedure, List<SqlParameter> sqlParameters = null);
        Task<DataRow> GetSingle(string storedProcedure, List<SqlParameter> sqlParameters = null);
        Task Insert(string storedProcedure, List<SqlParameter> sqlParameters = null);
        Task Update(string storedProcedure, List<SqlParameter> sqlParameters = null);
        Task Delete(string storedProcedure, List<SqlParameter> sqlParameters = null);

        SqlParameter CreateOutputParameter(string parameterName, SqlDbType sqlDbType);
        SqlParameter CreateOutputParameter(string parameterName, SqlDbType sqlDbType, int size);
        SqlParameter CreateInputParameter(string parameterName, SqlDbType sqlDbType);
        SqlParameter CreateInputParameter(string parameterName, object value);
        SqlParameter CreateInputParameter(string parameterName, object value, SqlDbType sqlDbType);
    }
}