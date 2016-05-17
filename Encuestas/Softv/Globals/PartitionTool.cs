using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globals
{
    public static class PartitionTool
    {
        public static List<int> PartitionInto(int value, int count)
        {
            if (count <= 0) throw new ArgumentException("count must be greater than zero.", "count");
            var result = new int[count];
            int runningTotal = 0;
            for (int i = 0; i < count; i++)
            {
                var remainder = value - runningTotal;
                var share = remainder > 0 ? remainder / (count - i) : 0;
                result[i] = share;
                runningTotal += share;
            }
            if (runningTotal < value) result[count - 1] += value - runningTotal;
            return result.ToList();
        }
    }
}
