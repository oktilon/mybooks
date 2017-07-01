using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MyBooks
{
	public class BO_Field
	{
		public string Name = "";
		public object Value = null;
		public int Flags = 0;
		public int Limit = 0;
		public HorizontalAlignment DataAlign = HorizontalAlignment.Left;
		public object Param = null;

		/// <summary>
		/// Для временного хранения значения поля в редакторе
		/// </summary>
		public int TmpFlagValue = 0;
		/// <summary>
		/// Новое поле (Имя, Значение, Флаги(1-r/o, 2-флаги), Макс.длина, Выравнивание, Доп.парам)
		/// </summary>
		/// <param name="s">Имя поля</param>
		/// <param name="o">Значение поля</param>
		/// <param name="pFlags">Флаги для поля (1-только для чтения, 2-поле флаговое)</param>
		/// <param name="pLimit">Максимальная длина текста</param>
		/// <param name="da">Выравнивание текста в ячейке</param>
		/// <param name="pPar">Дополнительный параметр (2-класс списка флагов)</param>
		public BO_Field(string s, object o, int pFlags = 0, int pLimit = 0, HorizontalAlignment da = HorizontalAlignment.Left, object pPar = null)
		{
			Name = s;
			Value = o;
			Flags = pFlags;
			Limit = pLimit;
			DataAlign = da;
			Param = pPar;
			if (FlagField) TmpFlagValue = (int)Value;
		}
		public bool ReadOnly { get { return Flags.HasBit(1); } set { Flags = Flags.ApplyBit(1, value); } }
		public bool FlagField { get { return Flags.HasBit(2); } set { Flags = Flags.ApplyBit(2, value); } }

		public List<BO_Flag> FlagList { get { return Param is List<BO_Flag> ? (List<BO_Flag>)Param : new List<BO_Flag>(); } }

		public override string ToString() { return Name; }
		public override int GetHashCode() { return Name.GetHashCode(); }
	}
}
