namespace rota_praia1
{
    partial class frmAcfts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdAcfts = new System.Windows.Forms.DataGridView();
            this.matriculaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.velocidadeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.altitudeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corFonteMatriculaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corFonteAltitudeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corLinhasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corPerimetroDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.posicaoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mostrarMatriculaDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mostrarRumoDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MostrarPerimetro = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bsAcrf = new System.Windows.Forms.BindingSource(this.components);
            this.grpGrid = new System.Windows.Forms.GroupBox();
            this.grdLocais = new System.Windows.Forms.DataGridView();
            this.grdAcftLocais = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdAcfts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAcrf)).BeginInit();
            this.grpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocais)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAcftLocais)).BeginInit();
            this.SuspendLayout();
            // 
            // grdAcfts
            // 
            this.grdAcfts.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdAcfts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdAcfts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAcfts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.matriculaDataGridViewTextBoxColumn,
            this.tipoDataGridViewTextBoxColumn,
            this.velocidadeDataGridViewTextBoxColumn,
            this.altitudeDataGridViewTextBoxColumn,
            this.corFonteMatriculaDataGridViewTextBoxColumn,
            this.corFonteAltitudeDataGridViewTextBoxColumn,
            this.corLinhasDataGridViewTextBoxColumn,
            this.corPerimetroDataGridViewTextBoxColumn,
            this.xDataGridViewTextBoxColumn,
            this.yDataGridViewTextBoxColumn,
            this.posicaoDataGridViewTextBoxColumn,
            this.mostrarMatriculaDataGridViewCheckBoxColumn,
            this.mostrarRumoDataGridViewCheckBoxColumn,
            this.MostrarPerimetro});
            this.grdAcfts.DataSource = this.bsAcrf;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdAcfts.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdAcfts.Location = new System.Drawing.Point(17, 22);
            this.grdAcfts.Name = "grdAcfts";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdAcfts.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdAcfts.Size = new System.Drawing.Size(1019, 184);
            this.grdAcfts.TabIndex = 20;
            this.grdAcfts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAcfts_CellContentClick);
            // 
            // matriculaDataGridViewTextBoxColumn
            // 
            this.matriculaDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.matriculaDataGridViewTextBoxColumn.DataPropertyName = "Matricula";
            this.matriculaDataGridViewTextBoxColumn.Frozen = true;
            this.matriculaDataGridViewTextBoxColumn.HeaderText = "Matricula";
            this.matriculaDataGridViewTextBoxColumn.Name = "matriculaDataGridViewTextBoxColumn";
            // 
            // tipoDataGridViewTextBoxColumn
            // 
            this.tipoDataGridViewTextBoxColumn.DataPropertyName = "Tipo";
            this.tipoDataGridViewTextBoxColumn.HeaderText = "Tipo";
            this.tipoDataGridViewTextBoxColumn.Name = "tipoDataGridViewTextBoxColumn";
            this.tipoDataGridViewTextBoxColumn.Width = 80;
            // 
            // velocidadeDataGridViewTextBoxColumn
            // 
            this.velocidadeDataGridViewTextBoxColumn.DataPropertyName = "Velocidade";
            this.velocidadeDataGridViewTextBoxColumn.HeaderText = "Velocidade";
            this.velocidadeDataGridViewTextBoxColumn.Name = "velocidadeDataGridViewTextBoxColumn";
            this.velocidadeDataGridViewTextBoxColumn.Width = 70;
            // 
            // altitudeDataGridViewTextBoxColumn
            // 
            this.altitudeDataGridViewTextBoxColumn.DataPropertyName = "Altitude";
            this.altitudeDataGridViewTextBoxColumn.HeaderText = "Altitude";
            this.altitudeDataGridViewTextBoxColumn.Name = "altitudeDataGridViewTextBoxColumn";
            this.altitudeDataGridViewTextBoxColumn.Width = 60;
            // 
            // corFonteMatriculaDataGridViewTextBoxColumn
            // 
            this.corFonteMatriculaDataGridViewTextBoxColumn.DataPropertyName = "CorFonteMatricula";
            this.corFonteMatriculaDataGridViewTextBoxColumn.HeaderText = "Cor Matricula";
            this.corFonteMatriculaDataGridViewTextBoxColumn.Name = "corFonteMatriculaDataGridViewTextBoxColumn";
            this.corFonteMatriculaDataGridViewTextBoxColumn.Visible = false;
            // 
            // corFonteAltitudeDataGridViewTextBoxColumn
            // 
            this.corFonteAltitudeDataGridViewTextBoxColumn.DataPropertyName = "CorFonteAltitude";
            this.corFonteAltitudeDataGridViewTextBoxColumn.HeaderText = "Cor Altitude";
            this.corFonteAltitudeDataGridViewTextBoxColumn.Name = "corFonteAltitudeDataGridViewTextBoxColumn";
            this.corFonteAltitudeDataGridViewTextBoxColumn.Visible = false;
            // 
            // corLinhasDataGridViewTextBoxColumn
            // 
            this.corLinhasDataGridViewTextBoxColumn.DataPropertyName = "CorLinhas";
            this.corLinhasDataGridViewTextBoxColumn.HeaderText = "Cor Linhas";
            this.corLinhasDataGridViewTextBoxColumn.Name = "corLinhasDataGridViewTextBoxColumn";
            this.corLinhasDataGridViewTextBoxColumn.Visible = false;
            // 
            // corPerimetroDataGridViewTextBoxColumn
            // 
            this.corPerimetroDataGridViewTextBoxColumn.DataPropertyName = "CorPerimetro";
            this.corPerimetroDataGridViewTextBoxColumn.HeaderText = "Cor Perimetro";
            this.corPerimetroDataGridViewTextBoxColumn.Name = "corPerimetroDataGridViewTextBoxColumn";
            this.corPerimetroDataGridViewTextBoxColumn.Visible = false;
            // 
            // xDataGridViewTextBoxColumn
            // 
            this.xDataGridViewTextBoxColumn.DataPropertyName = "X";
            this.xDataGridViewTextBoxColumn.HeaderText = "X";
            this.xDataGridViewTextBoxColumn.Name = "xDataGridViewTextBoxColumn";
            this.xDataGridViewTextBoxColumn.Visible = false;
            // 
            // yDataGridViewTextBoxColumn
            // 
            this.yDataGridViewTextBoxColumn.DataPropertyName = "Y";
            this.yDataGridViewTextBoxColumn.HeaderText = "Y";
            this.yDataGridViewTextBoxColumn.Name = "yDataGridViewTextBoxColumn";
            this.yDataGridViewTextBoxColumn.Visible = false;
            // 
            // posicaoDataGridViewTextBoxColumn
            // 
            this.posicaoDataGridViewTextBoxColumn.DataPropertyName = "Posicao";
            this.posicaoDataGridViewTextBoxColumn.HeaderText = "Posicao";
            this.posicaoDataGridViewTextBoxColumn.Name = "posicaoDataGridViewTextBoxColumn";
            this.posicaoDataGridViewTextBoxColumn.ReadOnly = true;
            this.posicaoDataGridViewTextBoxColumn.Visible = false;
            // 
            // mostrarMatriculaDataGridViewCheckBoxColumn
            // 
            this.mostrarMatriculaDataGridViewCheckBoxColumn.DataPropertyName = "MostrarMatricula";
            this.mostrarMatriculaDataGridViewCheckBoxColumn.HeaderText = "Matricula";
            this.mostrarMatriculaDataGridViewCheckBoxColumn.Name = "mostrarMatriculaDataGridViewCheckBoxColumn";
            this.mostrarMatriculaDataGridViewCheckBoxColumn.Width = 60;
            // 
            // mostrarRumoDataGridViewCheckBoxColumn
            // 
            this.mostrarRumoDataGridViewCheckBoxColumn.DataPropertyName = "MostrarRumo";
            this.mostrarRumoDataGridViewCheckBoxColumn.HeaderText = "Rumo";
            this.mostrarRumoDataGridViewCheckBoxColumn.Name = "mostrarRumoDataGridViewCheckBoxColumn";
            this.mostrarRumoDataGridViewCheckBoxColumn.Width = 60;
            // 
            // MostrarPerimetro
            // 
            this.MostrarPerimetro.DataPropertyName = "MostrarPerimetro";
            this.MostrarPerimetro.HeaderText = "Perimetro";
            this.MostrarPerimetro.Name = "MostrarPerimetro";
            this.MostrarPerimetro.Width = 60;
            // 
            // bsAcrf
            // 
            this.bsAcrf.DataSource = typeof(rota_praia1.Acft);
            // 
            // grpGrid
            // 
            this.grpGrid.Controls.Add(this.grdLocais);
            this.grpGrid.Controls.Add(this.grdAcftLocais);
            this.grpGrid.Controls.Add(this.grdAcfts);
            this.grpGrid.Location = new System.Drawing.Point(9, 5);
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.Size = new System.Drawing.Size(1056, 463);
            this.grpGrid.TabIndex = 21;
            this.grpGrid.TabStop = false;
            // 
            // grdLocais
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdLocais.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdLocais.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdLocais.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdLocais.Location = new System.Drawing.Point(533, 215);
            this.grdLocais.Name = "grdLocais";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdLocais.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grdLocais.Size = new System.Drawing.Size(502, 231);
            this.grdLocais.TabIndex = 22;
            // 
            // grdAcftLocais
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdAcftLocais.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grdAcftLocais.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdAcftLocais.DefaultCellStyle = dataGridViewCellStyle8;
            this.grdAcftLocais.Location = new System.Drawing.Point(17, 215);
            this.grdAcftLocais.Name = "grdAcftLocais";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdAcftLocais.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.grdAcftLocais.Size = new System.Drawing.Size(502, 231);
            this.grdAcftLocais.TabIndex = 21;
            // 
            // frmAcfts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 487);
            this.Controls.Add(this.grpGrid);
            this.Name = "frmAcfts";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro e alteração de Aeronaves";
            ((System.ComponentModel.ISupportInitialize)(this.grdAcfts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAcrf)).EndInit();
            this.grpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLocais)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAcftLocais)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdAcfts;
        private System.Windows.Forms.GroupBox grpGrid;
        private System.Windows.Forms.DataGridView grdAcftLocais;
        private System.Windows.Forms.DataGridView grdLocais;
        private System.Windows.Forms.BindingSource bsAcrf;
        private System.Windows.Forms.DataGridViewTextBoxColumn matriculaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn velocidadeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn altitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn corFonteMatriculaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn corFonteAltitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn corLinhasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn corPerimetroDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn posicaoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mostrarMatriculaDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mostrarRumoDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MostrarPerimetro;
    }
}