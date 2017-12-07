using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming
{
    public static class UsingBlockingCollection
    {
        public static void BlockingCollectionExample()
        {
            BlockingCollection<string> col = new BlockingCollection<string>();

            Task read = Task.Run(() => {
                while (true)
                {
                    Console.WriteLine(col.Take());
                }
            });

            Task write = Task.Run(()=> {
                while (true)
                {
                    string s = Console.ReadLine();
                    if (string.IsNullOrEmpty(s)) break;

                    col.Add(s);
                }

            });

            write.Wait();
        }

        public static void BlockingCollectionExample2()
        {
            BlockingCollection<string> col = new BlockingCollection<string>();

            Task read = Task.Run(() => {
                foreach (var i in col.GetConsumingEnumerable())
                    Console.WriteLine(i);
                    
            });
        }
    }
}
