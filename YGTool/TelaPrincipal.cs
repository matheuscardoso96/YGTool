using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using YGTool.Arquivo;
using YGTool.Compressao;
using YGTool.Imagem;
using YGTool.Som;
using YGTool.Texto;
using YGTool.Telas;
using System.Drawing;
using System.Drawing.Imaging;

namespace YGTool
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
            // 0x7002 0x1B11

        }

        private void exportarÚnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivos ehp (*.ehp)|*.ehp|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {                        
                        string nomeDoArquivo = Path.GetFileName(openFileDialog.FileName).Replace(".ehp", "");
                        string diretorio = Path.GetDirectoryName(openFileDialog.FileName).Replace(".ehp", "");
                        Ehp ehp = new Ehp(diretorio,nomeDoArquivo);
                        ehp.ExportarArquivo();
                        bool resultado = ehp.ImportarArquivo();
                        if (resultado)
                        {
                            Mensagem(nomeDoArquivo + ".ehp exportado com sucesso!\nDiretório: " + diretorio + "\\" + nomeDoArquivo, "Sucesso", MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void exportarDePastaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;
                    bool resultado = false;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        string[] arquivos = Directory.GetFiles(commonOpenFile.FileName, "*.ehp");
                        

                        foreach (var diretorioArquivo in arquivos)
                        {
                            string nomeDoArquivo = Path.GetFileName(diretorioArquivo).Replace(".ehp", "");
                            string diretorio = Path.GetDirectoryName(diretorioArquivo).Replace(".ehp", "");
                            Ehp ehp = new Ehp(diretorio,nomeDoArquivo);
                            resultado = ehp.ExportarArquivo();
                        }

                        if (resultado)
                        {                            
                            Mensagem("Ehps exportados com sucesso! ", "Sucesso", MessageBoxIcon.Information);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importarÚnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        string nomeDoArquivo = Path.GetFileName(commonOpenFile.FileName).Replace(".ehp", "");
                        string diretorio = Path.GetDirectoryName(commonOpenFile.FileName).Replace(".ehp", "");
                        Ehp ehp = new Ehp(diretorio, nomeDoArquivo);                   
                        bool resultado = ehp.ImportarArquivo();
                        if (resultado)
                        {
                            Mensagem(".ehp importadado com sucesso!", "Sucesso!", MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importarEmLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;
                    var ehpCompasta = new List<string>();

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        string[] arquivos = Directory.GetFiles(commonOpenFile.FileName, "*.ehp");
                        bool resultado = false;

                        for (int i = 0; i < arquivos.Length; i++)
                        {
                            if (Directory.Exists(arquivos[i].Replace(".ehp", "")))
                            {
                                ehpCompasta.Add(arquivos[i]);
                            }
                        }

                        foreach (var diretorioArquivo in ehpCompasta)
                        {
                            string nomeDoArquivo = Path.GetFileName(diretorioArquivo).Replace(".ehp", "");
                            string diretorio = Path.GetDirectoryName(diretorioArquivo).Replace(".ehp", "");
                            Ehp ehp = new Ehp(diretorio, nomeDoArquivo);
                            resultado = ehp.ImportarArquivo();
                        }

                        if (resultado)
                        {
                            Mensagem("Ehps importados com sucesso! ", "Sucesso!", MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        


        private void ajudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "YGTool v1.00 por Matheus Abreu\n Github: https://github.com/matheuscardoso96/YGTool", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void únicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivos bin (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        TagForceTextos tft = new TagForceTextos();
                        tft.ExportarParaTxtPonteirosInternosIndiretos(openFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void emLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string binarioAtual = string.Empty;

            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        TagForceTextos tft = new TagForceTextos();
                        
                        string[] arquivos = Directory.GetFiles(commonOpenFile.FileName, "*.bin");


                        foreach (var diretorioArquivo in arquivos)
                        {
                            binarioAtual = diretorioArquivo;
                            tft.ExportarParaTxtPonteirosInternosIndiretos(diretorioArquivo);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivo = Path.GetFileName(binarioAtual);
                MessageBox.Show(this, "Binario: \"" + nomeArquivo + "\" incompatível.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void únicoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivos bin (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        TagForceTextos tft = new TagForceTextos();
                        tft.ExportarParaTxtPonteirosInternosDiretos(openFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loteeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string binarioAtual = string.Empty;

            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        TagForceTextos tft = new TagForceTextos();

                        string[] arquivos = Directory.GetFiles(commonOpenFile.FileName, "*.bin");


                        foreach (var diretorioArquivo in arquivos)
                        {
                            binarioAtual = diretorioArquivo;
                            tft.ExportarParaTxtPonteirosInternosDiretos(diretorioArquivo);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string nomeArquivo = Path.GetFileName(binarioAtual);
                MessageBox.Show(this, "Binario: \"" + nomeArquivo + "\" incompatível.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void únicoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Selecione os arquivos na seguinte ordem com Ctrl:\n1º Binário da tabela de ponteiros\n2º Binário contendo os textos", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.Multiselect = true;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        List<string> arquivos = commonOpenFile.FileNames.ToList();
                        TagForceTextos tf = new TagForceTextos();
                        tf.ExportarParaTxtPonteirosExternos(arquivos[1],arquivos[0],4, 0, true);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void únicoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Selecione a pasta contendo os arquivos do CardInfo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        TagForceTextos tf = new TagForceTextos();
                        tf.ExportarCartasParaTxt(commonOpenFile.FileName);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dLGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Selecione os arquivos na seguinte ordem com Ctrl:\n1º Binário da tabela de ponteiros\n2º Binário contendo os textos", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.Multiselect = true;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        List<string> arquivos = commonOpenFile.FileNames.ToList();
                        TagForceTextos tf = new TagForceTextos();
                        tf.ExportarParaTxtPonteirosExternos(arquivos[1], arquivos[0], 4, 0, false);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importarTextosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivos txt (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {                       
                        TagForceTextos tf = new TagForceTextos();
                        tf.ImportarTexto(openFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void únicoToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivos gim (*.gim)|*.gim|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Gim gim = new Gim(openFileDialog.FileName);
                        Bitmap imagemE = gim.GimParaBmp();
                        imagemE.Save(openFileDialog.FileName.Replace(".gim",".png"), ImageFormat.Png);
                        Mensagem("Imagem exportada com sucesso!\nDiretório: " + openFileDialog.FileName.Replace(".gim",".png"), "Sucesso", MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void emLoteDePastaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        string[] arquivos = Directory.GetFiles(commonOpenFile.FileName, "*.gim");
                       

                        foreach (var diretorioArquivo in arquivos)
                        {
                            Gim gim = new Gim(diretorioArquivo);
                            Bitmap imagemE = gim.GimParaBmp();
                            imagemE.Save(diretorioArquivo.Replace(".gim", ".png"), ImageFormat.Png);
                            imagemE.Dispose();
                            gim = null;
                            
                           
                        }

                        Mensagem("Imagens exportadas com sucesso!" , "Sucesso!", MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void únicoToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Imagem(*.png, *.BMP)|*.png;*.BMP";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string nomeArquivo = openFileDialog.FileName.Replace(Path.GetExtension(openFileDialog.FileName), ".gim");
                        
                        if (File.Exists(nomeArquivo))
                        {
                            Gim gim = new Gim(nomeArquivo);
                            gim.BmpParaGim(openFileDialog.FileName);
                        }

                        Mensagem("Imagem exportada com sucesso!", "Sucesso", MessageBoxIcon.Information);

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void exportarAT3DeToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void previewTagForceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TagPreviewer tagPreviewer = new TagPreviewer();
            tagPreviewer.Show();
        }

        private void at3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo snd (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        At3 at = new At3();
                        at.ProcurarAt3(openFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void exportarGIMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo cip (*.cip)|*.cip|Todos os Arquivos (*.*)|*.*";

                    int totalEncontrado = 0;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        labelGimComv.Text = "Scaneando e exportando...";
                        labelGimComv.Visible = true;
                        Cip cip = new Cip();
                        totalEncontrado  = await cip.ProcurarGim(openFileDialog.FileName);
                        MessageBox.Show(this, "Concluído! Foram encontrados: " + totalEncontrado + " gims.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        labelGimComv.Visible = false;

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        

        private void exportarGIMToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void importarGimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo cip (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        labelGimComv.Text = "Importando...";
                        labelGimComv.Visible = true;
                        Cip cip = new Cip();
                        cip.InsiraNoCip(openFileDialog.FileName);                       
                        labelGimComv.Visible = false;
                        Mensagem("Gims importados com sucesso ao card_h.cip!", "Sucesso", MessageBoxIcon.Information);
                    }

                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void emLoteDePastaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        var arquivos = Directory.GetFiles(commonOpenFile.FileName, "*.png").ToList();
                        arquivos.AddRange(Directory.GetFiles(commonOpenFile.FileName, "*.BMP"));


                        foreach (var diretorioArquivo in arquivos)
                        {
                            string nomeArquivo = diretorioArquivo.Replace(Path.GetExtension(diretorioArquivo), ".gim");

                            if (File.Exists(nomeArquivo))
                            {
                                Gim gim = new Gim(nomeArquivo);
                                gim.BmpParaGim(diretorioArquivo);
                            }

                        }

                        Mensagem("Imagens exportadas com sucesso!", "Sucesso!", MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ediToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorDeNomeDeCartas editor = new EditorDeNomeDeCartas();
            editor.Show();
        }


        private void importarEhpsNoEBOOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo eboot (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";
                    bool resultado = false;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        labelGimComv.Text = "Importando...";
                        labelGimComv.Visible = true;
                        Ehp ehp = new Ehp();
                        resultado = ehp.InsiraNoEboot(openFileDialog.FileName);
                        labelGimComv.Visible = false;

                    }

                   
                    if (resultado)
                    {
                        Mensagem("Importado com sucesso!", "Sucesso", MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportarEhpsNoEBBOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo boot (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";
                    bool resultado = false;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Ehp ehp = new Ehp();
                        resultado = ehp.ProcurarEHPNoBootBin(openFileDialog.FileName);
                    }

                    if (resultado)
                    {
                        Mensagem("ehps exportados com sucesso!", "Sucesso", MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void Mensagem(string mensagem, string label , MessageBoxIcon icone)
        {
            MessageBox.Show(this, mensagem, label, MessageBoxButtons.OK, icone);
        }


        private void únicoToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "Arquivos gzip (*.gz)|*.gz|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Gzip gz = new Gzip();
                    gz.Descomprimir(openFileDialog.FileName);
                    Mensagem("Descomprimido com sucesso!", "Sucesso!", MessageBoxIcon.Information);
                }
                
            }
        }

        private void únicoToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "Arquivos (*.*)|*.*|All files (*.*)|*.*";


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Gzip gz = new Gzip();
                    gz.Comprimir(openFileDialog.FileName);
                    Mensagem("Comprimido com sucesso!", "Sucesso!", MessageBoxIcon.Information);
                }
                
            }
        }

        private void emLoteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        string[] arquivos = Directory.GetFiles(commonOpenFile.FileName, "*.gzip");


                        foreach (var diretorioArquivo in arquivos)
                        {
                            Gzip gz = new Gzip();
                            gz.Descomprimir(diretorioArquivo);
                        }

                        Mensagem("Gzips descomprimidos com sucesso! ", "Sucesso", MessageBoxIcon.Information);
                        
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void emLoteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        string[] arquivos = Directory.GetFiles(commonOpenFile.FileName, "*.gzip");


                        foreach (var diretorioArquivo in arquivos)
                        {
                            Gzip gz = new Gzip();
                            gz.Comprimir(diretorioArquivo);
                        }

                        Mensagem("Gzips comprimidos com sucesso!", "Sucesso", MessageBoxIcon.Information);

                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void unicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo Sound Package (*.sp)|*.sp|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        SP sp = new SP();
                        sp.ExporteAudiosDeDentroDoPacote(openFileDialog.FileName);
                        Mensagem("SPS exportados com sucesso!", "Sucesso", MessageBoxIcon.Information);

                    }

                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;
                    bool resultado = false;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        string[] arquivos = Directory.GetFiles(commonOpenFile.FileName, "*.sp");


                        foreach (var diretorioArquivo in arquivos)
                        {                          
                            SP sp = new SP();
                            sp.ExporteAudiosDeDentroDoPacote(diretorioArquivo);
                            
                        }

                       
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importarPacotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            string dirsnd = "";
            string dirEboot = "";

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo snddat (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        dirsnd = openFileDialog.FileName;
                     
                        
                    }

                    
                }

                using (OpenFileDialog openFileDialog2 = new OpenFileDialog())
                {

                    openFileDialog2.Filter = "Arquivo eboot (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog2.ShowDialog() == DialogResult.OK)
                    {
                        dirEboot = openFileDialog2.FileName;
                        SndDat snd = new SndDat();
                        snd.RemontarSdat(dirsnd, dirEboot);
                        Mensagem("Sps importados com sucesso ao psp_snddat!", "Sucesso", MessageBoxIcon.Information);
                    }


                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExporteSdtDat(string dirSnd, string dirEboot, int tamanhoTabela, int posicaoTabela, int posTabelaTamanhoDePacotes)
        {
            SndDat sndDat = new SndDat();
            sndDat.ExportePacotesDeAudio(dirSnd, dirEboot, tamanhoTabela / 4, posicaoTabela,posTabelaTamanhoDePacotes);
        }

        private void packstrExplainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivos bin (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        TagForceTextos tft = new TagForceTextos();
                        tft.ExportarParaTxtPonteirosInternosIndiretosX2(openFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void europeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string dirSnd = "";
                string dirEboot = "";

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo psp_snddat (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        dirSnd = openFileDialog.FileName;

                        using (OpenFileDialog openFileDialog2 = new OpenFileDialog())
                        {

                            openFileDialog2.Filter = "Arquivo eboot (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                            if (openFileDialog2.ShowDialog() == DialogResult.OK)
                            {
                                dirEboot = openFileDialog2.FileName;
                                ExporteSdtDat(dirSnd, dirEboot, 2912, 0x98F74, 0x961FC);

                                Mensagem("Sps Exportados com sucesso ao psp_snddat!", "Sucesso", MessageBoxIcon.Information);
                            }


                        }

                    }
                }

                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void japanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string dirSnd = "";
                string dirEboot = "";

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo psp_snddat (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        dirSnd = openFileDialog.FileName;

                        using (OpenFileDialog openFileDialog2 = new OpenFileDialog())
                        {

                            openFileDialog2.Filter = "Arquivo eboot (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                            if (openFileDialog2.ShowDialog() == DialogResult.OK)
                            {
                                dirEboot = openFileDialog2.FileName;
                                ExporteSdtDat(dirSnd, dirEboot, 2916, 0x98F4C, 0x961C4);

                                Mensagem("Sps Exportados com sucesso ao psp_snddat!", "Sucesso", MessageBoxIcon.Information);
                            }


                        }

                    }
                }

               
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void sPRenamerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (CommonOpenFileDialog commonOpenFile = new CommonOpenFileDialog())
                {
                    commonOpenFile.Title = "Selecione uma pasta";
                    commonOpenFile.IsFolderPicker = true;

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        
                        SP sp = new SP();
                        sp.SpRenamer(commonOpenFile.FileName);
                       
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uSAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string dirSnd = "";
                string dirEboot = "";

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo psp_snddat (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        dirSnd = openFileDialog.FileName;

                        using (OpenFileDialog openFileDialog2 = new OpenFileDialog())
                        {

                            openFileDialog2.Filter = "Arquivo eboot (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                            if (openFileDialog2.ShowDialog() == DialogResult.OK)
                            {
                                dirEboot = openFileDialog2.FileName;
                                ExporteSdtDat(dirSnd, dirEboot, 0x7F90, 0xAFF9C, 0x90164);

                                Mensagem("Sps Exportados com sucesso ao psp_snddat!", "Sucesso", MessageBoxIcon.Information);
                            }


                        }

                    }
                }

                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void jpanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string dirSnd = "";
                string dirEboot = "";

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo psp_snddat (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        dirSnd = openFileDialog.FileName;

                        using (OpenFileDialog openFileDialog2 = new OpenFileDialog())
                        {

                            openFileDialog2.Filter = "Arquivo eboot (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                            if (openFileDialog2.ShowDialog() == DialogResult.OK)
                            {
                                dirEboot = openFileDialog2.FileName;
                                ExporteSdtDat(dirSnd, dirEboot, 0x7F90, 0xB061C, 0x907E4);

                                Mensagem("Sps Exportados com sucesso ao psp_snddat!", "Sucesso", MessageBoxIcon.Information);
                            }


                        }

                    }
                }

               
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void LbaRecalc(string lba)
        {
            List<string> lbasSt = File.ReadAllLines(lba).ToList();
            var lbas = new List<int>();
            int contador = 0;
            List<string> lbasStN = new List<string>();
            foreach (var item in lbasSt)
            {
                string[] info = item.Split(',');
                lbas.Add(int.Parse(info[0].Replace(" ","")));
                lbasStN.Add(info[1]);

            }

            List<int> diferencas = new List<int>();
            for (int i = 0; i < lbas.Count - 1; i++)
            {
                int v1 = lbas[i];
                int v2 = lbas[i + 1];

                diferencas.Add(v2 - v1);
            }

           

            diferencas = diferencas.Skip(1).Take(diferencas.Count - 1).ToList();
            lbas = lbas.Skip(2).Take(lbas.Count - 2).ToList();
            lbasStN = lbasStN.Skip(1).Take(lbasStN.Count - 1).ToList();
            List<string> novosLba = new List<string>();

            int LBABase = 665634;
            novosLba.Add(LBABase.ToString("0000000") + " ," + lbasStN[0]);
            lbasStN = lbasStN.Skip(1).Take(lbasStN.Count - 1).ToList();
            for (int i = 0; i < diferencas.Count - 1; i++)
            {
                LBABase += diferencas[i];
                novosLba.Add(LBABase.ToString("0000000") + " ," + lbasStN[i]);

                if (LBABase + diferencas[i + 1] < lbas[i + 1])
                {
                    break;
                }
            }

            File.WriteAllLines(lba.Replace(".txt","(novo).txt"), novosLba);
        }

        private void lBARecalcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog2 = new OpenFileDialog())
            {

                openFileDialog2.Filter = "Arquivo txt (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";


                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    LbaRecalc(openFileDialog2.FileName);
                }


            }
        }
    }
}
