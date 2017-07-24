using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace MyBooks
{
    public partial class denSQL
    {
        // connections
        private MySqlConnection sqlConn = null;

        // commands
        private MySqlCommand sqlCmd = null;
        private bool bHasReader;

        // transactions
        private MySqlTransaction sqlTrans = null;


        public String m_ConnString = "";
        public String m_Database = "books";
        public String m_user = "";
        public String m_pwd = "";

        public bool ReadOnly = false;
        public bool IsConnectedMaster = false;
        public bool IsConnectedRelay = false;


        public delegate void SqlErrorHandler(Exception ex, string caption, string sql);
        public delegate void SqlLogHandler(string sLine);

        private static denSQL m_dn_sql = null;
        private static System.Globalization.CultureInfo cif = null;
        public static DateTime DateNone = new DateTime(1, 1, 1);
        public static SqlErrorHandler errorHandler = null;
        public static SqlLogHandler logHandler = null;

        public denSQL(string sServ, string sDBName, string sUser, string sPwd)
        {
            m_user = sUser;
            m_pwd = sPwd;
            m_Database = sDBName == "" ? m_Database : sDBName;
            m_ConnString = string.Format("server={0};user id={1}; password={2}; database={3}; pooling=false; CharSet=utf8", sServ, m_user, m_pwd, m_Database);
            Connect(false);
        }
        ~denSQL() { if (sqlConn != null) sqlConn.Close(); }

        void Connect(bool bSilent)
        {
            IsConnectedMaster = false;
            try
            {
                if (sqlConn == null) sqlConn = new MySqlConnection(m_ConnString);
                if (sqlConn.State == ConnectionState.Open) sqlConn.Close();
                sqlConn.Open();
            }
            catch (MySqlException ex)
            {
                if (bSilent) return;
                if (ex.Number == 1042) { return; } // Server unricheable
                ProceedErrorSql(ex, "denSql.Connect, OpenDB");
            }
            try
            {
                if (sqlConn == null || sqlConn.State != ConnectionState.Open) return;
                MySqlCommand cmd = new MySqlCommand("SET NAMES utf8", sqlConn);
                int tmp = cmd.ExecuteNonQuery();
                IsConnectedMaster = true;
            }
            catch (MySqlException ex) { ProceedErrorSql(ex, "denSql.Connect, QueryVariables", "SHOW VARIABLES LIKE 'basedir'"); }
        }

        public bool TestConnection()
        {
            if (sqlConn == null || sqlConn.State != ConnectionState.Open)
            {
                IsConnectedMaster = false;
                ReadOnly = true;
                return false;
            }
            bool bPing = sqlConn.Ping();
            IsConnectedMaster = bPing;
            ReadOnly = !bPing;
            return bPing;
        }

        public static void ProceedErrorSql(MySqlException mex, string sCaption, string sql = "")
        {
            string msg = string.Format("{0} (Err#{1})", sCaption, mex.Number.ToString());
            errorHandler?.Invoke(mex, msg, sql);
        }

        public static System.Globalization.CultureInfo Cif_sql
        {
            get
            {
                if (cif == null) {
                    cif = new System.Globalization.CultureInfo("ru-RU");
                    cif.NumberFormat.NumberDecimalSeparator = ".";
                    cif.NumberFormat.CurrencyDecimalSeparator = ".";
                    cif.NumberFormat.CurrencyDecimalDigits = 2;
                    cif.NumberFormat.CurrencySymbol = "";
                }
                return cif;
            }
        }

        public static denSQL m_sql
        {
            get
            {
                if (m_dn_sql == null) throw new Exception("denSQL not initialized yet!");
                return m_dn_sql;
            }
        }

        public static void init(string sServ, string sDb, string sUsr, string sPwd)
        {
            if(m_dn_sql != null)
            {
                m_dn_sql.Dispose();
                m_dn_sql = null;
            }
            m_dn_sql = new denSQL(sServ, sDb, sUsr, sPwd);
        }

        public void Dispose()
        {
            //bOnDispose = true;
            try { if (sqlConn != null && sqlConn.State == ConnectionState.Open) sqlConn.Close(); }
            catch { }
        }

        public bool HasReader { get { return bHasReader; } }

        public static bool hasConnection { get { return m_sql.IsConnected; } }
        public bool IsConnected { get { return (sqlConn != null && sqlConn.State == ConnectionState.Open); } }

        public int LastId { get { return (sqlCmd == null) ? 0 : (int)sqlCmd.LastInsertedId; } }
        public long LastLId { get { return (sqlCmd == null) ? 0L : sqlCmd.LastInsertedId; } }

        public bool BeginTransaction(IsolationLevel DataLockLevel)
        {
            try
            {
                if (!IsConnected) return false;
                if ((sqlTrans = sqlConn.BeginTransaction(DataLockLevel)) == null) return false;
                return true;
            }
            catch (MySqlException e) { ProceedErrorSql(e, "denSQL.BeginTransaction"); return false; }

        }

        public void RollbackTransaction()
        {
            try
            {
                if (sqlTrans != null)
                {
                    sqlTrans.Rollback();
                    sqlTrans.Dispose();
                    sqlTrans = null;
                }
            }
            catch (MySqlException e) { ProceedErrorSql(e, "denSQL.RollbackTransaction"); }
        }

        public void CommitTransaction()
        {
            try
            {
                if (sqlTrans != null)
                {
                    sqlTrans.Commit();
                    sqlTrans.Dispose();
                    sqlTrans = null;
                }
            }
            catch (MySqlException e) { ProceedErrorSql(e, "denSQL.CommitTransaction"); }
        }

        public object ExecuteScalar(string sqlCommand)
        {
            try
            {
                object ret = null;
                if (!IsConnected) return ret;
                if ((sqlCmd = sqlConn.CreateCommand()) == null) return ret;
                sqlCmd.CommandText = sqlCommand;
                ret = sqlCmd.ExecuteScalar();
                return ret;
            }
            catch (MySqlException e) { ProceedErrorSql(e, "denSQL.ExecuteScalar", sqlCommand); return null; }
        }

        public int ExecuteNonQuery(string sqlCommand)
        {
            try
            {
                if (!IsConnected) return 0;
                if ((sqlCmd = sqlConn.CreateCommand()) == null) return 0;
                sqlCmd.CommandText = sqlCommand;
#if DEBUG
                logHandler?.Invoke(string.Format("{0:dd.MM.yyyy HH:mm:ss}: {1}", DateTime.Now, sqlCommand));
#endif
                return sqlCmd.ExecuteNonQuery();
            }
            catch (MySqlException e) { ProceedErrorSql(e, "denSQL.ExecuteNonQuery", sqlCommand); return 0; }
        }

        public denReader ExecuteReader(string sqlCommand)
        {
            denReader rd = new denReader(1, this);
            if (bHasReader) return rd;
            try
            {
                if (!IsConnected) return rd;
                if ((sqlCmd = sqlConn.CreateCommand()) == null) return rd;
                sqlCmd.CommandText = sqlCommand;
                rd.setReader(sqlCmd.ExecuteReader());
                bHasReader = true;
            }
            catch (MySqlException ex)
            {
                ProceedErrorSql(ex, "denSQL.ExecuteReader", sqlCommand);
            }
            return rd;
        }

        public static denTable CreateTable(string sName, string sId = "") { return new denTable(sName, sId, m_sql); }

        public static denReader Query(string sFmt, params object[] oPar) {
            string sql_cmd = oPar != null ? string.Format(Cif_sql, sFmt, oPar) : sFmt;
            return m_sql.ExecuteReader(sql_cmd);
        }
        public static int Command(string sFmt, params object[] oPar) {
            string sql_cmd = oPar != null ? string.Format(Cif_sql, sFmt, oPar) : sFmt;
            return m_sql.ExecuteNonQuery(sql_cmd);
        }
        public static object Scalar(string sFmt, params object[] oPar) {
            string sql_cmd = oPar != null ? string.Format(Cif_sql, sFmt, oPar) : sFmt;
            return m_sql.ExecuteScalar(sql_cmd);
        }

        public static bool hasReader { get { return m_sql.HasReader; } }
        public static int lastId { get { return m_sql.LastId; } }

        public static bool beginTransaction() { return m_sql.BeginTransaction(IsolationLevel.ReadCommitted); }
        public static void commitTransaction() { m_sql.CommitTransaction(); }
        public static void rollbackTransaction() { m_sql.RollbackTransaction(); }

    }
}