using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Programming
{
    public static class UsingConcurrent
    {
        public static void UsingConcurrentBag()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();

            bag.Add(42);
            bag.Add(21);
            bag.Add(31);
            bag.Add(51);

            int result;

            if (bag.TryTake(out result))
                Console.WriteLine(result);

            if (bag.TryPeek(out result))
                Console.WriteLine("There is a next item: {0}", result);
        }

        public static void UsingConcurrentBagEnumerate()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();

            Task.Run(() =>
            {
                bag.Add(42);
                Thread.Sleep(1000);
                bag.Add(21);
            });

            Task.Run(() =>
            {
                foreach (int i in bag)
                    Console.WriteLine(i);
            }).Wait();
        }

        public static void UsingConcurrentStack()
        {
            ConcurrentStack<int> stack = new ConcurrentStack<int>();
            stack.Push(42);

            int result;
            if(stack.TryPop(out result))
                Console.WriteLine("Popped: {0}", result);

            stack.PushRange(new int[] { 1, 2, 3});

            int[] values = new int[2];
            stack.TryPopRange(values);

            foreach(int i in values)
                Console.WriteLine(i);
        }

        public static void UsingConcurrentQueue()
        {
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>();

            queue.Enqueue(42);

            int result;
            if(queue.TryDequeue(out result))
                Console.WriteLine("Dequeue: {0}", result);

        }

        public static void UsingConcurrentDictionary()
        {
            var dict = new ConcurrentDictionary<string, int>();

            if (dict.TryAdd("k1", 42))
            {
                Console.WriteLine("Added");
            }

            if (dict.TryUpdate("k1", 21, 42))
            {
                Console.WriteLine("42 update to 21");
            }

            dict["k1"] = 42;

            int r1 = dict.AddOrUpdate("k1", 3, (s,i) => i * 2);
            int r2 = dict.GetOrAdd("k2", 3);

        }
    }
}
