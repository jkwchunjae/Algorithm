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
            string s = Console.ReadLine();
            string[] ss = s.Split();
            var N = new BigInteger();
            foreach (var digit in ss[0])
            {
                N = N * 10 + int.Parse(digit.ToString());
            }
            Console.WriteLine(N);
        }
    }
}
