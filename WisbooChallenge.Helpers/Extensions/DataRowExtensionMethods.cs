using System;
using System.Data;

namespace WisbooChallenge.Helpers.Extensions
{
    public static class DataRowExtensionMethods
    {
        public static T GetValue<T>(this DataRow row, string column)
        {
            object value = (row.Table.Columns.Contains(column) ? row[column] : null);
            object result = value != DBNull.Value ? value : null;

            if (result != null)
            {
                Type t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                return (T)Convert.ChangeType(result, t);
            }
            else
            {
                return (T)result;
            }
        }
    }
}