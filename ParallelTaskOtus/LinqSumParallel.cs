using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTaskOtus
{
    public static class Parallel
    {
        public static long LinqSumParallel(int[] array)
        {
            return array.AsParallel().Sum(x => (long)x); 
        }

    }
}
