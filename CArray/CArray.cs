using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CArray
{
    public unsafe ref struct CArray<T> where T : unmanaged
    {
        T* _ptr;

        public CArray(int length, bool initializeValues = false)
        {
            Length = length;
            var size = Marshal.SizeOf<T>();
            var fullSize = size * length;
            _ptr = (T*)Marshal.AllocHGlobal(fullSize);

            if (initializeValues)
            {
                var defaultT = default(T);
                for (int i = 0; i < length; i++)
                    *GetPosition(i) = defaultT;
            }
        }

        public static CArray<T> FromArray(T[] source)
        {
            var length = source.Length;
            var cArray = new CArray<T>(length);

            for (int i = 0; i < length; i++)
                cArray[i] = source[i];

            return cArray;
        }

        public bool IsEmpty => Length == 0;

        public int Length { get; }

        public unsafe T this[int index]
        {
            get
            {
                CheckPosition(index);
                var ptr = GetPosition(index);
                return *ptr;
            }
            set
            {
                CheckPosition(index);
                var ptr = GetPosition(index);
                *ptr = value;
            }
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal((IntPtr)_ptr);
            _ptr = (T*)IntPtr.Zero;
        }

        public T[] ToArray()
        {
            var array = Array.CreateInstance(typeof(T), Length);

            for (int i = 0; i < Length; i++)
                array.SetValue(this[i], i);

            return (T[])array;
        }

        public unsafe T* GetPointer(int index)
        {
            CheckPosition(index);
            var ptr = GetPosition(index);
            return ptr;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CArrayIterator<T>(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CheckPosition(int pos)
        {
            if (pos < 0 || pos + 1 > Length)
                throw new ArgumentOutOfRangeException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe T* GetPosition(int index)
        {
            return _ptr + index;
        }
    }
}
