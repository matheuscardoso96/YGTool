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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivosTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.tagForceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eHPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarÚnicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarDePastaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compressãoTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.gzipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comprimirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imagensTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.tagForceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarÚnicoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarDePastaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.textoTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.tagForceToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.importarÚnicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarEmLoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.poneirosInternosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.únicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emLoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ponteirosInternosDiretosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.únicoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loteeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ponteirosExternosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.únicoToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cardInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.únicoToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.dLGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarTextosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivosTSM,
            this.compressãoTSM,
            this.imagensTSM,
            this.textoTSM,
            this.ajudaTSM});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(344, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivosTSM
            // 
            this.arquivosTSM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagForceToolStripMenuItem});
            this.arquivosTSM.Name = "arquivosTSM";
            this.arquivosTSM.Size = new System.Drawing.Size(66, 20);
            this.arquivosTSM.Text = "Arquivos";
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
            // compressãoTSM
            // 
            this.compressãoTSM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gzipToolStripMenuItem});
            this.compressãoTSM.Name = "compressãoTSM";
            this.compressãoTSM.Size = new System.Drawing.Size(85, 20);
            this.compressãoTSM.Text = "Compressão";
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
            // imagensTSM
            // 
            this.imagensTSM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagForceToolStripMenuItem1});
            this.imagensTSM.Name = "imagensTSM";
            this.imagensTSM.Size = new System.Drawing.Size(64, 20);
            this.imagensTSM.Text = "Imagens";
            // 
            // tagForceToolStripMenuItem1
            // 
            this.tagForceToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gimToolStripMenuItem});
            this.tagForceToolStripMenuItem1.Name = "tagForceToolStripMenuItem1";
            this.tagForceToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.tagForceToolStripMenuItem1.Text = "Tag Force";
            // 
            // gimToolStripMenuItem
            // 
            this.gimToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportarÚnicoToolStripMenuItem1,
            this.exportarDePastaToolStripMenuItem1});
            this.gimToolStripMenuItem.Name = "gimToolStripMenuItem";
            this.gimToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
            // textoTSM
            // 
            this.textoTSM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagForceToolStripMenuItem2});
            this.textoTSM.Name = "textoTSM";
            this.textoTSM.Size = new System.Drawing.Size(52, 20);
            this.textoTSM.Text = "Textos";
            // 
            // tagForceToolStripMenuItem2
            // 
            this.tagForceToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extrairToolStripMenuItem,
            this.importarTextosToolStripMenuItem});
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
            this.ponteirosInternosDiretosToolStripMenuItem,
            this.ponteirosExternosToolStripMenuItem1,
            this.cardInfoToolStripMenuItem});
            this.extrairToolStripMenuItem.Name = "extrairToolStripMenuItem";
            this.extrairToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.extrairToolStripMenuItem.Text = "Extrair Textos";
            // 
            // poneirosInternosToolStripMenuItem
            // 
            this.poneirosInternosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.únicoToolStripMenuItem,
            this.emLoteToolStripMenuItem});
            this.poneirosInternosToolStripMenuItem.Name = "poneirosInternosToolStripMenuItem";
            this.poneirosInternosToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.poneirosInternosToolStripMenuItem.Text = "Ponteiros Internos Indiretos";
            // 
            // ajudaTSM
            // 
            this.ajudaTSM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajudaToolStripMenuItem});
            this.ajudaTSM.Name = "ajudaTSM";
            this.ajudaTSM.Size = new System.Drawing.Size(50, 20);
            this.ajudaTSM.Text = "Ajuda";
            // 
            // ajudaToolStripMenuItem
            // 
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            this.ajudaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ajudaToolStripMenuItem.Text = "Sobre";
            this.ajudaToolStripMenuItem.Click += new System.EventHandler(this.ajudaToolStripMenuItem_Click);
            // 
            // únicoToolStripMenuItem
            // 
            this.únicoToolStripMenuItem.Name = "únicoToolStripMenuItem";
            this.únicoToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.únicoToolStripMenuItem.Text = "strtbl/ msg / res / stbl";
            this.únicoToolStripMenuItem.Click += new System.EventHandler(this.únicoToolStripMenuItem_Click);
            // 
            // emLoteToolStripMenuItem
            // 
            this.emLoteToolStripMenuItem.Name = "emLoteToolStripMenuItem";
            this.emLoteToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.emLoteToolStripMenuItem.Text = "De pasta / Vários";
            this.emLoteToolStripMenuItem.Click += new System.EventHandler(this.emLoteToolStripMenuItem_Click);
            // 
            // ponteirosInternosDiretosToolStripMenuItem
            // 
            this.ponteirosInternosDiretosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.únicoToolStripMenuItem1,
            this.loteeToolStripMenuItem});
            this.ponteirosInternosDiretosToolStripMenuItem.Name = "ponteirosInternosDiretosToolStripMenuItem";
            this.ponteirosInternosDiretosToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.ponteirosInternosDiretosToolStripMenuItem.Text = "Ponteiros Internos Diretos";
            // 
            // únicoToolStripMenuItem1
            // 
            this.únicoToolStripMenuItem1.Name = "únicoToolStripMenuItem1";
            this.únicoToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.únicoToolStripMenuItem1.Text = "tuto_text";
            this.únicoToolStripMenuItem1.Click += new System.EventHandler(this.únicoToolStripMenuItem1_Click);
            // 
            // loteeToolStripMenuItem
            // 
            this.loteeToolStripMenuItem.Name = "loteeToolStripMenuItem";
            this.loteeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loteeToolStripMenuItem.Text = "De Pasta / Vários";
            this.loteeToolStripMenuItem.Click += new System.EventHandler(this.loteeToolStripMenuItem_Click);
            // 
            // ponteirosExternosToolStripMenuItem1
            // 
            this.ponteirosExternosToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.únicoToolStripMenuItem2,
            this.dLGToolStripMenuItem});
            this.ponteirosExternosToolStripMenuItem1.Name = "ponteirosExternosToolStripMenuItem1";
            this.ponteirosExternosToolStripMenuItem1.Size = new System.Drawing.Size(219, 22);
            this.ponteirosExternosToolStripMenuItem1.Text = "Ponteiros Externos";
            // 
            // únicoToolStripMenuItem2
            // 
            this.únicoToolStripMenuItem2.Name = "únicoToolStripMenuItem2";
            this.únicoToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.únicoToolStripMenuItem2.Text = "Story_Scr";
            this.únicoToolStripMenuItem2.Click += new System.EventHandler(this.únicoToolStripMenuItem2_Click);
            // 
            // cardInfoToolStripMenuItem
            // 
            this.cardInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.únicoToolStripMenuItem3});
            this.cardInfoToolStripMenuItem.Name = "cardInfoToolStripMenuItem";
            this.cardInfoToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.cardInfoToolStripMenuItem.Text = "Card_Info";
            // 
            // únicoToolStripMenuItem3
            // 
            this.únicoToolStripMenuItem3.Name = "únicoToolStripMenuItem3";
            this.únicoToolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.únicoToolStripMenuItem3.Text = "Pasta com binários";
            this.únicoToolStripMenuItem3.Click += new System.EventHandler(this.únicoToolStripMenuItem3_Click);
            // 
            // dLGToolStripMenuItem
            // 
            this.dLGToolStripMenuItem.Name = "dLGToolStripMenuItem";
            this.dLGToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dLGToolStripMenuItem.Text = "DLG";
            this.dLGToolStripMenuItem.Click += new System.EventHandler(this.dLGToolStripMenuItem_Click);
            // 
            // importarTextosToolStripMenuItem
            // 
            this.importarTextosToolStripMenuItem.Name = "importarTextosToolStripMenuItem";
            this.importarTextosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importarTextosToolStripMenuItem.Text = "Importar Textos";
            this.importarTextosToolStripMenuItem.Click += new System.EventHandler(this.importarTextosToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 153);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(360, 192);
            this.MinimumSize = new System.Drawing.Size(360, 0);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YGTool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivosTSM;
        private System.Windows.Forms.ToolStripMenuItem tagForceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eHPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarÚnicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarDePastaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compressãoTSM;
        private System.Windows.Forms.ToolStripMenuItem gzipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comprimirToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem imagensTSM;
        private System.Windows.Forms.ToolStripMenuItem tagForceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarÚnicoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem textoTSM;
        private System.Windows.Forms.ToolStripMenuItem exportarDePastaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tagForceToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem importarÚnicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importarEmLoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem poneirosInternosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajudaTSM;
        private System.Windows.Forms.ToolStripMenuItem ajudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem únicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emLoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ponteirosInternosDiretosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem únicoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loteeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ponteirosExternosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem únicoToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem cardInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem únicoToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem dLGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importarTextosToolStripMenuItem;
    }
}

