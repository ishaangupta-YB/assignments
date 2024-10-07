using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureArray
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int Q = int.Parse(Console.ReadLine());
            for (int i = 0; i < Q; i++)
            {
                int N = int.Parse(Console.ReadLine());
                int[] array = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                PureArray(N, array);
            }
        }

        static void PureArray(int N, int[] A)
        {
            int ans = 0, left = 0, right = N - 1;
            bool isPossible = true;

            while (left <= right)
            {
                if ((A[left] == 0 && A[right] == 0) || (A[left] == 0 || A[right] == 0) || (left == right && A[left] == 0)) ans++;
                else if (A[left] != A[right])
                {
                    isPossible = false;
                    break;
                }
                left++;
                right--;
            }

            if (isPossible)
            {
                Console.WriteLine("YES");
                Console.WriteLine(ans);
            }
            else Console.WriteLine("NO");
        }
    }
}
