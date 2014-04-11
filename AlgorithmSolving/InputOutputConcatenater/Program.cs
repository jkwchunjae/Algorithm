using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputOutputConcatenater
{
    class Program
    {
        static void Main(string[] args)
        {
            var dirPath = @"C:\Users\jkwchunjae\Downloads\icpc2012-testdata.tar\icpc2012-testdata\stacking";
            var inputs = Directory.GetFiles(dirPath, "*.in").ToList();
            inputs = inputs.OrderBy(e => e).ToList();
            string strInput = "";
            foreach (var filepath in inputs)
            {
                Console.WriteLine(filepath);
                strInput = strInput + Environment.NewLine + File.ReadAllText(filepath);
            }
            File.WriteAllText("input.in", strInput);

            var outputs = Directory.GetFiles(dirPath, "*.ans").ToList();
            outputs = outputs.OrderBy(e => e).ToList();
            string strOutput = "";
            foreach (var filepath in outputs)
            {
                Console.WriteLine(filepath);
                strOutput = strOutput + File.ReadAllText(filepath);
            }
            File.WriteAllText("output.out", strOutput);
        }
    }
}
