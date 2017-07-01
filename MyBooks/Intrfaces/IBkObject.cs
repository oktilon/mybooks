using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
	public interface IBkObject
	{
		int I_Id { get; }
		string I_Name { get; }
		string I_Short { get; }
		System.Drawing.Image I_Icon { get; }
		bool I_OnNew();
		bool I_Edit();
		bool I_Store();
		List<BO_Field> I_Params();
		void I_SetParam(int idx, object val);
	}
}
