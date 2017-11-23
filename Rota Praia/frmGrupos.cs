using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace rota_praia1
{
    public partial class frmGrupos : Form
    {
        public bool novoGrupo = false;
        public Grupo grupoSelecionado = null;

        BO bo = new BO();
        public BindingSource bsGrupos = new BindingSource();
        
        public frmGrupos()
        {
            InitializeComponent();
            grdGrupos.DataSource = bsGrupos;
        }

        private void frmGrupos_Load(object sender, EventArgs e)
        {
            if (novoGrupo)
            {
                lblNomeTela.ForeColor = Color.Green;
                lblNomeTela.Text = "Por favor, informe os dados do NOVO Grupo de Aeronaves";
                btnSalvar.Text = "Salvar novo grupo";
            }
            else
            {
                lblNomeTela.ForeColor = Color.Blue;
                lblNomeTela.Text = "Selecione o Grupo de Aeronaves para ABRIR na tela";
                btnSalvar.Text = "Salvar todos grupos";
            }

            AtualizarGridGrupos();
        }

        public void AtualizarGridGrupos()
        {
            List<Grupo> listaGrupos = bo.LerGrupos();
            if (novoGrupo)
                listaGrupos = listaGrupos.Where(o => o.Id == 0).ToList();

            bsGrupos.DataSource = listaGrupos;
            bsGrupos.ResetBindings(true);
        }

        private void grdGrupos_DoubleClick(object sender, EventArgs e)
        {
            grupoSelecionado = (Grupo)(grdGrupos.CurrentRow.DataBoundItem);

            if (grupoSelecionado == null)
                grupoSelecionado = new Grupo();

            Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (grdGrupos.CurrentRow == null)
            {
                MessageBox.Show("Você deve cadastrar os dados do grupo e depois salvar!", "Grupo inválido", MessageBoxButtons.OK);
                return;
            }

            grupoSelecionado = (Grupo)(grdGrupos.CurrentRow.DataBoundItem);

            if (grupoSelecionado.Data == null)
                grupoSelecionado.Data = DateTime.Now;
            if (grupoSelecionado.Titulo == null)
                grupoSelecionado.Titulo = "";
            if (grupoSelecionado.Descricao == null)
                grupoSelecionado.Descricao = "";

            bo.InserirAlterarGrupo((List<Grupo>)bsGrupos.DataSource);
        }

        private void frmGrupos_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (grdGrupos.CurrentRow == null)
            {
                grupoSelecionado = new Grupo();
                grupoSelecionado.Id = -1;
                return;
            }
            */

            //grupoSelecionado = (Grupo)(grdGrupos.CurrentRow.DataBoundItem);

            if (grupoSelecionado == null)
            {
                grupoSelecionado = new Grupo();
                grupoSelecionado.Id = -1;
            }
            else
            {
                if (novoGrupo && grupoSelecionado.Id == 0)
                {
                    MessageBox.Show("Você ainda não gravou os dados cadastrados!", "Salvar", MessageBoxButtons.OK);
                    e.Cancel = true;
                    grupoSelecionado = null;
                    /*
                    if (MessageBox.Show("Deseja realmente sair da tela?", "Confirmar sem salvar", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                    */
                }
            }
        }

    }
}
