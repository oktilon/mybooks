using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
    class T
    {
        public string m_id = "";
        public string m_text = "";

        public T(denSQL.denReader r)
        {
            m_id = r.GetString("key");
            m_text = r.GetString("val");
        }

        public override int GetHashCode() { return m_id.GetHashCode(); }
        public override string ToString() { return m_text; }

        private static Dictionary<string, T> m_Data;

        public static void initLanguage(int langId)
        {
            m_Data = new Dictionary<string, T>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_string WHERE lang={0}", langId);
            while(r.Read()) { m_Data.Add(r.GetString("key"), new T(r)); }
            r.Close();
        }

        public static string Tx(string id)
        {
            if (m_Data == null) return "";
            KeyValuePair<string, T> r = m_Data.FirstOrDefault(x => x.Key == id);
            return r.Value == null ? id : r.Value.m_text;
        }
    }
}
