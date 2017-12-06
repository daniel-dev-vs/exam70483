using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Programming
{
    public static class ParallelLINQ
    {

        public static void UsingUnorderedParallel()
        {
            var numbers = Enumerable.Range(0, 10);
            var parallelResult = numbers.AsParallel().Where(i => i % 2 == 0).ToArray();

            foreach (var i in parallelResult)
            {
                Console.WriteLine(i);
            }
        }

        public static void UsingOrderedParallel()
        {
            var numbers = Enumerable.Range(0, 10);
            var parallelResult = numbers.AsParallel().AsOrdered().Where(i => i % 2 == 0).ToArray();

            foreach (var i in parallelResult)
            {
                Console.WriteLine(i);
            }
        }

        public static void UsingOrderedAsSequentialParallel()
        {
            var numbers = Enumerable.Range(0, 10);
            var parallelResult = numbers.AsParallel().AsOrdered().Where(i => i % 2 == 0).AsSequential().ToArray();

            foreach (var i in parallelResult.Take(5))
            {
                Console.WriteLine(i);
            }
        }

        public static void UsingForAll()
        {
            var numbers = Enumerable.Range(0, 10);

            var parallelResult = numbers.AsParallel().Where(i =>  i % 2 == 0);

            parallelResult.ForAll(e => Console.WriteLine(e));
        }

        public static void UsingForAllTreatment()
        {
            var numbers = Enumerable.Range(0, 20);

            try
            {
                var parallelResult = numbers.AsParallel().Where(i => IsEven(i));
                parallelResult.ForAll(e => Console.WriteLine(e));
            }
            catch (AggregateException e)
            {
                Console.WriteLine("There were  {0} exceptions", e.InnerExceptions.Count);
            }           

       
        }

        public static bool IsEven(int i)
        {
            if (i % 10 == 0) throw new ArgumentException();

            return i % 2 == 0;

        }
    }
}
