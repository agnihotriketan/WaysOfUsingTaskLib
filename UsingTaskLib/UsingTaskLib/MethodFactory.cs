using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingTaskLib
{
    class MethodFactory
    {

        public int Flight_book(int duration)
        {
            Console.WriteLine("--------- Flight_book --------- ");
            Thread.Sleep(duration);
            Console.WriteLine("Flight_book took {0} sec ", duration);
            return duration;
        }

        public int Hotel_book(int duration)
        {
            Console.WriteLine("--------- Hotel_book ---------");
            Thread.Sleep(duration);
            Console.WriteLine("Hotel_book took {0} sec", duration);
            return duration;
        }

        public int Car_book(int duration)
        {
            Console.WriteLine("--------- Car_book ---------");
            Thread.Sleep(duration);
            Console.WriteLine("Car_book took {0} sec", duration);
            return duration;
        }


        async void ProcessDataAsync()
        {
            var task = HandleFileAsync("d:\\enable1.txt");
            var x = await task;
            Console.WriteLine("Word Count: " + x);
        }

        async Task<int> HandleFileAsync(string file)
        {
            Console.WriteLine("Reading File - Watch Started");
            var count = 0;
            var watch = new Stopwatch();
            watch.Start();
            using (var reader = new StreamReader(file))
            {
                var v = await reader.ReadToEndAsync();
                count += v.Length;
                for (var i = 0; i < 1000; i++)
                {
                    var x = v.GetHashCode();
                    if (x == 0)
                    {
                        count--;
                    }
                }
            }
            Console.WriteLine("Read File Done in {0}", watch.Elapsed.TotalSeconds);
            return count;
        }
    }
}
