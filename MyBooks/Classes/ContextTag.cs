using System;

namespace MyBooks
{
    public class ContextTag : IBkTag
	{
		private SourceGrid.CellContext cntx;
		private IBkObject ibo;
		private Type type;
		private EventHandler OnDone;
		public ContextTag(SourceGrid.CellContext c, IBkObject o, Type t = null, EventHandler ev = null) { cntx = c; ibo = o; type = t; OnDone = ev; }
		public void Apply() { cntx.Value = ibo; OnDone?.Invoke(this, new EventArgs()); }
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

