using System;
using System.Collections.Generic;
using System.Text;

namespace MaxV.Helper
{
    public static class RandomHelper
    {
        public static string GenerateRandomString(int length = 40)
        {
            Random rd = new Random();

            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789-";
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public static int GenerateRandomInteger(int min, int max)
        {
            Random r = new Random();
            int rInt = r.Next(min, max); //for ints

            return rInt;
        }

        public static double GenerateRandomDouble(int range = 1000)
        {
            Random r = new Random();
            double rDouble = r.NextDouble() * range; //for doubles

            return rDouble;
        }
    }
}
