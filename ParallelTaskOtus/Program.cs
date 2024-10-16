using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Text;

namespace ParallelTaskOtus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Numbers numbers = new Numbers();
            int[] sizes = {100_000, 1_000_000, 10_000_000};
            foreach(var number  in sizes) 
            { 
                StringBuilder osInfo = Computer.GetParamComputer();
                string memory = Computer.GetPhysicalMemory("Win32_PhysicalMemory");
                string processor = Computer.GerProcessor("Win32_Processor");
                Console.WriteLine(osInfo.ToString());
                Console.WriteLine(processor);
                Console.WriteLine(memory);
                Stopwatch stopwatch = Stopwatch.StartNew();
                long sum = 0;
                for (int i = 0; i < number; i++) 
                {
                    sum += i + 1;
                }

                stopwatch.Stop();
                string WIN32_Class = "Win32_Processor";
                Console.WriteLine($"{sum}, последовательного время выполнения: {stopwatch.ElapsedMilliseconds}");

                //параллельное суммирование с использованием LINQ
                long series = stopwatch.ElapsedMilliseconds;
                stopwatch.Restart();
                long result = Parallel.LinqSumParallel(Enumerable.Range(1, number).ToArray());
                stopwatch.Stop();

                Console.WriteLine($"{result}, LINQ время выполнения: {stopwatch.ElapsedMilliseconds} ms");
                long linq = stopwatch.ElapsedMilliseconds;

                Numbers sumResult1 = new Numbers();
                Numbers sumResult2 = new Numbers();
                numbers.ArrayNumbers = Enumerable.Range(1, number).ToArray();
                int mid = number / 2;

                Thread thread1 = new Thread(() => ThreadParallel.CalculateSum(0, mid, numbers.ArrayNumbers, sumResult1));
                Thread thread2 = new Thread(() => ThreadParallel.CalculateSum(mid, numbers.ArrayNumbers.Length, numbers.ArrayNumbers, sumResult2));

                stopwatch.Restart();

                thread1.Start();
                thread2.Start();

                thread1.Join();
                thread2.Join();

                stopwatch.Stop();

                long totalSum = sumResult1.Sum + sumResult2.Sum;
                Console.WriteLine($"{totalSum}, thread время выполнения: {stopwatch.ElapsedMilliseconds} ms");
                long parralel = stopwatch.ElapsedMilliseconds;

                File.AppendAllText("Otus.txt", $"\n{osInfo}\n{processor}\n{memory}\n{totalSum} thread время выполнения: {series} ms\n{result}, LINQ время выполнения: {linq} ms\n{sum}, последовательного время выполнения: {parralel} ms");
        
            }
        }
    }
}
