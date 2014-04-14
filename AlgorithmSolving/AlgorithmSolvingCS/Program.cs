using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AlgorithmSolvingCS
{
    class Program
    {
        static void Main(string[] args)
        {
            var ans = new BigInteger[1010];
            ans[1] = 0;
            for (var i = 2; i <= 1000; i++)
                ans[i] = ans[i - 1] * 2 + (1 - (i % 2) * 2);

            string s = Console.ReadLine();
            string[] ss = s.Split();
            int N = int.Parse(ss[0]);

            Console.WriteLine(ans[N]);
        }
    }
}
