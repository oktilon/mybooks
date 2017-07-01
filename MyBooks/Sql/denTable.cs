using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MyBooks
{
    public partial class denSQL
    {
        public class denTable : IDisposable
        {
            private string sTblName = "";
            private string sIdName = "";
            private denSQL m_sql = null;
            /// <summary>
            /// Key=Field name, Value=FieldValue
            /// </summary>
            public Dictionary<string, string> Fields = new Dictionary<string, string>();
            private List<string> IsParam = new List<string>();

            /// <summary>
            /// Создание массива для записи в таблицу
            /// </summary>
            /// <param name="sName">Имя таблицы</param>
            /// <param name="sIdFieldName">Имя ключевого поля</param>
            /// <param name="sql">БД</param>
            public denTable(string sName, string sIdFieldName, denSQL sql) { sTblName = sName; sIdName = sIdFieldName; m_sql = sql; }

            /// <summary>
            /// Добавить текстовое поле или NULL
            /// </summary>
            public void AddNul(string sField, string oVal, int iLimit = 0, string sNull = "") { Fields.Add(sField, oVal.EscN(iLimit, sNull)); }
            /// <summary>
            /// Добавить текстовое поле NOT NULL
            /// </summary>
            public void AddFld(string sField, string oVal, int iLimit = 0) { Fields.Add(sField, "'" + oVal.Esc(iLimit) + "'"); }
            /// <summary>
            /// Добавить целое поле INTEGER
            /// </summary>
            public void AddFld(string sField, int oVal) { Fields.Add(sField, oVal.ToString()); }
            /// <summary>
            /// Добавить целое поле без знака UINT
            /// </summary>
            public void AddFld(string sField, uint oVal) { Fields.Add(sField, oVal.ToString()); }
            /// <summary>
            /// Добавить дробное поле DOUBLE
            /// </summary>
            public void AddFld(string sField, double oVal) { Fields.Add(sField, oVal.ToString(Cif_sql)); }
            /// <summary>
            /// Добавить дробное поле DOUBLE (Decimal)
            /// </summary>
            public void AddFld(string sField, decimal oVal) { Fields.Add(sField, oVal.ToString(Cif_sql)); }
            /// <summary>
            /// Добавить поле даты DATE(TIME)
            /// </summary>
            public void AddFld(string sField, DateTime oVal, bool bWithTime = false) { Fields.Add(sField, "'" + oVal.ToString(bWithTime ? "yyyy-MM-dd HH:mm:ss" : "yyyy-MM-dd") + "'"); }
            /// <summary>
            /// Добавить поле даты DATE(TIME)?-Nullable
            /// </summary>
            public void AddFld(string sField, DateTime? oVal, bool bWithTime = false) { Fields.Add(sField, oVal == null ? "NULL" : string.Format(bWithTime ? "'{0:yyyy-MM-dd HH:mm:ss}'" : "'{0:yyyy-MM-dd}'", oVal)); }
            /// <summary>
            /// Добавить поле даты или NULL
            /// </summary>
            public void AddNul(string sField, DateTime oVal, bool bWithTime = false) { Fields.Add(sField, oVal.EscN(bWithTime)); }
            /// <summary>
            /// Добавить большое текстовое поле TEXT 
            /// </summary>
            public void AddTXT(string sField, string oBigVal)
            {
                if (oBigVal == "")
                    AddNul(sField, "");
                else
                {
                    Fields.Add(sField, oBigVal);
                    IsParam.Add(sField);
                }
            }

            private int Update(int Id, bool bAddIdField = false)
            {
                MySqlCommand cmd = null;
                try
                {
                    if (bAddIdField) AddFld(sIdName, Id);
                    string v = "";
                    foreach (KeyValuePair<string, string> fv in Fields)
                        v += (v == "" ? "" : ", ") + fv.Key + "=" + (IsParam.Contains(fv.Key) ? ("@" + fv.Key) : fv.Value);
                    cmd = new MySqlCommand("UPDATE " + sTblName + " SET " + v + " WHERE " + sIdName + "=" + Id.ToString(), m_sql.sqlConn);
                    if (cmd == null) return 0;
                    foreach (string sP in IsParam)
                        cmd.Parameters.AddWithValue("@" + sP, Fields[sP]);

                    logHandler?.Invoke(string.Format("{0:dd.MM.yyyy HH:mm:ss}: {1}", DateTime.Now, cmd.CommandText));
                    return cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex) { errorHandler?.Invoke(ex, "", cmd != null ? cmd.CommandText : ""); return 0; }
            }

            private int Insert(ref int Id)
            {
                MySqlCommand cmd = null;
                int i = 0;
                try
                {
                    string f = "";
                    string v = "";
                    foreach (KeyValuePair<string, string> fv in Fields)
                    {
                        v += (v == "" ? "" : ", ") + (IsParam.Contains(fv.Key) ? ("@" + fv.Key) : fv.Value);
                        f += (f == "" ? "" : ", ") + fv.Key;
                    }
                    cmd = new MySqlCommand("INSERT INTO " + sTblName + " (" + f + ") VALUES (" + v + ")", m_sql.sqlConn);
                    if (cmd == null) return 0;
                    foreach (string sP in IsParam)
                        cmd.Parameters.AddWithValue("@" + sP, Fields[sP]);

                    logHandler?.Invoke(string.Format("{0:dd.MM.yyyy HH:mm:ss}: {1}", DateTime.Now, cmd.CommandText));
                    i = cmd.ExecuteNonQuery();
                    if (i > 0) Id = (int)cmd.LastInsertedId;
                }
                catch (MySqlException ex) { errorHandler?.Invoke(ex, "", cmd != null ? cmd.CommandText : ""); }
                return i;
            }

            /// <summary>
            /// Сохранить или обновить таблицу
            /// </summary>
            /// <param name="Id">Задать код обновляемой строки или получить код сохраненной</param>
            /// <returns>Число внесенных/измененных строк</returns>
            public int Store(ref int Id)
            {
                if (Id < 0) return 0;
                if (Fields.Count == 0) return 0;
                if (m_sql == null) return 0;
                if (!m_sql.IsConnected) return 0;
                if (m_sql.ReadOnly) return 0;

                lock (m_sql.sqlConn)
                {
                    if (!m_sql.TestConnection()) return 0;
                    if (Id == 0)
                        return Insert(ref Id);
                    return Update(Id);
                }
            }

            /// <summary>
            /// Сохранить или обновить таблицу
            /// </summary>
            /// <param name="Id">Задать код обновляемой строки или получить код сохраненной</param>
            /// <returns>Число внесенных/измененных строк</returns>
            public int StoreWithId(int Id)
            {
                int i = Id;
                if (Fields.Count == 0) return 0;
                if (m_sql == null) return 0;
                if (!m_sql.IsConnected) return 0;
                if (m_sql.ReadOnly) return 0;

                lock (m_sql.sqlConn)
                {
                    if (!m_sql.TestConnection()) return 0;
                    if (Id == 0)
                        return Insert(ref i);
                    return Update(Id, true);
                }
            }

            void IDisposable.Dispose()
            {
                Fields.Clear();
                IsParam.Clear();
                Fields = null;
                IsParam = null;
            }
        }
    }
}