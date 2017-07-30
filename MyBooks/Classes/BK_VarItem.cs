using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
	public class BK_VarItem
	{
		public int Id = 0;
		public int VarId = 0;
		public int ItemId = 0;
		public int Count = 0;

		public BK_VarItem(denSQL.denReader r)
		{
			Id = r.GetInt("id");
            VarId = r.GetInt("var_id");
            ItemId = r.GetInt("item_id");
			Count = r.GetInt("item_cnt");
		}

        public static void getVariantItems(BK_Variant v)
        {
            v.Items.Clear();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_variant_item WHERE var_id = {0}", v.Id);
            while (r.Read()) v.Items.Add(new BK_VarItem(r));
            r.Close();
        }
	}
}
