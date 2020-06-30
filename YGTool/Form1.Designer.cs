namespace YGTool
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagForceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eHPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarÚnicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarDePastaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compressãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gzipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comprimirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imagensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagForceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarÚnicoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarDePastaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.textoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagForceToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.importarÚnicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarEmLoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.poneirosInternosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ponteirosExternosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivosToolStripMenuItem,
            this.compressãoToolStripMenuItem,
            this.imagensToolStripMenuItem,
            this.textoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivosToolStripMenuItem
            // 
            this.arquivosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagForceToolStripMenuItem});
            this.arquivosToolStripMenuItem.Name = "arquivosToolStripMenuItem";
            this.arquivosToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.arquivosToolStripMenuItem.Text = "Arquivos";
            // 
            // tagForceToolStripMenuItem
            // 
            this.tagForceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eHPToolStripMenuItem});
            this.tagForceToolStripMenuItem.Name = "tagForceToolStripMenuItem";
            this.tagForceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tagForceToolStripMenuItem.Text = "Tag Force";
            // 
            // eHPToolStripMenuItem
            // 
            this.eHPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportarÚnicoToolStripMenuItem,
            this.exportarDePastaToolStripMenuItem,
            this.importarÚnicoToolStripMenuItem,
            this.importarEmLoteToolStripMenuItem});
            this.eHPToolStripMenuItem.Name = "eHPToolStripMenuItem";
            this.eHPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eHPToolStripMenuItem.Text = "EHP";
            // 
            // exportarÚnicoToolStripMenuItem
            // 
            this.exportarÚnicoToolStripMenuItem.Name = "exportarÚnicoToolStripMenuItem";
            this.exportarÚnicoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportarÚnicoToolStripMenuItem.Text = "Exportar único";
            this.exportarÚnicoToolStripMenuItem.Click += new System.EventHandler(this.exportarÚnicoToolStripMenuItem_Click);
            // 
            // exportarDePastaToolStripMenuItem
            // 
            this.exportarDePastaToolStripMenuItem.Name = "exportarDePastaToolStripMenuItem";
            this.exportarDePastaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportarDePastaToolStripMenuItem.Text = "Exportar de Em Lote";
            this.exportarDePastaToolStripMenuItem.Click += new System.EventHandler(this.exportarDePastaToolStripMenuItem_Click);
            // 
            // compressãoToolStripMenuItem
            // 
            this.compressãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gzipToolStripMenuItem});
            this.compressãoToolStripMenuItem.Name = "compressãoToolStripMenuItem";
            this.compressãoToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.compressãoToolStripMenuItem.Text = "Compressão";
            // 
            // gzipToolStripMenuItem
            // 
            this.gzipToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comprimirToolStripMenuItem,
            this.comprimirToolStripMenuItem1});
            this.gzipToolStripMenuItem.Name = "gzipToolStripMenuItem";
            this.gzipToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gzipToolStripMenuItem.Text = "Gzip";
            // 
            // comprimirToolStripMenuItem
            // 
            this.comprimirToolStripMenuItem.Name = "comprimirToolStripMenuItem";
            this.comprimirToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.comprimirToolStripMenuItem.Text = "Descomprimir Único";
            this.comprimirToolStripMenuItem.Click += new System.EventHandler(this.comprimirToolStripMenuItem_Click);
            // 
            // comprimirToolStripMenuItem1
            // 
            this.comprimirToolStripMenuItem1.Name = "comprimirToolStripMenuItem1";
            this.comprimirToolStripMenuItem1.Size = new System.Drawing.Size(183, 22);
            this.comprimirToolStripMenuItem1.Text = "Comprimir";
            this.comprimirToolStripMenuItem1.Click += new System.EventHandler(this.comprimirToolStripMenuItem1_Click);
            // 
            // imagensToolStripMenuItem
            // 
            this.imagensToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagForceToolStripMenuItem1});
            this.imagensToolStripMenuItem.Name = "imagensToolStripMenuItem";
            this.imagensToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.imagensToolStripMenuItem.Text = "Imagens";
            // 
            // tagForceToolStripMenuItem1
            // 
            this.tagForceToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gimToolStripMenuItem});
            this.tagForceToolStripMenuItem1.Name = "tagForceToolStripMenuItem1";
            this.tagForceToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.tagForceToolStripMenuItem1.Text = "Tag Force";
            // 
            // gimToolStripMenuItem
            // 
            this.gimToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportarÚnicoToolStripMenuItem1,
            this.exportarDePastaToolStripMenuItem1});
            this.gimToolStripMenuItem.Name = "gimToolStripMenuItem";
            this.gimToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.gimToolStripMenuItem.Text = ".Gim";
            // 
            // exportarÚnicoToolStripMenuItem1
            // 
            this.exportarÚnicoToolStripMenuItem1.Name = "exportarÚnicoToolStripMenuItem1";
            this.exportarÚnicoToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.exportarÚnicoToolStripMenuItem1.Text = "Exportar único";
            // 
            // exportarDePastaToolStripMenuItem1
            // 
            this.exportarDePastaToolStripMenuItem1.Name = "exportarDePastaToolStripMenuItem1";
            this.exportarDePastaToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.exportarDePastaToolStripMenuItem1.Text = "Exportar de pasta";
            // 
            // textoToolStripMenuItem
            // 
            this.textoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagForceToolStripMenuItem2});
            this.textoToolStripMenuItem.Name = "textoToolStripMenuItem";
            this.textoToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.textoToolStripMenuItem.Text = "Textos";
            // 
            // tagForceToolStripMenuItem2
            // 
            this.tagForceToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extrairToolStripMenuItem});
            this.tagForceToolStripMenuItem2.Name = "tagForceToolStripMenuItem2";
            this.tagForceToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.tagForceToolStripMenuItem2.Text = "Tag Force";
            // 
            // importarÚnicoToolStripMenuItem
            // 
            this.importarÚnicoToolStripMenuItem.Name = "importarÚnicoToolStripMenuItem";
            this.importarÚnicoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importarÚnicoToolStripMenuItem.Text = "Importar Único";
            this.importarÚnicoToolStripMenuItem.Click += new System.EventHandler(this.importarÚnicoToolStripMenuItem_Click);
            // 
            // importarEmLoteToolStripMenuItem
            // 
            this.importarEmLoteToolStripMenuItem.Name = "importarEmLoteToolStripMenuItem";
            this.importarEmLoteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importarEmLoteToolStripMenuItem.Text = "Importar Em Lote";
            this.importarEmLoteToolStripMenuItem.Click += new System.EventHandler(this.importarEmLoteToolStripMenuItem_Click);
            // 
            // extrairToolStripMenuItem
            // 
            this.extrairToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.poneirosInternosToolStripMenuItem,
            this.ponteirosExternosToolStripMenuItem});
            this.extrairToolStripMenuItem.Name = "extrairToolStripMenuItem";
            this.extrairToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.extrairToolStripMenuItem.Text = "Extrair Textos";
            // 
            // poneirosInternosToolStripMenuItem
            // 
            this.poneirosInternosToolStripMenuItem.Name = "poneirosInternosToolStripMenuItem";
            this.poneirosInternosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.poneirosInternosToolStripMenuItem.Text = "Ponteiros Internos";
            this.poneirosInternosToolStripMenuItem.Click += new System.EventHandler(this.poneirosInternosToolStripMenuItem_Click);
            // 
            // ponteirosExternosToolStripMenuItem
            // 
            this.ponteirosExternosToolStripMenuItem.Name = "ponteirosExternosToolStripMenuItem";
            this.ponteirosExternosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ponteirosExternosToolStripMenuItem.Text = "Ponteiros Externos";
            this.ponteirosExternosToolStripMenuItem.Click += new System.EventHandler(this.ponteirosExternosToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagForceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eHPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarÚnicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarDePastaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compressãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gzipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comprimirToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem imagensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagForceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarÚnicoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem textoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarDePastaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tagForceToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem importarÚnicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importarEmLoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem poneirosInternosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ponteirosExternosToolStripMenuItem;
    }
}

