using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
    public class PageType
    {
        public int Id = 0;
        public int Cols = 0;
        public string[] Names;
        public int[] Width;
        public int ixCar = 0;
        public int ixDev = 0;
        public int ixPrc = 0;
        public int ixCnt = 0;
        public int ixUnit = 0;
        public int ixRest = 0;
        public int ixNote = 0;

        public static PageType Default = new PageType();

        private static List<PageType> cache;

        public PageType() { }
        public PageType(denSQL.denReader r)
        {
            string tmp = "";
            string[] arr;
            Id = r.GetInt("t_id");
            Cols = r.GetInt("t_col_cnt");
            tmp = r.GetString("t_names");
            Names = tmp.Split('|');
            tmp = r.GetString("t_width");
            arr = tmp.Split(',');
            Width = arr.Select(x => Convert.ToInt32(x)).ToArray();
            ixCar = r.GetInt("t_carrier");
            ixDev = r.GetInt("t_device");
            ixPrc = r.GetInt("t_price");
            ixCnt = r.GetInt("t_count");
            ixUnit = r.GetInt("t_unit");
            ixRest = r.GetInt("t_rest");
            ixNote = r.GetInt("t_note");
        }

        public static void initPageTypes()
        {
            cache = new List<PageType>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_tab_type");
            while (r.Read()) cache.Add(new PageType(r));
            r.Close();
        }

        public static PageType getPageType(int id)
        {
            if (cache == null) initPageTypes();
            PageType pt = cache.FirstOrDefault(x => x.Id == id);
            return pt == null ? Default : pt;
        }
        public static PageType getPageType(denSQL.denReader rd, string sField = "t_type")
        {
            return getPageType(rd.GetInt(sField));
        }

    }
}
