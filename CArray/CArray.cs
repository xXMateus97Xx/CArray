using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CArray
{
    public ref struct CArray<T> where T : unmanaged
    {
        IntPtr _ptr;
        int _size;

        public CArray(int length, bool initializeValues = false)
        {
            Length = length;
            _size = Marshal.SizeOf<T>();
            var fullSize = _size * length;
            _ptr = Marshal.AllocHGlobal(fullSize);

            if (initializeValues)
                for (int i = 0; i < fullSize; i++)
                    Marshal.WriteByte(_ptr, i, 0);
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
                return *(T*)ptr;
            }
            set
            {
                CheckPosition(index);
                var ptr = GetPosition(index);
                *(T*)ptr = value;
            }
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(_ptr);
            _ptr = IntPtr.Zero;
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
            return (T*)ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CheckPosition(int pos)
        {
            if (pos < 0 || pos + 1 > Length)
                throw new ArgumentOutOfRangeException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IntPtr GetPosition(int index)
        {
            return _ptr + index * _size;
        }
    }
}
