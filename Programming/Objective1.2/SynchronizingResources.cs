using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming.Objective1._2
{
    public class SynchronizingResources
    {
        /// <summary>
        /// Bad example of accessing shared data in multithreaded.
        /// </summary>
        public static void WrongExample()
        {
            int n = 0;
            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    n++;
                }
            });

            for (int i = 0; i < 1000000; i++)
            {
                n--;
            }

            up.Wait();
            Console.WriteLine(n);
        }
        /// <summary>
        /// Good example of acessing shared data in multithreaded.
        /// </summary>
        public static void RightExample()
        {
            int n = 0;
            object _lock = new object();

            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    lock (_lock)
                    n++;
                }
            });

            for (int i = 0; i < 1000000; i++)
            {
                lock(_lock)
                n--;
            }

            up.Wait();
            Console.WriteLine(n);
        }
    }
}
