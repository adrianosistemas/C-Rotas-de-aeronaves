using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.IO;

namespace rota_praia1
{
    public partial class frmRota : Form
    {
        public Grupo grupo = new Grupo();
        public Grupo grupoAnterior = new Grupo();

        public BO_ACFTs listaACFTs = new BO_ACFTs();
        public BO_Locais listaLocais = new BO_Locais();

        private string caminho;

        public frmRota()
        {
            InitializeComponent();
        }

        private void frmRota_Load(object sender, EventArgs e)
        {
            grpCarta.Size = this.Size;
            imgCarta.Size = this.Size;

            AlterarDistanciaLateralMin();
            AlterarDistanciaVerticalMin();
        }

        private void frmRota_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (MessageBox.Show("Verifique se já salvou tudo!\n\nDeseja realmente fechar a tela?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No);
        }

        //-------------------------------------------------------------------------------------------------------------------

        BO bo = new BO();

        private void lerAcfts()
        {
            txtACFTMatricula.Clear();

            List<Acft> acfts = bo.LerAcfts(grupo);

            foreach (var acft in acfts)
            {
                acft.distanciaLateralMin = tckBarDistanciaLateralMinima.Value;
                acft.distanciaVerticalMin = tckBarDistanciaVerticalMinima.Value;
                acft.Incremento = tckBarVelocidade.Value;

                int c = 1;
                foreach (var local in acft.locais)
                {
                    listaLocais.incluirLOCAL(imgCarta, local, 5, Color.Black);
                    
                    if (c == 1)
                        listaACFTs.IncluirACFT(imgCarta, acft, local, tckBarTamFonte.Value);
                    else
                        acft.IncluirRetaNoFinal(local);
                    
                    c++;
                }
            }
        }

        private void lerLocais()
        {
            List<Local> locais = bo.LerLocais(grupo);

            foreach (var local in locais)
                listaLocais.incluirLOCAL(imgCarta, local, 5, Color.Black);
        }

        private void gravarGrupoNovo()
        {
            if (grupo.Id == 0)
            {
                if (listaACFTs.ACFTs.Count > 0 || listaLocais.locais.Count > 0)
                {
                    if (MessageBox.Show("Os objetos da tela não foram salvos! \n\nDeseja criar grupo agora?", "Grupo novo", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        MessageBox.Show("Não foi possível salvar as Acfts e Locais da tela!", "Grupo inválido", MessageBoxButtons.OK);
                        return;
                    }
                }

                grupo = AbrirGrupos(true);
             
                if (grupo.Id == -1)
                    grupo.Id = 0;
            }
            
            gravarGrupoAtual();
        }

        private void gravarGrupoAtual()
        {
            if (grupo.Id != 0)
            {
                listaACFTs.ACFTs.ForEach(o => o.donoGrupo = grupo);
                listaACFTs.ACFTs.ForEach(o => o.locais.ForEach(p => p.donoGrupo = grupo));
                listaLocais.locais.ForEach(o => o.donoGrupo = grupo);

                txtGrupoTitulo.Text = grupo.Titulo;
                txtGrupoDescricao.Text = grupo.Descricao;

                bo.InserirAlterarAcft(listaACFTs.ACFTs);

                bo.ExcluirLocaisGrupo(grupo);
                bo.InserirLocais(listaLocais.locais.Where(o => o.donoAcft == null).ToList());

                MessageBox.Show("Todos os dados foram gravados com sucesso.", "Salvar", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Não foi possível salvar as Acfts e Locais da tela!", "Grupo inválido", MessageBoxButtons.OK);
                return;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------

        private void incluirACFT(Local local)
        {
            Acft acft = new Acft();
            
            acft.donoGrupo = grupo;

            acft.Matricula = txtACFTMatricula.Text;
            acft.Tipo = txtACFTTipo.Text;
            acft.Velocidade = double.Parse(txtACFTVelocidade.Text);
            acft.Altitude = int.Parse(txtACFTAltitude.Text);
            acft.distanciaLateralMin = tckBarDistanciaLateralMinima.Value;
            acft.distanciaVerticalMin = tckBarDistanciaVerticalMinima.Value;

            acft.Incremento = tckBarVelocidade.Value;

            acft.CorFonteMatricula = lblCorAcft.ForeColor;
            acft.CorLinhas = lblCorLinhas.ForeColor;
            acft.CorPerimetro = lblCorPerimetro.ForeColor;
            acft.CorFonteAltitude = lblCorAltitude.ForeColor;

            listaACFTs.IncluirACFT(imgCarta, acft, local, tckBarTamFonte.Value);

            //imgCarta.SendToBack();
        }

        //-------------------------------------------------------------------------------------------------------------------

        void imgCarta_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < listaACFTs.ACFTs.Count; i++)
            {
                if (listaACFTs.ACFTs[i].MostrarRumo)
                    foreach (var reta in listaACFTs.ACFTs[i].retas)
                    {
                        Pen p = new Pen(listaACFTs.ACFTs[i].CorLinhas);

                        e.Graphics.DrawLine(p, reta.Ini.X, reta.Ini.Y, reta.Fim.X, reta.Fim.Y);
                    }

                if (listaACFTs.ACFTs[i].MostrarPerimetro)
                    foreach (var reta in listaACFTs.ACFTs[i].retas)
                    {
                        Pen p = new Pen(listaACFTs.ACFTs[i].CorPerimetro);

                        e.Graphics.DrawArc(p, listaACFTs.ACFTs[i].Posicao.X - (listaACFTs.ACFTs[i].distanciaLateralMin / 2)
                                            , listaACFTs.ACFTs[i].Posicao.Y - (listaACFTs.ACFTs[i].distanciaLateralMin / 2)
                                            , listaACFTs.ACFTs[i].distanciaLateralMin, listaACFTs.ACFTs[i].distanciaLateralMin, 0, 360);
                    }
            }
        }

        private void imgCarta_MouseClick(object sender, MouseEventArgs e)
        {
            switch (tabPrincipal.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    int i = listaACFTs.indiceSelecionado;

                    if (e.Button == MouseButtons.Left || (i != -1 && !listaACFTs.ACFTs[i].timer.Enabled))
                        imgCarta_ACRF_ClickEsquerdo(e.X, e.Y);
                    else
                        imgCarta_ACRF_ClickDireito(e.X, e.Y);

                    break;
                case 2:
                    int j = listaLocais.indiceSelecionado;
                    if (e.Button == MouseButtons.Left || j != -1)
                        imgCarta_LOCAL_ClickEsquerdo(e.X, e.Y);
                    else
                        imgCarta_LOCAL_ClickDireito(e.X, e.Y);

                    break;
                case 3:
                    break;

                default:
                    break;
            }
        }

        private void imgCarta_ACRF_ClickEsquerdo(int x, int y)
        {
            int i = listaACFTs.indiceSelecionado;

            //if (!validouCampos())
            //    return;

            if (txtACFTMatricula.Text != "")
            {
                Local local = listaLocais.incluirLOCAL(imgCarta, x, y, "Ini", "", 0, 5, Color.Black);
                incluirACFT(local);
                txtACFTMatricula.Text = "";
            }
            else
                if (i != -1)
                {
                    Local local = listaLocais.incluirLOCAL(imgCarta, x, y, "A", "", 0, 5, Color.Black);
                    //acfts.objetos[i].MudarRumo(local);
                    listaACFTs.ACFTs[i].IncluirRetaNoFinal(local);
                    //imgCarta.Refresh();
                }

            imgCarta.Refresh();
        }

        private void imgCarta_ACRF_ClickDireito(int x, int y)
        {
            int i = listaACFTs.indiceSelecionado;

            imgCarta.Refresh();
        }

        //

        private void imgCarta_LOCAL_ClickEsquerdo(int x, int y)
        {
            int i = listaLocais.indiceSelecionado;

            //if (!validouCampos())
            //    return;

            if (txtLocalNome.Text != "")
            {
                Local local = listaLocais.incluirLOCAL(imgCarta, x, y, txtLocalNome.Text, txtLocalTipo.Text, double.Parse(txtLocalAltitude.Text), tckBarTamFonte.Value, Color.Black);
                local.donoGrupo = grupo;
                txtLocalNome.Text = "";
            }

            imgCarta.Refresh();
        }

        private void imgCarta_LOCAL_ClickDireito(int x, int y)
        {
            int i = listaLocais.indiceSelecionado;

            imgCarta.Refresh();
        }

        //

        public void ImgCarta_Refresh()
        {
            chkMostrarImagem.Checked = !chkMostrarImagem.Checked;
            chkMostrarImagem.Checked = !chkMostrarImagem.Checked;

            imgCarta.Refresh();
        }

        //----------------------------------------------------------------------------------------

        /*
        private bool validouCampos()
        {
            try
            {
                int v = Int32.Parse(txtACFTVelocidade.Text);
                if (v <= 0)
                    throw new Exception();
                return true;
            }
            catch (Exception) 
            {
                MessageBox.Show("Velocidade deve ser um número Inteiro maior que zero", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return false;
        }
        */

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            foreach (var acft in listaACFTs.ACFTs)
                acft.Incremento = tckBarVelocidade.Value;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            frmAcfts frmAltAcfts = new frmAcfts();
            frmAltAcfts.frmChamador = this;
            frmAltAcfts.acfts = listaACFTs;
            frmAltAcfts.locais = listaLocais;
            frmAltAcfts.AtualizarGridAcfts();
            frmAltAcfts.AtualizarGridLocais();

            frmAltAcfts.Show();
        }

        //------------------------------------------------------------------------------------------

        private void dgvRetas_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            imgCarta.Refresh();
        }

        //----------------------------------------------------------------------------------------

        private void btnAbrirLocalImagem_Click(object sender, EventArgs e)
        {
            DialogResult local = openFileDialog1.ShowDialog();
            if (local.ToString() == "OK")
            {
                caminho = openFileDialog1.FileName;
                imgCarta.Load(caminho);
                imgCarta.SizeMode = PictureBoxSizeMode.AutoSize;
                Size tam = new Size(imgCarta.Size.Width, imgCarta.Size.Height);

                imgCarta.SizeMode = PictureBoxSizeMode.Normal;
                imgCarta.Size = tam;
                chkMostrarImagem.Checked = true;
            }
        }

        private void chkMostrarImagem_CheckedChanged(object sender, EventArgs e)
        {
            if (caminho == null)
                return;

            if (!chkMostrarImagem.Checked)
                imgCarta.Image = null;
            else
                imgCarta.Load(caminho);

            listaACFTs.HabilitarPiscar(chkMostrarImagem.Checked);
        }

        private void tckBarTamFonte_Scroll(object sender, EventArgs e)
        {
            listaACFTs.AlterarTamanhoFonte(tckBarTamFonte.Value);
        }

        private void AlterarDistanciaLateralMin()
        {
            lblDistanciaLateralMinima.Text = "Distância Lateral Mínima : " + tckBarDistanciaLateralMinima.Value;
            listaACFTs.AlterarDistanciaEntreACFTs(tckBarDistanciaLateralMinima.Value, tckBarDistanciaVerticalMinima.Value);
        }

        private void AlterarDistanciaVerticalMin()
        {
            lblDistanciaVerticalMinima.Text = "Distância Vertical Mínima : " + tckBarDistanciaVerticalMinima.Value;
            listaACFTs.AlterarDistanciaEntreACFTs(tckBarDistanciaLateralMinima.Value, tckBarDistanciaVerticalMinima.Value);
        }

        private void tckBarDistanciaLateralMin_Scroll(object sender, EventArgs e)
        {
            AlterarDistanciaLateralMin();
        }

        private void tckBarDistanciaVerticalMin_Scroll(object sender, EventArgs e)
        {
            AlterarDistanciaVerticalMin();
        }

        private void lblCorAcft_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                lblCorAcft.ForeColor = colorDialog1.Color;

                Acft acft = new Acft();
                if (listaACFTs.indiceSelecionado != -1)
                    acft = listaACFTs.ACFTs[listaACFTs.indiceSelecionado];
                
                acft.CorFonteMatricula = lblCorAcft.ForeColor;
            }
        }

