using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace rota_praia1
{
    public class BO_ACFTs
    {
        public PictureBox imgCarta = null;
        public List<Acft> ACFTs = new List<Acft>();

        public int indiceSelecionado = -1;
        
        //int proximoID = 1;
        int indiceSelecionadoOld = -2;
        int ajusteX, ajusteY;

        //

        public void Inicializar()
        {
            ACFTs.ForEach(o => o.Inicializar());
            ACFTs.ForEach(o => o.locais.Clear());
            ACFTs.Clear();
            indiceSelecionado = -1;
            indiceSelecionadoOld = -2;
        }

        public Acft IncluirACFT(PictureBox imagem, Acft acft, Local local, float tamFonte)
        {
            imgCarta = imagem;

            acft.listaACFTs_parent = this;

            //acft.Id = proximoID++;

            if (local == null)
            {
                local = new Local();
                local.X = acft.X;
                local.Y = acft.Y;
            }
            else
            {
                acft.X = local.X;
                acft.Y = local.Y;
            }

            local.donoGrupo = acft.donoGrupo;
            local.donoAcft = acft;

            /////////////////////////////////////////////////////////
            
            acft.lblMatricula.Parent = imagem;
            acft.lblMatricula.AutoSize = true;
            acft.lblMatricula.BorderStyle = BorderStyle.FixedSingle;

            acft.lblMatricula.Font = new Font("Tahoma", tamFonte, FontStyle.Bold);

            acft.lblMatricula.Click += new EventHandler(this.lbl_Click);
            
            /////////////////////////////////////////////////////////
            
            acft.lblAltitude.Parent = imagem;
            acft.lblAltitude.AutoSize = true;
            acft.lblAltitude.BorderStyle = BorderStyle.None;

            acft.lblAltitude.Text = acft.Altitude.ToString();
            acft.AtualizarDadosAltitude();

            /////////////////////////////////////////////////////////

            if (!acft.locais.Contains(local))
                acft.locais.Add(local);
            acft.retas.Add(new Reta(local, local));
            acft.indiceRetaAtual = 0;

            ACFTs.Add(acft);

            this.lbl_Click(acft.lblMatricula, new EventArgs());

            return acft;
        }

        public void AlterarTamanhoFonte(float tamFonte)
        {
            foreach (var obj in ACFTs)
            {
                obj.lblMatricula.Font = new Font("Tahoma", tamFonte, FontStyle.Bold);
                obj.AtualizarDadosAltitude();
            }
        }

        public void AlterarDistanciaEntreACFTs(int distanciaLateral, int distanciaVertical)
        {
            foreach (var obj in ACFTs)
            {
                obj.distanciaLateralMin = distanciaLateral;
                obj.distanciaVerticalMin = distanciaVertical;
            }
        }

        public void HabilitarPiscar(bool piscar)
        {
            foreach (var obj in ACFTs)
                obj.piscarFundo = piscar;
        }

        private void lbl_Click(object sender, EventArgs e)
        {
            foreach (var acft in ACFTs)
            {
                acft.lblMatricula.BackColor = Color.White;
                acft.selecionado = false;
            }
            int i = this.procurarAcrt(sender as Label);
            this.selecionarAcrt(i);
        }

        private int procurarAcrt(Label lbl)
        {
            int i = 0, indice = -1;
            foreach (var acft in ACFTs)
            {
                if (acft.lblMatricula == lbl)
                    indice = i;
                i++;
            }
            return indice;
        }

        private void selecionarAcrt(int i)
        {
            if (i != indiceSelecionado)
            {
                indiceSelecionadoOld = indiceSelecionado;
                indiceSelecionado = i;
                //objetos[i].lblMatricula.MouseMove -= new MouseEventHandler(this.label_MouseMove);
                //objetos[i].lblMatricula.MouseDown -= new MouseEventHandler(this.label_MouseDown);
                //objetos[i].lblMatricula.MouseUp -= new MouseEventHandler(this.label_MouseUp);

                //objetos[i].lblMatricula.MouseMove += new MouseEventHandler(this.label_MouseMove);
                //objetos[i].lblMatricula.MouseDown += new MouseEventHandler(this.label_MouseDown);
                //objetos[i].lblMatricula.MouseUp += new MouseEventHandler(this.label_MouseUp);
            }

            ACFTs[i].selecionado = true;
            ACFTs[i].lblMatricula.BackColor = Color.Cyan;
        }

        private void label_MouseMove(object sender, MouseEventArgs e)
        {
            Label lbl = (sender as Label);
            if (e.Button == MouseButtons.Left)
            {
                if (indiceSelecionado != indiceSelecionadoOld)
                {
                    indiceSelecionadoOld = indiceSelecionado;
                    ajusteX = e.X;
                    ajusteY = e.Y;
                }

                lbl.Location = new Point(lbl.Left + e.X - ajusteX, lbl.Top + e.Y - ajusteY);
            }
        }

        private void label_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //if(indiceSelecionado != -1)
                //    if (acfts[indiceSelecionado].timer != null)
                //        acfts[indiceSelecionado].timer.Enabled = false;
            }
        }

        private void label_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (indiceSelecionado == -1)
                return;
            //if (indiceSelecionado != indiceSelecionadoOld)
            //    return;

            int i = indiceSelecionado;

            ACFTs[i].Mover((sender as Label).Left + e.X - ajusteX, (sender as Label).Top + e.Y - ajusteY);

            imgCarta.Refresh();

            indiceSelecionadoOld = -2;
        }

    }
}