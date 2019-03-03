using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CArray
{
    class CArrayEnumerable<T> : IEnumerable<T> where T :unmanaged
    {
        private IEnumerator<T> _enumerator;

        public CArrayEnumerable(CArray<T> cArray)
        {
            _enumerator = new CArrayIterator<T>(cArray);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _enumerator;
        }
    }
}
