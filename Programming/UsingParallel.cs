using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Programming
{
    public static class UsingParallel
    {
        public static void UsingForAndForeach()
        {
            Parallel.For(0, 10, i =>
            {
                Console.WriteLine("running for");
                Thread.Sleep(1000);
            });

            var numbers = Enumerable.Range(0, 10);
            Parallel.ForEach(numbers, i =>
            {
                Console.WriteLine("running foreach");
                Thread.Sleep(1000);
            });

        }

        public static void UsingBreakFor()
        {
            ParallelLoopResult resultLoop = Parallel.For(0, 1000, (int i, ParallelLoopState loopState) =>
            {
                Console.WriteLine("working loop");
                if (i == 500)
                {
                    Console.WriteLine("Breaking loop ------------------------------------");
                    loopState.Break();

                }

                return;

            });
        }

        public static void UsingAsyncAwait()
        {
            string result = DownloadContent().Result;
            Console.WriteLine(result);
        }

        public static async Task<string> DownloadContent()
        {
            using (HttpClient client = new HttpClient())
            {
                string result = await client.GetStringAsync("http://www.microsoft.com");
                return result;
            }

        }

        public static Task SleepAsyncA(int milliSecondsTimeout)
        {
            return Task.Run(() => Thread.Sleep(milliSecondsTimeout));
        }

        public static Task SleepAsyncB(int milliSecondsTimeout)
        {
            TaskCompletionSource<bool> tcs = null;
            var t = new Timer(delegate { tcs.TrySetResult(true); }, null, -1, -1);
            tcs = new TaskCompletionSource<bool>(t);

            t.Change(milliSecondsTimeout, -1);
            return tcs.Task;
        }

        private static async void Button_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            string content = await httpClient
            .GetStringAsync("http://www.microsoft.com")
.ConfigureAwait(false);

            using (FileStream sourceStream = new FileStream("temp.html",
                FileMode.Create, FileAccess.Write, FileShare.None,
                4096, useAsync: true))
            {
                byte[] encodedText = Encoding.Unicode.GetBytes(content);
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length)
                .ConfigureAwait(false);
            };
        }
    }
}
