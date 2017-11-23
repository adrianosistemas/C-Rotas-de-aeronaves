using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace rota_praia1
{
    public class BO_Locais
    {
        public PictureBox imgCarta = null;
        public List<Local> locais = new List<Local>();

        public int indiceSelecionado = -1;
        
        int indiceSelecionadoOld = -2;
        int ajusteX, ajusteY;

        //

        public void Inicializar()
        {
            locais.ForEach(o => o.Inicializar());
            locais.Clear();

            indiceSelecionado = -1;
            indiceSelecionadoOld = -2;
        }

        public Local incluirLOCAL(PictureBox imagem, int X, int Y, string nome, string tipo, double altitude, float tamFonte, Color cor)
        {
            Local local = new Local();
            local.Tipo = tipo;
            local.Nome = nome;
            local.Altitude = altitude;

            local.X = X;
            local.Y = Y;

            return incluirLOCAL(imagem, local, tamFonte, cor);
        }

        public Local incluirLOCAL(PictureBox imagem, Local local, float tamFonte, Color cor)
        {
            imgCarta = imagem;

            local.lblNome.Parent = imagem;
            local.lblNome.AutoSize = true;
            local.lblNome.BorderStyle = BorderStyle.FixedSingle;

            local.lblNome.Text = local.Nome;
            local.lblNome.ForeColor = cor;

            local.lblNome.Left = local.X;
            local.lblNome.Top = local.Y;
            local.lblNome.Font = new Font("Tahoma", tamFonte, FontStyle.Bold);

            local.lblNome.Click += new EventHandler(this.lbl_Click);

            locais.Add(local);

            this.lbl_Click(local.lblNome, new EventArgs());

            return local;
        }

        /*
        public void excluirLOCAL(Local local)
        {
            locais.Remove(local);
        }
        */

        public void alterarTamanhoFonte(float tamFonte)
        {
            foreach (var loc in locais)
                loc.lblNome.Font = new Font("Tahoma", tamFonte, FontStyle.Bold);
        }

        void lbl_Click(object sender, EventArgs e)
        {
            this.mudarFundo(Color.White);
            int i = this.procurarLocal(sender as Label);
            this.selecionarLocal(i);
        }

        private void mudarFundo(Color color)
        {
            foreach (var loc in locais)
                loc.lblNome.BackColor = color;
        }

        private int procurarLocal(Label lbl)
        {
            int i = 0, indice = -1;
            foreach (var loc in locais)
            {
                if (loc.lblNome == lbl)
                    indice = i;
                i++;
            }
            return indice;
        }

        private void selecionarLocal(int i)
        {
            if (i != indiceSelecionado)
            {
                indiceSelecionadoOld = indiceSelecionado;
                indiceSelecionado = i;
                locais[i].lblNome.MouseMove -= new MouseEventHandler(this.label_MouseMove);
                locais[i].lblNome.MouseDown -= new MouseEventHandler(this.label_MouseDown);
                locais[i].lblNome.MouseUp -= new MouseEventHandler(this.label_MouseUp);
                //locais[i].lblNome.KeyUp -= new KeyEventHandler(this.label_KeyUp);
                locais[i].lblNome.DoubleClick -= new EventHandler(this.label_DoubleClick);

                locais[i].lblNome.MouseMove += new MouseEventHandler(this.label_MouseMove);
                locais[i].lblNome.MouseDown += new MouseEventHandler(this.label_MouseDown);
                locais[i].lblNome.MouseUp += new MouseEventHandler(this.label_MouseUp);
                //locais[i].lblNome.KeyUp += new KeyEventHandler(this.label_KeyUp);
                locais[i].lblNome.DoubleClick += new EventHandler(this.label_DoubleClick);
            }

            locais[i].lblNome.BackColor = Color.Cyan;
        }

        private void label_MouseMove(object sender, MouseEventArgs e)
        {
            Label lbl = (sender as Label);

            if (e.Button == MouseButtons.Left)
            {
                if (this.indiceSelecionado != this.indiceSelecionadoOld)
                {
                    this.indiceSelecionadoOld = this.indiceSelecionado;
                    this.ajusteX = e.X;
                    this.ajusteY = e.Y;
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

            locais[i].Mover((sender as Label).Left + e.X - ajusteX, (sender as Label).Top + e.Y - ajusteY);

            imgCarta.Refresh();

            indiceSelecionadoOld = -2;
        }

        /*
        private void label_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (var local in locais)
                {
                    if (local.lblNome == sender)
                        locais.Remove(local);
                }
            }
        }
        */

        private void label_DoubleClick(object sender, EventArgs e)
        {
            Local remover = null;
            Local l1 = null;
            Local l2 = null;
            
            int c = 0, i = 0;
            
            Acft dono = (locais.Where(o => o.lblNome == sender).ToList()[0]).donoAcft;

            //se for um local avulso
            if (dono == null)
            {
                foreach (var local in locais)
                {
                    if (local.lblNome == sender)
                    {
                        locais.Remove(local);
                        local.lblNome.Parent = null;
                        indiceSelecionado = -1;
                        //imgCarta.Refresh();
                        return;
                    }
                }
            }


            //---------------------------------------------------------------
            //filtrando apenas os locais da acft selecionada
            List<Local> vlocais = locais.Where(o => o.donoAcft == dono).ToList();

            foreach (var local in vlocais)
            {
                if (local.lblNome == sender)
                    remover = local;
                else
                    if (remover == null)
                    {
                        i = c;
                        l1 = local;
                    }
                    else
                    {
                        l2 = local;
                        break;
                    }
                c++;
            }

            if (l2 == null)
            {
                if (l1.donoAcft.retas.Count == 1)
                {
                    l1.donoAcft.retas[i].Fim = remover.donoAcft.retas[i].Ini;

                    dono.locais.Remove(remover);
                    locais.Remove(remover);
                    remover.lblNome.Parent = null;
                }
                else
                    if (l1.donoAcft.indiceRetaAtual < l1.donoAcft.retas.Count() - 1)
                    {
                        l1.donoAcft.retas[i].Fim = remover.donoAcft.retas[i].Fim;
                        l1.donoAcft.retas.Remove(l1.donoAcft.retas[i]);

                        dono.locais.Remove(remover);
                        locais.Remove(remover);
                        remover.lblNome.Parent = null;

                        if (i == l1.donoAcft.indiceRetaAtual)
                            l1.donoAcft.Trafegar(l1.donoAcft.retas[l1.donoAcft.indiceRetaAtual].Fim);
                    }
            }
            else
            {
                if (l1 == null)
                    return;

                l1.donoAcft.retas[i].Fim = remover.donoAcft.retas[i + 1].Fim;
                l1.donoAcft.retas.Remove(l1.donoAcft.retas[i + 1]);

                if (i == l1.donoAcft.indiceRetaAtual)
                    l1.donoAcft.Trafegar(l2);
                else
                    if (i < l1.donoAcft.indiceRetaAtual)
                        --l1.donoAcft.indiceRetaAtual;

                dono.locais.Remove(remover);
                locais.Remove(remover);
                remover.lblNome.Parent = null;
            }

            indiceSelecionado = -1;
            imgCarta.Refresh();
        }

    }
}