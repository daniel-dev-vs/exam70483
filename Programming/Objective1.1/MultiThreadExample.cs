using System;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Programming
{
    public static class MultiThreadExample
    {

        [ThreadStatic]
        public static int _field;

        public static ThreadLocal<int> _fieldLocal = new ThreadLocal<int>( () => {
            return Thread.CurrentThread.ManagedThreadId;
        });

        #region thread methods
        private static void ThreadMethod()
        {

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc METHOD 1 :{0}", i);
                Thread.Sleep(1000);
            }
        }

        private static void ThreadMethod2(object o)
        {

            for (int i = 0; i < (int)o; i++)
            {
                Console.WriteLine("ThreadProc METHOD 2 :{0}", i);
                Thread.Sleep(1000);
            }
        }

        private static void ForegroundMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc: {0} ", i);
                Thread.Sleep(1000);
            }
        }

        #endregion

        #region examples

        public static void CodeProjectExample()
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            Thread t2 = new Thread(new ParameterizedThreadStart(ThreadMethod2));

            t.Start();
            t2.Start(10);
        }
        public static void FirstExample()
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.Start();

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Main thread: Do some work.");
                Thread.Sleep(1);
            }

            t.Join();
        }

        public static void ForegroundExample()
        {
            Thread t = new Thread(new ThreadStart(ForegroundMethod));
            t.IsBackground = false;
            t.Start();
        }

        public static void ParameterizedThreadExample()
        {
            Thread t = new Thread(new ParameterizedThreadStart(ThreadMethod2));
            t.Start(5);
            t.Join();
        }

        public static void StopExample()
        {
            bool stopped = false;

            Thread t = new Thread(new ThreadStart(() => {

                while (!stopped)
                {
                    Console.WriteLine("Running....");
                    Thread.Sleep(1000);
                }
            }));

            t.Start();
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();

            stopped = true;

            t.Join();
        }

        public static void ThreadStaticAttribute()
        {
            new Thread(() => {
                for (int i = 0; i < 10; i++)
                {
                    _field++;
                    Console.WriteLine("Thread A: {0}", _field);
                }
            } ).Start();


            new Thread(() => {
                for (int i = 0; i < 10; i++)
                {
                    _field++;
                    Console.WriteLine("Thread B: {0}", _field);
                }
            }).Start();

            Console.ReadKey();

        }

        public static void ThreadLocal()
        {
            new Thread(() => {
                for (int i = 0; i < _fieldLocal.Value; i++)
                {
                    Console.WriteLine("Thread A: {0}", i);
                }
            }).Start();

            new Thread(() => {
                for (int i = 0; i < _fieldLocal.Value; i++)
                {
                    Console.WriteLine("Thread B: {0}", i);
                }
            }).Start();

            Console.ReadKey();
        }

        public static void ThreadPoolInUse()
        {
           ThreadPool.QueueUserWorkItem((s) => {
               Console.WriteLine("Working on a thread from threadpool");
           });

            Console.ReadLine();
        }


        

        #endregion

    }
}
