using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CMSite.Extension
{
    public static class MyClassExtension
    {
        /// <summary>
        /// Convert a IEnumerable{T} to a DataTable.
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            DataRow dr = null;
            PropertyInfo pInfo = null;
            foreach (T item in items)
            {
                dr = tb.NewRow();

                foreach(DataColumn col in dr.Table.Columns)
                {
                    pInfo = props.Where(p => p.Name == col.ColumnName).FirstOrDefault();
                    if(pInfo != null)
                    {
                        dr[col] = pInfo.GetValue(item);
                    }
                }

                tb.Rows.Add(dr);
            }

            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        private static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        private static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
    }
}