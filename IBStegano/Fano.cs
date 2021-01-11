using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBStegano
{
    public static class Fano
    {
        static double[] P1 = { 0.062, 0.014, 0.038, 0.013, 0.025, 0.072, 0.072, 0.007, 0.016, 0.062, 0.010, 0.028, 0.035, 0.026, 0.053, 0.090, 0.023, 0.040, 0.045, 0.053, 0.021, 0.002, 0.009, 0.004, 0.012, 0.006, 0.003, 0.014, 0.003, 0.014, 0.003, 0.006, 0.018, 0.08 , 0.004 };
        public static char[] Alpha = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я', ' ', 'Ѓ' };

        public static string[] Res = new string[Alpha.Length];

        static double schet1 = 0;
        static double schet2 = 0;

        public static void Sort()
        {
            for (int i = 0; i < P1.Length; i++)
            {
                for (int j = 0; j < P1.Length - i - 1; j++)
                {
                    if (P1[j] < P1[j + 1])
                    {
                        char temp2;
                        double temp1 = 0;

                        temp1 = P1[j];
                        temp2 = Alpha[j];
                        P1[j] = P1[j + 1];
                        Alpha[j] = Alpha[j + 1];
                        P1[j + 1] = temp1;
                        Alpha[j + 1] = temp2;

                    }
                }
            }

        }

        public static int Divine(int L, int R)
        {
            int m;
            schet1 = 0;
            for (int i = L; i <= R - 1; i++)
            {
                schet1 = schet1 + P1[i];
            }

            schet2 = P1[R];
            m = R;
            while (schet1 >= schet2)
            {
                m = m - 1;
                schet1 = schet1 - P1[m];
                schet2 = schet2 + P1[m];
            }
            return m;

        }

        public static void Create(int L, int R)
        {
            int n;

            if (L < R)
            {

                n = Divine(L, R);
                for (int i = L; i <= R; i++)
                {
                    if (i <= n)
                    {
                        Res[i] += Convert.ToByte(0);
                    }
                    else
                    {
                        Res[i] += Convert.ToByte(1);
                    }
                }

                Create(L, n);

                Create(n + 1, R);

            }

        }
    }
}
