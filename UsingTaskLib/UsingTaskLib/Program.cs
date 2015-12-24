using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingTaskLib
{

    class Program
    {
        static void Main(string[] args)
        {
            //var watch = new Stopwatch();
            //watch.Start();
            Console.WriteLine("1. Basic Flow \n2. Adding to task \n3. Task Array Demo\n4. ParrallelForEach\nYour Choice:");
            var choice = Console.ReadLine();
            var ch = 0;
            if (int.TryParse(choice, out ch))
            {
                switch (ch)
                {
                    case 1:
                        BasicFlow();
                        break;
                    case 2:
                        WaitAllDemo();
                        break;
                    case 3:
                        TaskArrayDemo();
                        break;
                    case 4:
                        ParrallelForEach();
                        break;
                    case 5:
                        WaysOfCallingTask();
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            }
            else
                Console.WriteLine("Wrong input");

            //Console.WriteLine("Total Duration : {0}", watch.Elapsed.TotalSeconds);
            Console.ReadLine();
        }

        private static void WaysOfCallingTask()
        {
            Task.Factory.StartNew(() => { Console.WriteLine("Hello Task library!"); });

            var task1 = new Task(new Action(PrintMessage));
            task1.Start();


            var task2 = new Task(delegate { PrintMessage(); });
            task2.Start();

            var task3 = new Task(() => PrintMessage());
            task3.Start();

            var task4 = new Task(() => { PrintMessage(); });
            task4.Start();


        }

        private static void ParrallelForEach()
        {
            var watch1 = new Stopwatch();
            watch1.Start();
            var urls = new[] { "http://www.facebook.com", "http://www.google.com", "http://www.linkedin.com" };
            Parallel.ForEach(urls, item =>
            {
                var wc = new WebClient();
                var watch2 = new Stopwatch();
                watch1.Start();
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadDataAsync(new Uri(item));
                Console.Write("took time {0} for {1}", watch2.Elapsed.TotalSeconds, item);
            });

            Console.Write("Total time for downloading is {0}", watch1.Elapsed.TotalSeconds);
        }
        static void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
        }

        private static void TaskArrayDemo()
        {
            var watch = new Stopwatch();
            var methodFactory = new MethodFactory();
            watch.Start();
            var bookingTask = new[]
							{
								Task.Factory.StartNew(() =>
								{
								    methodFactory.Flight_book(1000);
								}),Task.Factory.StartNew(() =>
								{
                                    methodFactory.Hotel_book(2000);
								}),Task.Factory.StartNew(() =>
								{
                                    methodFactory.Car_book(3000);
								})
							};
            Task.WaitAll(bookingTask);
            Console.WriteLine("Time taken to TaskArray Demo {0}", watch.Elapsed.TotalSeconds);
        }

        private static void WaitAllDemo()
        {
            var watch = new Stopwatch();
            watch.Start();
            var methodFactory = new MethodFactory();
            var tasks = new List<Task>();
            var cancelToken = new CancellationTokenSource();

            var methodA = Task.Factory.StartNew(() =>
            {
                methodFactory.Flight_book(1000);
            }, cancelToken.Token);
            var methodB = Task.Factory.StartNew(() =>
            {
                methodFactory.Hotel_book(2000);
            }, cancelToken.Token);
            var methodC = Task.Factory.StartNew(() =>
            {
                methodFactory.Car_book(3000);
            }, cancelToken.Token);

            tasks.Add(methodA);
            tasks.Add(methodB);
            tasks.Add(methodC);
            Task.WaitAll(tasks.ToArray());
            cancelToken.Cancel();
            Console.WriteLine("Time taken to AddingToTask Demo {0}", watch.Elapsed.TotalSeconds);
        }

        private static void BasicFlow()
        {
            var watch = new Stopwatch();
            watch.Start();

            var methodFactory = new MethodFactory();
            methodFactory.Flight_book(1000);
            methodFactory.Hotel_book(2000);
            methodFactory.Car_book(3000);

            //var oneTask = new Task(ProcessDataAsync);
            //oneTask.Start();
            //oneTask.Wait();
            Console.WriteLine("Time taken to BasicFlow Demo {0}", watch.Elapsed.TotalSeconds);
        }


        #region Ways of Calling Task

        public static async Task DoWork1()
        {
            await Task.Run(() => PrintMessage());
        }

        public static async Task DoWork2()
        {
            var res = await Task.FromResult<int>(GetSum(4, 5));
        }

        private static int GetSum(int a, int b)
        {
            return a + b;
        }

        private static void PrintMessage()
        {

            Console.WriteLine("Hello Task library!");
        }
        static void Function1() { Console.WriteLine("F1()"); }
        static void Function2() { Console.WriteLine("F2()"); }
        static void Function3() { Console.WriteLine("F3()"); }

        static void TestInvoke()
        {
            Parallel.Invoke(Function1, Function2, Function3);

            var myList = new List<int>();
            myList.AsParallel().ForAll(i => { /*DO SOMETHING*/ });
            Parallel.ForEach(myList, i => { /*DO SOMETHING*/ });
        }

        static void TestFor()
        {
            Parallel.For(0, 100, i => Console.WriteLine(i));

            for (int i = 0; i < 100; i++)
                Console.WriteLine(i);
        }

        static void TestForState()
        {
            Parallel.For(0, 100, (i, state) =>
            {
                Console.WriteLine(i); if (i == 50) state.Break();
            });
        }


        #endregion
    }
}
