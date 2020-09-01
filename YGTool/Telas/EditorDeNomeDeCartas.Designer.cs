namespace YGTool.Telas
{
    partial class EditorDeNomeDeCartas
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
            this.listaDeImagens = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.painelImgCarta = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.preferênciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selecionarPastaComPNGSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureNomeOrdemCerta = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBoxEditaval = new System.Windows.Forms.PictureBox();
            this.comboCorFonte = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboFundoAtributo = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.painelImgCarta)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNomeOrdemCerta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEditaval)).BeginInit();
            this.SuspendLayout();
            // 
            // listaDeImagens
            // 
            this.listaDeImagens.FormattingEnabled = true;
            this.listaDeImagens.Location = new System.Drawing.Point(12, 47);
            this.listaDeImagens.Name = "listaDeImagens";
            this.listaDeImagens.Size = new System.Drawing.Size(120, 212);
            this.listaDeImagens.TabIndex = 0;
            this.listaDeImagens.SelectedIndexChanged += new System.EventHandler(this.listaDeImagens_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "BMP";
            // 
            // painelImgCarta
            // 
            this.painelImgCarta.BackColor = System.Drawing.SystemColors.ControlDark;
            this.painelImgCarta.Location = new System.Drawing.Point(356, 77);
            this.painelImgCarta.Name = "painelImgCarta";
            this.painelImgCarta.Size = new System.Drawing.Size(128, 64);
            this.painelImgCarta.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.painelImgCarta.TabIndex = 2;
            this.painelImgCarta.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferênciasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(496, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // preferênciasToolStripMenuItem
            // 
            this.preferênciasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selecionarPastaComPNGSToolStripMenuItem});
            this.preferênciasToolStripMenuItem.Name = "preferênciasToolStripMenuItem";
            this.preferênciasToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.preferênciasToolStripMenuItem.Text = "Preferências";
            // 
            // selecionarPastaComPNGSToolStripMenuItem
            // 
            this.selecionarPastaComPNGSToolStripMenuItem.Name = "selecionarPastaComPNGSToolStripMenuItem";
            this.selecionarPastaComPNGSToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.selecionarPastaComPNGSToolStripMenuItem.Text = "Selecionar pasta com imagens de cartas";
            this.selecionarPastaComPNGSToolStripMenuItem.Click += new System.EventHandler(this.selecionarPastaComPNGSToolStripMenuItem_Click);
            // 
            // pictureNomeOrdemCerta
            // 
            this.pictureNomeOrdemCerta.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureNomeOrdemCerta.Location = new System.Drawing.Point(356, 47);
            this.pictureNomeOrdemCerta.Name = "pictureNomeOrdemCerta";
            this.pictureNomeOrdemCerta.Size = new System.Drawing.Size(96, 12);
            this.pictureNomeOrdemCerta.TabIndex = 4;
            this.pictureNomeOrdemCerta.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(138, 77);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(207, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pictureBoxEditaval
            // 
            this.pictureBoxEditaval.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxEditaval.Location = new System.Drawing.Point(139, 47);
            this.pictureBoxEditaval.Name = "pictureBoxEditaval";
            this.pictureBoxEditaval.Size = new System.Drawing.Size(96, 12);
            this.pictureBoxEditaval.TabIndex = 6;
            this.pictureBoxEditaval.TabStop = false;
            // 
            // comboCorFonte
            // 
            this.comboCorFonte.FormattingEnabled = true;
            this.comboCorFonte.Location = new System.Drawing.Point(138, 120);
            this.comboCorFonte.Name = "comboCorFonte";
            this.comboCorFonte.Size = new System.Drawing.Size(82, 21);
            this.comboCorFonte.TabIndex = 7;
            this.comboCorFonte.SelectedIndexChanged += new System.EventHandler(this.comboCorFonte_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Cor da Fonte:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Nome:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Prévia:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(353, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Original";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(226, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Fundo/Atributo:";
            // 
            // comboFundoAtributo
            // 
            this.comboFundoAtributo.FormattingEnabled = true;
            this.comboFundoAtributo.Location = new System.Drawing.Point(226, 120);
            this.comboFundoAtributo.Name = "comboFundoAtributo";
            this.comboFundoAtributo.Size = new System.Drawing.Size(119, 21);
            this.comboFundoAtributo.TabIndex = 14;
            this.comboFundoAtributo.SelectedIndexChanged += new System.EventHandler(this.comboFundoAtributo_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(138, 148);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Salvar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(296, 148);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(49, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = ">>";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(229, 148);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(52, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "<<";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // EditorDeNomeDeCartas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 292);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboFundoAtributo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboCorFonte);
            this.Controls.Add(this.pictureBoxEditaval);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureNomeOrdemCerta);
            this.Controls.Add(this.painelImgCarta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listaDeImagens);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EditorDeNomeDeCartas";
            this.Text = "Editor: Nome de Cartas Tag Force";
            ((System.ComponentModel.ISupportInitialize)(this.painelImgCarta)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNomeOrdemCerta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEditaval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listaDeImagens;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox painelImgCarta;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem preferênciasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selecionarPastaComPNGSToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureNomeOrdemCerta;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBoxEditaval;
        private System.Windows.Forms.ComboBox comboCorFonte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboFundoAtributo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}