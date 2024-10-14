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
            numbers.ArrayNumbers = new int[10_000_000];
            StringBuilder osInfo = Computer.GetParamComputer();
            string memory =  Computer.GetPhysicalMemory("Win32_PhysicalMemory");
            string processor = Computer.GerProcessor("Win32_Processor");
            Console.WriteLine(osInfo.ToString());
            Console.WriteLine(processor);
            Console.WriteLine(memory);
            for (int i = 0; i < numbers.ArrayNumbers.Length; i++)
            {
                numbers.ArrayNumbers[i] = i + 1;
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            long sum = 0;
            foreach (var number in numbers.ArrayNumbers)
            {
                sum += number;
            }

            stopwatch.Stop();
            string WIN32_Class = "Win32_Processor";
            Console.WriteLine($"{sum}, последовательного время выполнения: {stopwatch.ElapsedMilliseconds}");
            stopwatch.Restart();

            long result = Parallel.LinqSumParallel(numbers.ArrayNumbers);

            stopwatch.Stop();

            Console.WriteLine($"{result}, LINQ время выполнения: {stopwatch.ElapsedMilliseconds} ms");

            Numbers sumResult1 = new Numbers();

            Numbers sumResult2 = new Numbers();

            int mid = numbers.ArrayNumbers.Length / 2;

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
        }
    }
}
