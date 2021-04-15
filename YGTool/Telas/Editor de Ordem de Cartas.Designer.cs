namespace YGTool.Telas
{
    partial class Editor_de_Ordem_de_Cartas
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PonteiroColuna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeDaCartaColuna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdColuna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.ColunaCont = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColunaNomeIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColunaIDIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Abrir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PonteiroColuna,
            this.NomeDaCartaColuna,
            this.IdColuna});
            this.dataGridView1.Location = new System.Drawing.Point(12, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(547, 359);
            this.dataGridView1.TabIndex = 1;
            // 
            // PonteiroColuna
            // 
            this.PonteiroColuna.HeaderText = "Ponteiro";
            this.PonteiroColuna.Name = "PonteiroColuna";
            // 
            // NomeDaCartaColuna
            // 
            this.NomeDaCartaColuna.HeaderText = "Nome";
            this.NomeDaCartaColuna.Name = "NomeDaCartaColuna";
            this.NomeDaCartaColuna.Width = 300;
            // 
            // IdColuna
            // 
            this.IdColuna.HeaderText = "ID Card Sort";
            this.IdColuna.Name = "IdColuna";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColunaCont,
            this.ColunaNomeIn,
            this.ColunaIDIn});
            this.dataGridView2.Location = new System.Drawing.Point(577, 21);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(476, 359);
            this.dataGridView2.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(577, 405);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Gerar CardSort";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ColunaCont
            // 
            this.ColunaCont.HeaderText = "Cont";
            this.ColunaCont.Name = "ColunaCont";
            this.ColunaCont.ReadOnly = true;
            this.ColunaCont.Width = 50;
            // 
            // ColunaNomeIn
            // 
            this.ColunaNomeIn.HeaderText = "Nome";
            this.ColunaNomeIn.MinimumWidth = 25;
            this.ColunaNomeIn.Name = "ColunaNomeIn";
            this.ColunaNomeIn.ReadOnly = true;
            this.ColunaNomeIn.Width = 300;
            // 
            // ColunaIDIn
            // 
            this.ColunaIDIn.HeaderText = "IDIn";
            this.ColunaIDIn.Name = "ColunaIDIn";
            this.ColunaIDIn.ReadOnly = true;
            // 
            // Editor_de_Ordem_de_Cartas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "Editor_de_Ordem_de_Cartas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor de Ordem de Cartas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PonteiroColuna;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeDaCartaColuna;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdColuna;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColunaCont;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColunaNomeIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColunaIDIn;
    }
}