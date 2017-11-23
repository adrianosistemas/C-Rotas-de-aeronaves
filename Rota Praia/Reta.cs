using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rota_praia1
{
    public class Reta
    {
        public Local Ini;
        public Local Fim;

        public int a, b, c;          //variáveis não aparecem automaticamente no grid

        public Reta()
        {
        }

        public void Inicializar()
        {
            Ini.Inicializar();
            Fim.Inicializar();
        }

        /*
        public Reta(int pxIni, int pyIni)
        {
            Ini.X = pxIni;
            Ini.Y = pyIni;
        }
        */

        public Reta(Local pIni, Local pFim)
        {
            Ini = pIni;
            Fim = pFim;
        }

        /*
        public Reta(int pxIni, int pyIni, int pxFin, int pyFin)
        {
            Ini.X = pxIni;
            Ini.Y = pyIni;
            Fim.X = pxFin;
            Fim.Y = pyFin;
        }
        */
    }
}