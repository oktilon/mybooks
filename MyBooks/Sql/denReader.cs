using System;
using MySql.Data.MySqlClient;

namespace MyBooks
{
    public partial class denSQL
    {
        public class denReader
        {
            private MySqlDataReader m_rdr;
            private int iNum;
            private denSQL ds;

            public denReader(int i, denSQL s)
            {
                m_rdr = null;
                iNum = i;
                ds = s;
            }
            public void setReader(MySqlDataReader rd)
            {
                m_rdr = rd;
            }

            public int Num { get { return iNum; } }
            public denSQL sql { get { return ds; } }

            public void Close()
            {
                if (Yes && !m_rdr.IsClosed)
                    m_rdr.Close();
                m_rdr = null;
                sql.bHasReader = false;
            }

            private bool Yes { get { return (m_rdr != null); } }
            public bool Good(int i) { return Yes ? !m_rdr.IsDBNull(i) : false; }
            public bool Good(string f) { return Good(m_rdr.GetOrdinal(f)); }

            public bool IsNull(int i) { return Yes ? m_rdr.IsDBNull(i) : true; }
            public bool IsNull(string f) { return Yes ? IsNull(m_rdr.GetOrdinal(f)) : true; }

            public bool HasRows { get { try { return Yes ? m_rdr.HasRows : false; } catch (MySqlException e) { OnErr(e, "HasRows"); return false; } } }

            public string GetString(int i, string Def) { try { return Good(i) ? m_rdr.GetString(i) : Def; } catch (MySqlException e) { OnErr(e, "GetString"); return Def; } }
            public string GetString(int i) { return GetString(i, ""); }
            public string GetString(string f, string Def) { try { return Good(f) ? m_rdr.GetString(f) : Def; } catch (MySqlException e) { OnErr(e, "GetString"); return Def; } }
            public string GetString(string f) { return GetString(f, ""); }

            public int GetInt(int i, int Def) { try { return Good(i) ? m_rdr.GetInt32(i) : Def; } catch (MySqlException e) { OnErr(e, "GetInt"); return Def; } }
            public int GetInt(int i) { return GetInt(i, 0); }
            public int GetInt(string f, int Def) { try { return Good(f) ? m_rdr.GetInt32(f) : Def; } catch (MySqlException e) { OnErr(e, "GetInt"); return Def; } }
            public int GetInt(string f) { return GetInt(f, 0); }

            public long GetLong(int i, long Def) { try { return Good(i) ? m_rdr.GetInt64(i) : Def; } catch (MySqlException e) { OnErr(e, "GetLong"); return Def; } }
            public long GetLong(int i) { return GetInt(i, 0); }
            public long GetLong(string f, long Def) { try { return Good(f) ? m_rdr.GetInt64(f) : Def; } catch (MySqlException e) { OnErr(e, "GetLong"); return Def; } }
            public long GetLong(string f) { return GetInt(f, 0); }

            public uint GetUInt(int i, uint Def) { try { return Good(i) ? m_rdr.GetUInt32(i) : Def; } catch (MySqlException e) { OnErr(e, "GetUInt"); return Def; } }
            public uint GetUInt(int i) { return GetUInt(i, 0); }
            public uint GetUInt(string f, uint Def) { try { return Good(f) ? m_rdr.GetUInt32(f) : Def; } catch (MySqlException e) { OnErr(e, "GetUInt"); return Def; } }
            public uint GetUInt(string f) { return GetUInt(f, 0); }

            public bool GetBool(int i, bool Def) { try { return Good(i) ? m_rdr.GetBoolean(i) : Def; } catch (MySqlException e) { OnErr(e, "GetBool"); return Def; } }
            public bool GetBool(int i) { return GetBool(i, false); }
            public bool GetBool(string f, bool Def) { try { return Good(f) ? m_rdr.GetBoolean(f) : Def; } catch (MySqlException e) { OnErr(e, "GetBool"); return Def; } }
            public bool GetBool(string f) { return GetBool(f, false); }

            public double GetDoubleVal(int i, double Def) { try { return Good(i) ? m_rdr.GetDouble(i) : Def; } catch (MySqlException e) { OnErr(e, "GetDoubleVal"); return Def; } }
            public double GetDoubleVal(int i) { return GetDoubleVal(i, 0d); }
            public double GetDoubleVal(string f, double Def) { try { return Good(f) ? m_rdr.GetDouble(f) : Def; } catch (MySqlException e) { OnErr(e, "GetDoubleVal"); return Def; } }
            public double GetDoubleVal(string f) { return GetDoubleVal(f, 0d); }

            public DateTime GetDateTime(int i, DateTime Def) { try { return Good(i) ? m_rdr.GetDateTime(i) : Def; } catch (MySqlException e) { OnErr(e, "GetDateTime"); return Def; } }
            public DateTime GetDateTime(int i) { return GetDateTime(i, DateNone); }
            public DateTime GetDateTime(string f, DateTime Def) { try { return Good(f) ? m_rdr.GetDateTime(f) : Def; } catch (MySqlException e) { OnErr(e, "GetDateTime"); return Def; } }
            public DateTime GetDateTime(string f) { return GetDateTime(f, DateNone); }

            public object GetValue(int i, object Def) { try { return Good(i) ? m_rdr.GetValue(i) : Def; } catch (MySqlException e) { OnErr(e, "GetValue"); return Def; } }
            public object GetValue(int i) { return GetValue(i, null); }
            public object GetValue(string f, object Def) { try { return Good(f) ? m_rdr.GetValue(m_rdr.GetOrdinal(f)) : Def; } catch (MySqlException e) { OnErr(e, "GetValue"); return Def; } }
            public object GetValue(string f) { return GetValue(f, null); }

            public float GetFloat(int i, float Def) { try { return Good(i) ? m_rdr.GetFloat(i) : Def; } catch (MySqlException e) { OnErr(e, "GetFloat"); return Def; } }
            public float GetFloat(int i) { return GetFloat(i, 0f); }
            public float GetFloat(string f, float Def) { try { return Good(f) ? m_rdr.GetFloat(f) : Def; } catch (MySqlException e) { OnErr(e, "GetFloat"); return Def; } }
            public float GetFloat(string f) { return GetFloat(f, 0f); }

            public decimal GetDecimal(int i, decimal Def) { try { return Good(i) ? m_rdr.GetDecimal(i) : Def; } catch (MySqlException e) { OnErr(e, "GetDecimal"); return Def; } }
            public decimal GetDecimal(int i) { return GetDecimal(i, 0m); }
            public decimal GetDecimal(string f, decimal Def) { try { return Good(f) ? m_rdr.GetDecimal(f) : Def; } catch (MySqlException e) { OnErr(e, "GetDecimal"); return Def; } }
            public decimal GetDecimal(string f) { return GetDecimal(f, 0m); }

            public int GetValues(object[] obj) { try { return m_rdr.GetValues(obj); } catch (MySqlException e) { OnErr(e, "GetValues"); return 0; } }

            public bool Read() { try { return Yes ? m_rdr.Read() : false; } catch (MySqlException e) { OnErr(e, "Read"); return false; } }

            public void OnErr(MySqlException e, string sPart) { ProceedErrorSql(e, "denReader." + sPart, ds.sqlCmd.CommandText); }
        }
    }
}