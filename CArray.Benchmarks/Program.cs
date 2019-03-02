using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace CArray.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            //var x = new CArrayVsCsArray();
            //x.WriteAndReadCArray();

            var summary = BenchmarkRunner.Run<CArrayVsCsArray>();
        }
    }

    public class CArrayVsCsArray
    {
        const int length = 10000000;

        //TODO: Separate write and read Benchmarks, how to setup ref structs?

        [Benchmark]
        public void InstanceCArray()
        {
            new CArray<int>(length).Dispose();
        }

        [Benchmark]
        public void InstanceCArrayCleaned()
        {
            new CArray<int>(length, true).Dispose();
        }

        [Benchmark]
        public void InstanceCSArray()
        {
            var x = new int[length];
        }

        [Benchmark]
        public void WriteAndReadCArray()
        {
            var array = new CArray<int>(length);

            for (int i = 0; i < length; i++)
                array[i] = i;

            for (int i = 0; i < length; i++)
            {
                var x = array[i];
                if (x == 9999999)
                    Console.WriteLine("9999999");
            }

            array.Dispose();
        }

        [Benchmark]
        public void WriteAndReadCsArray()
        {
            var array = new int[length];

            for (int i = 0; i < length; i++)
                array[i] = i;

            for (int i = 0; i < length; i++)
            {
                var x = array[i];
                if (x == 9999999)
                    Console.WriteLine("9999999");
            }
        }
    }
}
