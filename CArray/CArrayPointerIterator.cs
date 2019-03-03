using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CArray
{
    internal unsafe class CArrayPointerIterator<T> : IEnumerator<IntPtr> where T : unmanaged
    {
        readonly T* _ptr;
        readonly int _length;
        int _index;

        public CArrayPointerIterator(CArray<T> cArray)
        {
            _length = cArray.Length;
            _index = -1;
            if (!cArray.IsEmpty)
                _ptr = cArray.GetPointer(0);
        }

        public unsafe IntPtr Current => CurrentItem();

        object IEnumerator.Current => CurrentItem();

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (++_index >= _length)
                return false;

            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IntPtr CurrentItem()
        {
            var ptr = _ptr + _index;
            return (IntPtr)ptr;
        }
    }
}