        private void lblCorAltitude_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                lblCorAltitude.ForeColor = colorDialog1.Color;

                Acft acft = new Acft();
                if (listaACFTs.indiceSelecionado != -1)
                    acft = listaACFTs.ACFTs[listaACFTs.indiceSelecionado];

                acft.CorFonteAltitude = lblCorAltitude.ForeColor;
            }
        }

        private void lblCorLinhas_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                lblCorLinhas.ForeColor = colorDialog1.Color;

                Acft acft = new Acft();
                if (listaACFTs.indiceSelecionado != -1)
                    acft = listaACFTs.ACFTs[listaACFTs.indiceSelecionado];

                acft.CorLinhas = lblCorLinhas.ForeColor;
            }
        }

        private void lblCorPerimetro_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                lblCorPerimetro.ForeColor = colorDialog1.Color;

                Acft acft = new Acft();
                if (listaACFTs.indiceSelecionado != -1)
                    acft = listaACFTs.ACFTs[listaACFTs.indiceSelecionado];

                acft.CorPerimetro = lblCorPerimetro.ForeColor;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (grupo.Id == 0)
            {
                if (listaACFTs.ACFTs.Count > 0 || listaLocais.locais.Count > 0)
                    gravarGrupoNovo();
                else
                    MessageBox.Show("Antes de salvar, você deve inserir algum objeto na tela.", "Impossível salvar grupo", MessageBoxButtons.OK);
            }
            else
                gravarGrupoAtual();
        }


        private void btnCarregarGrupo_Click(object sender, EventArgs e)
        {
            Grupo grupoNovo = new Grupo();

            if (grupo.Id == 0)
            {
                if (listaACFTs.ACFTs.Count > 0 || listaLocais.locais.Count > 0)
                {
                    //CarregarGrupo_porNovo();
                    gravarGrupoNovo();
                    CarregarGrupo_porExistente();
                }
                else
                    CarregarGrupo_porExistente();
            }
            else
                CarregarGrupo_porExistente();
        }

        private void AbrirPaginaEmBranco()
        {
            grupoAnterior = grupo;
            grupo = new Grupo();
            Inicializar();

            imgCarta.Refresh();

            MessageBox.Show("Você está abrindo uma página em branco. \n\n Não esqueça de salvar seu novo grupo!", "Grupo novo ainda não salvo", MessageBoxButtons.OK);
        }

        private void AbrirGrupo(Grupo grupoAbrir)
        {
            grupoAnterior = grupo;
            grupo = grupoAbrir;
            Inicializar();

            lerAcfts();
            lerLocais();

            imgCarta.Refresh();
        }

        /*
        private void CarregarGrupo_porNovo()
        {
            Grupo grupoNovo = new Grupo();

            if (MessageBox.Show("Os dados não foram salvos!\n\nDeseja criar um novo grupo?", "Novo grupo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                grupoNovo = AbrirGrupos(true);
                
                if (grupo.Id == -1)
                {
                    grupo.Id = 0;
                    return;
                }
                //if (grupoNovo.Id != 0)
                //    AbrirPaginaEmBranco();
            }
            else
            {
                CarregarGrupo_porExistente();
            }

            //imgCarta.Refresh();
        }
        */

        private void CarregarGrupo_porExistente()
        {
            ////if (houvealteracao)
            //{
            //    if (MessageBox.Show("Os dados não foram salvos!\n\nDeseja salvar o grupo agora?", "Salvar grupo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        gravarGrupoAtual();
            //}

            Grupo grupoNovo = AbrirGrupos();

            if (grupoNovo.Id == -1)
                return;

            if (grupoNovo.Id == 0)
                AbrirPaginaEmBranco();
            else
            {
                if (grupoNovo.Id != grupo.Id)
                    AbrirGrupo(grupoNovo);
                else
                    MessageBox.Show("Esse grupo já está em execução!", "Tá com falta de atenção?", MessageBoxButtons.OK);
            }

            //imgCarta.Refresh();
        }

        private void Inicializar()
        {
            listaACFTs.Inicializar();
            listaLocais.Inicializar();

            txtGrupoTitulo.Text = grupo.Titulo;
            txtGrupoDescricao.Text = grupo.Descricao;

            //txtGrupoTitulo.Text = "";
            //txtGrupoDescricao.Text = "";
        }

        private Grupo AbrirGrupos(bool novoGrupo = false)
        {
            frmGrupos frmgrupos = new frmGrupos();

            frmgrupos.novoGrupo = novoGrupo;
            frmgrupos.ShowDialog();

            return frmgrupos.grupoSelecionado;
        }

        private void txtGrupoTitulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("Não mexe aqui, cara!", "Fica a dica", MessageBoxButtons.OK);
            e.KeyChar = '\0';
        }

        private void txtGrupoDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("Não mexe aqui também, cara!", "Fica a dica", MessageBoxButtons.OK);
            e.KeyChar = '\0';
        }

        private void tabPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Visible = tabPrincipal.SelectedIndex < 3;
        }

    }
}