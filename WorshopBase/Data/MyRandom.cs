using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorshopBase.Data
{
    public class MyRandom
    {
        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                //Генерируем число являющееся латинским символом в юникоде
                int chCode = Convert.ToInt32(Math.Floor(58 * random.NextDouble() + 65));
                if (chCode > 90 && chCode < 97)
                    chCode += Convert.ToInt32(Math.Floor(15 * random.NextDouble() + 6));

                ch = Convert.ToChar(chCode);
                //Конструируем строку со случайно сгенерированными символами
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
