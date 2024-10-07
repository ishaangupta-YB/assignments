using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeSquare
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int T = int.Parse(Console.ReadLine());
            int[] res = new int[T];
            for (int i = 0; i < T; i++)
            {
                int N = int.Parse(Console.ReadLine());
                res[i] = CubesSquares(N);
            }
            foreach (int x in res) Console.WriteLine(x);
        }

        static int CubesSquares(int N)
        {
            HashSet<int> hs = new HashSet<int>();
            for (int i = 1; i * i <= N; i++) hs.Add(i * i);
            for (int i = 1; i * i * i <= N; i++) hs.Add(i * i * i);
            return hs.Count;
        }
    }
}
