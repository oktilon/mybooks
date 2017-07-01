using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
	public class BO_Flag
	{
		public int Bit = 0;
		public string Name = "<нет>";
		public string Short = "-";
		public BO_Flag(int b, string n, string s) { Bit = b; Name = n; Short = s; }
		
		public override string ToString() { return Name; }
		public override int GetHashCode() { return Name.GetHashCode(); }
		
		public static string GetCaption(BO_Field f)
		{
            List<string> sCap = new List<string>();
			foreach (BO_Flag x in f.FlagList) if (((int)f.TmpFlagValue).HasBit(x.Bit)) sCap.Add(x.Short);
            return sCap.Count > 0 ? String.Join(",", sCap.ToArray()) : "-";
		}

	}
}
