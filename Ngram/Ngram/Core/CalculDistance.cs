using System;
using System.Collections.Generic;
using System.Text;

namespace Ngram
{
    internal class CalculDistance
    {
        private static int Ngram(ref string[] qgs, string s, int Q, bool InversionGram)
        {
            int num1;
            if (Q > 0)
            {
                if (Q > 1)
                {
                    char str = '\f';
                    if (InversionGram)
                        str = '\v';

                    s = new string('\v', checked(Q - 1)) + s + new string(str, checked(Q - 1));
                }
                int num2 = checked(s.Length - Q);
                qgs = new string[checked(num2 + 1)];
                int num3 = num2;
                int index = 0;
                while (index <= num3)
                {
                    qgs[index] = s.Substring(checked(index /*+ 1*/), Q);
                    checked { ++index; }
                }
                num1 = checked(num2 + 1);
            }
            else
            {
                qgs = s.Split(' ');
                num1 = checked(qgs.GetUpperBound(1) + 1);
            }
            return num1;
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static float[] IndiceSimilarite(string s1, string s2, int TypeNgram = 1, bool InversionGram = false, int MaxDistance = -1, StringComparison TypeComparaison = StringComparison.CurrentCultureIgnoreCase)
        {
            long num1 = (long)s1.Length;
            long num2 = (long)s2.Length;
            //float[] num3 = { -1f, -1f, -1f, -1f, -1f, -1f, -1f };
            float[] num3 = { 0f, 0f, 0f, 0f, 0f, 0f/*, 0f*/ };

            if (string.IsNullOrWhiteSpace(s1) && string.IsNullOrWhiteSpace(s2))
            {
                num3[0] = 1f;
                num3[1] = 1f;
                num3[2] = 1f;
                num3[3] = 1f;
                num3[4] = 1f;
                num3[5] = 1f;
            }

            if (num1 >= 1L & num2 >= 1L & num1 <= 256L & num2 <= 256L & TypeNgram >= 0 & TypeNgram <= 5)
            {
                if (TypeNgram == 1 & InversionGram)
                    InversionGram = false;
                string[] qgs1 = default(string[]);
                int num4 = CalculDistance.Ngram(ref qgs1, s1, TypeNgram, InversionGram);
                string[] qgs2 = default(string[]);
                int num5 = CalculDistance.Ngram(ref qgs2, s2, TypeNgram, InversionGram);
                int num6 = checked(num5 - 1);
                int num7 = checked(num4 - 1);
                int index1 = 0;
                int num8 = 0;
                while (index1 <= num7)
                {
                    string str = qgs1[index1];
                    string String1 = null;
                    if (InversionGram)
                        String1 = ReverseString(str);
                    int num9 = 0;
                    int num10 = num6;
                    if (MaxDistance > -1)
                    {
                        if (index1 > MaxDistance)
                            num9 = checked(index1 - MaxDistance);
                        if (num10 > checked(index1 + MaxDistance))
                            num10 = checked(index1 + MaxDistance);
                    }
                    int num11 = num9;
                    int num12 = num10;
                    int index2 = num11;
                    while (index2 <= num12)
                    {
                        if (!string.IsNullOrEmpty(qgs2[index2]))
                        {
                            bool flag = String.Compare(str, qgs2[index2], TypeComparaison) == 0;
                            if (!flag & InversionGram)
                                flag = String.Compare(String1, qgs2[index2], TypeComparaison) == 0;
                            if (flag)
                            {
                                checked { ++num8; }
                                qgs2[index2] = (string)null;
                                break;
                            }
                        }
                        checked { ++index2; }
                    }
                    checked { ++index1; }
                }

                num3[0] = (float)num8 / (float)Math.Pow((double)checked((long)num4 * (long)num5), 2.0);
                num3[1] = (float)checked(2 * num8) / (float)checked(num4 + num5);
                num3[2] = (float)num8 / (float)checked(num4 + num5 - num8);
                num3[3] = (float)(0.5 * ((double)num8 / (double)num4 + (double)num8 / (double)num5));
                num3[4] = num4 < num5 ? (float)num8 / (float)num5 : (float)num8 / (float)num4;
                num3[5] = num4 > num5 ? (float)num8 / (float)num5 : (float)num8 / (float)num4;

            }
            return num3;
        }

        public enum eIndiceSimilarite
        {
            eCosinus = 0,
            eDice = 1,
            eJaccard = 2,
            eKulczynski = 3,
            eBraunBlanquet = 4,
            eSimpson = 5,
        }
    }
}
