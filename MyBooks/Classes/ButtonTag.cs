using System;
using System.Windows.Forms;

namespace MyBooks
{




	public class ButtonTag : IBkTag
	{
		private Button btn;
		private IBkObject ibo;
		private Type type;
		private EventHandler OnDone;
		public ButtonTag(Button b, IBkObject o, Type t = null, EventHandler ev = null) { btn = b; ibo = o; type = t; OnDone = ev; }
		public void Apply() { btn.Tag = ibo; btn.Text = ibo.I_Short; OnDone?.Invoke(this, new EventArgs()); }
		public void Make()
		{
			if (type == null) return;
			System.Reflection.ConstructorInfo ci = type.GetConstructor(new Type[] { });
			ibo = (IBkObject)ci.Invoke(new object[] { });
			if (!ibo.I_OnNew()) return;
			Apply();
		}
		public void Edit() { if (ibo.I_Edit()) Apply(); }
	}
}

