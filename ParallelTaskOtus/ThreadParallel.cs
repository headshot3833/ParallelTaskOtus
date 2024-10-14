using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTaskOtus
{
    public static class ThreadParallel
    {
        public static void CalculateSum(int start, int end, int[] array, Numbers result)
        {
            long sum = 0;
            for (int i = start; i < end; i++)
            { 
                result.Sum += array[i];
            }
        }
    }
}
