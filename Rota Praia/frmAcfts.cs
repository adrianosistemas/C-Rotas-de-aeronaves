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
    public partial class frmAcfts : Form
    {
        public frmRota frmChamador;
        public BO_ACFTs acfts;
        public BO_Locais locais;

        public BindingSource bsAcfts = new BindingSource();
        public BindingSource bsLocais = new BindingSource();
        
        public frmAcfts()
        {
            InitializeComponent();
            grdAcfts.DataSource = bsAcfts;
        }

        public void AtualizarGridAcfts()
        {
            grdAcfts.AutoGenerateColumns = true;

            bsAcfts.DataSource = acfts.ACFTs;
            bsAcfts.ResetBindings(true);
        }

        public void AtualizarGridLocais()
        {
            grdAcfts.AutoGenerateColumns = true;

            bsAcfts.DataSource = acfts.ACFTs;
            bsAcfts.ResetBindings(true);
        }

        private void dgvAcfts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11)
                acfts.ACFTs[e.RowIndex].MostrarMatricula = !acfts.ACFTs[e.RowIndex].MostrarMatricula;

            if (e.ColumnIndex == 12)
                acfts.ACFTs[e.RowIndex].MostrarRumo = !acfts.ACFTs[e.RowIndex].MostrarRumo;
                
            if (e.ColumnIndex == 13)
            {
                acfts.ACFTs[e.RowIndex].MostrarPerimetro = !acfts.ACFTs[e.RowIndex].MostrarPerimetro;
                frmChamador.ImgCarta_Refresh();
            }
        }

    }
}
