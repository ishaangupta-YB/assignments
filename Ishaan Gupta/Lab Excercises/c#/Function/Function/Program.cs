using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int A = int.Parse(Console.ReadLine());
            int B = int.Parse(Console.ReadLine());
            int K = int.Parse(Console.ReadLine());
            int P = int.Parse(Console.ReadLine());
            Console.WriteLine(functionEq(A, B, K, P));
        }

        static int functionEq(int A, int B, int K, int P)
        {
            int fK = Fn(A, B, K);
            int fP = Fn(A, B, P);
            return fK - fP;
        }
        static int Fn(int A, int B, int N)
        {
            int res = 0;
            for (int i = 0; i < N; i++) res += A + (i * B);
            return res;
        }
    }
}
