using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace rota_praia1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //if (DateTime.Now <= DateTime.Parse("21/03/2016"))
                Application.Run(new frmRota());
            //else
            //    MessageBox.Show("Impossível abrir programa\n\nProblema na Leitura dos dados das ACFTs", "Inicializando banco de dados", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
        }
    }
}
