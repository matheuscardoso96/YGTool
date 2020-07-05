using GimSharp;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YGTool.Arquivo;
using YGTool.Compressao;
using YGTool.Texto;

namespace YGTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

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

                    if (commonOpenFile.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        string[] arquivos = Directory.GetFiles(commonOpenFile.FileName, "*.ehp");
                        

                        foreach (var diretorioArquivo in arquivos)
                        {
                            string nomeDoArquivo = Path.GetFileName(diretorioArquivo).Replace(".ehp", "");
                            string diretorio = Path.GetDirectoryName(diretorioArquivo).Replace(".ehp", "");
                            Ehp ehp = new Ehp(diretorio,nomeDoArquivo);
                            ehp.ExportarArquivo();
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
                        ehp.ImportarArquivo();
                        
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
                            ehp.ImportarArquivo();
                        }
                        

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void comprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "Arquivos gzip (*.gz)|*.gz|All files (*.*)|*.*";


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Gzip gz = new Gzip();
                    gz.Descomprimir(openFileDialog.FileName);
                }
            }
        }

        private void comprimirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "Arquivos (*.*)|*.*|All files (*.*)|*.*";


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Gzip gz = new Gzip();
                    gz.Comprimir(openFileDialog.FileName);
                }
            }
        }

        private void ajudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "YGTool por Matheus Abreu\n Github: https://github.com/matheuscardoso96/YGTool", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
