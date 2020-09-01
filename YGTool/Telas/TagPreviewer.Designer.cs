namespace YGTool.Telas
{
    partial class TagPreviewer
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirtxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textoParaEdicao = new System.Windows.Forms.TextBox();
            this.listaDeDialogos = new System.Windows.Forms.ListBox();
            this.textoOriginal = new System.Windows.Forms.TextBox();
            this.cenaBox = new System.Windows.Forms.ComboBox();
            this.cenaPicture = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.totalDePonteiros = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cenaPicture)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1344, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirtxtToolStripMenuItem,
            this.salvarToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // abrirtxtToolStripMenuItem
            // 
            this.abrirtxtToolStripMenuItem.Name = "abrirtxtToolStripMenuItem";
            this.abrirtxtToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.abrirtxtToolStripMenuItem.Text = "Abrir";
            this.abrirtxtToolStripMenuItem.Click += new System.EventHandler(this.abrirtxtToolStripMenuItem_Click);
            // 
            // salvarToolStripMenuItem
            // 
            this.salvarToolStripMenuItem.Name = "salvarToolStripMenuItem";
            this.salvarToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.salvarToolStripMenuItem.Text = "Salvar";
            // 
            // textoParaEdicao
            // 
            this.textoParaEdicao.Location = new System.Drawing.Point(48, 40);
            this.textoParaEdicao.Multiline = true;
            this.textoParaEdicao.Name = "textoParaEdicao";
            this.textoParaEdicao.Size = new System.Drawing.Size(315, 141);
            this.textoParaEdicao.TabIndex = 1;
            this.textoParaEdicao.TextChanged += new System.EventHandler(this.textoParaEdicao_TextChanged);
            // 
            // listaDeDialogos
            // 
            this.listaDeDialogos.FormattingEnabled = true;
            this.listaDeDialogos.Location = new System.Drawing.Point(8, 55);
            this.listaDeDialogos.Name = "listaDeDialogos";
            this.listaDeDialogos.Size = new System.Drawing.Size(167, 563);
            this.listaDeDialogos.TabIndex = 2;
            this.listaDeDialogos.SelectedValueChanged += new System.EventHandler(this.listaDeDialogos_SelectedValueChanged);
            // 
            // textoOriginal
            // 
            this.textoOriginal.Location = new System.Drawing.Point(48, 335);
            this.textoOriginal.Multiline = true;
            this.textoOriginal.Name = "textoOriginal";
            this.textoOriginal.ReadOnly = true;
            this.textoOriginal.Size = new System.Drawing.Size(315, 140);
            this.textoOriginal.TabIndex = 3;
            // 
            // cenaBox
            // 
            this.cenaBox.FormattingEnabled = true;
            this.cenaBox.Location = new System.Drawing.Point(79, 17);
            this.cenaBox.Name = "cenaBox";
            this.cenaBox.Size = new System.Drawing.Size(244, 21);
            this.cenaBox.TabIndex = 4;
            this.cenaBox.SelectedIndexChanged += new System.EventHandler(this.cenaBox_SelectedIndexChanged);
            // 
            // cenaPicture
            // 
            this.cenaPicture.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.cenaPicture.Location = new System.Drawing.Point(39, 58);
            this.cenaPicture.Name = "cenaPicture";
            this.cenaPicture.Size = new System.Drawing.Size(575, 114);
            this.cenaPicture.TabIndex = 5;
            this.cenaPicture.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Diálogos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(94, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 26);
            this.label2.TabIndex = 7;
            this.label2.Text = "Texto Editável";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(114, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 26);
            this.label3.TabIndex = 8;
            this.label3.Text = "Texto Original";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(261, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 26);
            this.label4.TabIndex = 9;
            this.label4.Text = "Prévia";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 26);
            this.label5.TabIndex = 10;
            this.label5.Text = "Cena:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Total de Ponteiros:";
            // 
            // totalDePonteiros
            // 
            this.totalDePonteiros.AutoSize = true;
            this.totalDePonteiros.Location = new System.Drawing.Point(111, 39);
            this.totalDePonteiros.Name = "totalDePonteiros";
            this.totalDePonteiros.Size = new System.Drawing.Size(13, 13);
            this.totalDePonteiros.TabIndex = 12;
            this.totalDePonteiros.Text = "0";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.totalDePonteiros);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.listaDeDialogos);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 634);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.textoOriginal);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textoParaEdicao);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(213, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(411, 634);
            this.panel2.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.cenaPicture);
            this.panel3.Location = new System.Drawing.Point(663, 95);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(647, 566);
            this.panel3.TabIndex = 15;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.cenaBox);
            this.panel4.Location = new System.Drawing.Point(663, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(647, 52);
            this.panel4.TabIndex = 16;
            // 
            // TagPreviewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 729);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TagPreviewer";
            this.Text = "TagPreviewer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cenaPicture)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirtxtToolStripMenuItem;
        private System.Windows.Forms.TextBox textoParaEdicao;
        private System.Windows.Forms.ListBox listaDeDialogos;
        private System.Windows.Forms.TextBox textoOriginal;
        private System.Windows.Forms.ComboBox cenaBox;
        private System.Windows.Forms.PictureBox cenaPicture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem salvarToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label totalDePonteiros;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}