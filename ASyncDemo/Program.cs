using System;
using System.Threading;
using System.Threading.Tasks;

namespace ASyncDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(() =>
            {
                var result = NumberOfPrimesInInterval(2, 200000);

                Console.WriteLine(result + " comes from thread #" 
                    + Thread.CurrentThread.ManagedThreadId);
            });
            thread.Start();

            for (int i = 0; i < 5; i++)
            {
                var task = Task.Run(() => NumberOfPrimesInInterval(2, 200000));
                task.ContinueWith((taskResult) => Console.WriteLine(taskResult.Result
                     + " comes from thread #"
                    + Thread.CurrentThread.ManagedThreadId));
            }

            Console.WriteLine("The main thread is #" + Thread.CurrentThread.ManagedThreadId);

            while (true)
            {
                string line = Console.ReadLine();
                if (line == "exit")
                {
                    return;
                }
                else
                {
                    Console.WriteLine(line);
                }
            }
        }

        public static int NumberOfPrimesInInterval(int min, int max)
        {
            int count = 0;

            for (int i = min; i <= max; i++)
            {
                bool isPrime = true;

                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
