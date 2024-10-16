using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTaskOtus
{
    public static class Parallel
    {
        public static long LinqSumParallel(int[] number)
        {
            return number.AsParallel().WithDegreeOfParallelism(2).Sum(x => (long)x); 
        }

    }
}
