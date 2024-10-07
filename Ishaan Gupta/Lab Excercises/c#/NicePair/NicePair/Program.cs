using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicePair
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string s = Console.ReadLine();
            Console.WriteLine(NicePair(s));
        }
        static int NicePair(string s)
        {
            int ctrA = 0, ans = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'a') ctrA++;
                else if (s[i] == 'b') ans += ctrA;
            }
            return ans;
        }
    }
}
