using System;

namespace MyBooks
{
    public static class denSQLExtension {

        /// <summary>
        /// Подготовка строки к запросу
        /// </summary>
        public static string Esc(this string @base, int iLimit = 0)
        {
            string ret = MySql.Data.MySqlClient.MySqlHelper.EscapeString(@base);
            if (iLimit > 0)
            {
                int iSz = @base.Length;
                while (ret.Length > iLimit)
                    ret = MySql.Data.MySqlClient.MySqlHelper.EscapeString(@base.Substring(0, --iSz));
            }
            return ret;
        }

        /// <summary>
        /// Подготовка строки к запросу или NULL
        /// </summary>
        public static string EscN(this string @base, int iLimit = 0, string sNull = "") { return @base == sNull ? "NULL" : "'" + @base.Esc(iLimit) + "'"; }

        /// <summary>
        /// Подготовка даты к запросу в БД или NULL
        /// </summary>
        public static string EscN(this DateTime @base, bool bWithTime = false) { return @base.Year == 1 ? "NULL" : string.Format(denSQL.Cif_sql, bWithTime ? "'{0:yyyy-MM-dd HH:mm:ss}'" : "'{0:yyyy-MM-dd}'", @base); }

    }
}