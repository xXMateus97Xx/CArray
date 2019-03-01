using NUnit.Framework;
using System;
using System.Linq;

namespace CArray.Tests
{
    public class Tests
    {

        [Test]
        public void Should_Test_Empty_Instance()
        {
            var array = new CArray<int>();
            try
            {
                Assert.IsTrue(array.IsEmpty);
                Assert.IsTrue(array.Length == 0);
            }
            finally
            {
                array.Dispose();
            }
        }

        [Test]
        public void Should_Throws_Argument_Out_Of_Range_Exception()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var array2 = new CArray<int>();

                try
                {
                    array2[2] = 10;
                }
                finally
                {
                    array2.Dispose();
                }
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var array2 = new CArray<int>();

                try
                {
                    var x = array2[2];
                }
                finally
                {
                    array2.Dispose();
                }
            });
        }

        [Test]
        public void Should_Test_Set_And_Get_Values()
        {
            var a = new CArray<int>(10);
            try
            {
                a[0] = 20;
                Assert.AreEqual(20, a[0]);

                a[5] = 30;
                Assert.AreEqual(30, a[5]);

                a[9] = 40;
                Assert.AreEqual(40, a[9]);
            }
            finally
            {
                a.Dispose();
            }
        }

        [Test]
        public unsafe void Should_Test_Get_Pointer_And_Set_Value()
        {
            var a = new CArray<int>(10);

            try
            {
                var ptr = a.GetPointer(3);
                *ptr = 20;

                Assert.AreEqual(20, a[3]);
            }
            finally
            {
                a.Dispose();
            }
        }

        [Test]
        public void Should_Initialize_Cleaned()
        {
            var a = new CArray<int>(10, true);

            try
            {
                for (var i = 0; i < a.Length; i++)
                    Assert.AreEqual(0, a[i]);
            }
            finally
            {
                a.Dispose();
            }
        }

        [Test]
        public void Should_Convert_To_And_From_Managed_Array()
        {
            var managedArray = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            var cArray = CArray<int>.FromArray(managedArray);

            try
            {
                var newManagedArray = cArray.ToArray();
                Assert.IsTrue(managedArray.SequenceEqual(newManagedArray));
            }
            finally
            {
                cArray.Dispose();
            }
        }

        [Test]
        public void Should_Test_Iterator()
        {
            var a = new CArray<int>(10);

            try
            {
                for (var i = 0; i < a.Length; i++)
                    a[i] = i;

                var index = 0;
                foreach (var n in a)
                    Assert.AreEqual(n, a[index++]);
            }
            finally
            {
                a.Dispose();
            }
        }

        [Test]
        public void Should_Test_Iterator_With_Byte()
        {
            var a = new CArray<byte>(10);

            try
            {
                for (byte i = 0; i < a.Length; i++)
                    a[i] = i;

                var index = 0;
                foreach (var n in a)
                    Assert.AreEqual(n, a[index++]);
            }
            finally
            {
                a.Dispose();
            }
        }

        [Test]
        public void Should_Test_Empty_Iterator()
        {
            var a = new CArray<int>();

            try
            {
                foreach (var n in a)
                    Assert.Fail();

                Assert.Pass();
            }
            finally
            {
                a.Dispose();
            }
        }
    }
}