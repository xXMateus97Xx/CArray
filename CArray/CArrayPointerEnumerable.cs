using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CArray
{
    class CArrayPointerEnumerable<T> : IEnumerable<IntPtr> where T :unmanaged
    {
        private IEnumerator<IntPtr> _enumerator;

        public CArrayPointerEnumerable(CArray<T> cArray)
        {
            _enumerator = new CArrayPointerIterator<T>(cArray);
        }

        public IEnumerator<IntPtr> GetEnumerator()
        {
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _enumerator;
        }
    }
}
