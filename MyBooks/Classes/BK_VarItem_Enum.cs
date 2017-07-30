using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
    class BK_VarItem_Enum : IEnumerator
    {
        public BK_VarItem[] _items;
        int position = -1;

        public BK_VarItem_Enum(List<BK_VarItem> lst)
        {
            _items = lst.ToArray();
        }
        object IEnumerator.Current => throw new NotImplementedException();

        public BK_VarItem Current
        {
            get
            {
                try
                {
                    return _items[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        bool IEnumerator.MoveNext()
        {
            position++;
            return (position < _items.Length);
        }

        void IEnumerator.Reset()
        {
            position = -1;
        }
    }
}
