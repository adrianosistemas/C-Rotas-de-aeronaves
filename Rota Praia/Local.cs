using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace rota_praia1
{
    public class Local
    {
        public int Id;
        public Grupo donoGrupo;
        public Acft donoAcft;

        public Label lblNome = new Label();
        public Label lblTipo = new Label();
        public Label lblAltitude = new Label();

        public void Inicializar()
        {
            lblNome.Dispose();
            lblTipo.Dispose();
            lblAltitude.Dispose();
        }

        public string Nome
        {
            get 
            {
                return lblNome.Text;
            }
            set 
            {
                lblNome.Text = value;
            }
        }

        public string Tipo { get; set; }

        public double Altitude
        {
            get;
            set;
        }

        public int X;
        public int Y;
        public int Ordem;

        public void Mover(int px, int py)
        {
            X = px;
            Y = py;

            lblNome.Left = X;
            lblNome.Top = Y;

            if (donoAcft != null)
            {
                if (donoAcft.retas[donoAcft.indiceRetaAtual].Ini == this)
                    donoAcft.Mover(donoAcft.retas[donoAcft.indiceRetaAtual].Ini.X, donoAcft.retas[donoAcft.indiceRetaAtual].Ini.Y);
                else
                    if (donoAcft.retas[donoAcft.indiceRetaAtual].Fim == this)
                    {
                        donoAcft.Mover(donoAcft.retas[donoAcft.indiceRetaAtual].Ini.X, donoAcft.retas[donoAcft.indiceRetaAtual].Ini.Y);
                        donoAcft.Trafegar(this);
                    }
            }
        }

        public double Distancia(Local local)
        {
            return Math.Sqrt(Math.Pow(this.X - local.X, 2) + Math.Pow(this.Y - local.Y, 2));
        }

    }
}