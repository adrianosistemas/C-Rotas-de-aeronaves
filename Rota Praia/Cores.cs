using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace rota_praia1
{
    public static class Cores
    {
        public static string toARGB(Color cor)
        {
            return cor.A + "," + cor.R + "," + cor.G + "," + cor.B;
        }

        public static Color toColor(string cor)
        {
            string [] key = cor.Split(',');

            return Color.FromArgb(int.Parse(key[0]), int.Parse(key[1]), int.Parse(key[2]), int.Parse(key[3]));
        }
    }
}
