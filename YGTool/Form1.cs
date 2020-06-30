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
            // List<string> arquivosNecessariosComprimir = ArquivosNecessarios("cardinfo_eng");
            // Huffman yugiohHuffman = new Huffman();
            // yugiohHuffman.Descomprimir(arquivosNecessariosComprimir[0], arquivosNecessariosComprimir[1], arquivosNecessariosComprimir[2]);
            //yugiohHuffman.Comprimir(arquivosNecessariosComprimir[0], arquivosNecessariosComprimir[1], arquivosNecessariosComprimir[2]);
            TagForceTextos tft = new TagForceTextos();
            tft.ExportarParaTxtPonteirosInternos("04_message_res.bin");
           

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

        private List<string> ArquivosNecessarios(string diretorio)
        {
            string[] arquivosParaComprimir = new string[] { "CARD_Desc", "CARD_Indx", "CARD_Huff" };
            string[] arquivosDaPasta = Directory.GetFiles(diretorio);
            List<string> arquivosVerificados = new List<string>();

            for (int i = 0; i < arquivosDaPasta.Length; i++)
            {
                for (int y = 0; y < arquivosParaComprimir.Length; y++)
                {
                    if (arquivosDaPasta[i].Contains(arquivosParaComprimir[y]))
                    {
                        arquivosVerificados.Add(arquivosDaPasta[i]);
                        break;
                    }
                }
                
            }

            return arquivosVerificados;
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

        private void poneirosInternosToolStripMenuItem_Click(object sender, EventArgs e)
        {          

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivos bin (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        TagForceTextos tft = new TagForceTextos();
                        tft.ExportarParaTxtPonteirosInternos(openFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void ponteirosExternosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
