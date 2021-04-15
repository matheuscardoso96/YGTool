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
using System.Text.RegularExpressions;

namespace YGTool
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
           /* string dic = File.ReadAllText(@"C:\Users\T-Gamer\source\repos\YGTool\YGTool\bin\Debug\__card_infos\tag_force_6\cardinfo_jpn\DICT_J.txt").Replace("<PONTEIRO", "~<PONTEIRO");
            string[] dicSplit = dic.Split('~');
            List<string> termos = new List<string>();
            for (int i = 0; i < dicSplit.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }

                if (dicSplit[i].Contains("$"))
                {
                    continue;
                }
                string termo = Regex.Match(dicSplit[i], "<TEXTO>(.*)<TEXTO§>").Value.Replace("<TEXTO>", "").Replace("<TEXTO§>", "").Replace("<NULL>","");
                termos.Add(termo);

            }
            string cardDesc = File.ReadAllText(@"C:\Users\T-Gamer\source\repos\YGTool\YGTool\bin\Debug\__card_infos\tag_force_6\cardinfo_jpn\CARD_Desc_J.txt");
            List<int> ocorrencias = new List<int>();
            int conta = 0;
            foreach (var item in termos)
            {
                
                int qtdValues = Regex.Matches(cardDesc,item).Count;
                ocorrencias.Add(qtdValues);
                conta++;
            }

            List<string> termosEOcorrencias = new List<string>();

            for (int i = 0; i < termos.Count; i++)
            {
                termosEOcorrencias.Add(termos[i] + "(" + ocorrencias[i] + ")");
            }

            File.WriteAllLines("_termos_oco.txt", termosEOcorrencias);*/
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
            string lastSelectedFolder = CarregarUltimoDiretorio();
            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {

                lastSelectedFolder = fbFolderBrowser.SelectedPath;

                string[] arquivos = Directory.GetFiles(lastSelectedFolder, "*.ehp");
                bool resultado = false;

                foreach (var diretorioArquivo in arquivos)
                {
                    string nomeDoArquivo = Path.GetFileName(diretorioArquivo).Replace(".ehp", "");
                    string diretorio = Path.GetDirectoryName(diretorioArquivo).Replace(".ehp", "");
                    Ehp ehp = new Ehp(diretorio, nomeDoArquivo);
                    resultado = ehp.ExportarArquivo();
                    
                }

                SalvarUltimoDiretorio(lastSelectedFolder);

                if (resultado)
                {
                    Mensagem("Ehps exportados com sucesso! ", "Sucesso", MessageBoxIcon.Information);
                }

            }

           
            
            
        }

        private void importarÚnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string lastSelectedFolder = CarregarUltimoDiretorio();
            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Save Last selected folder.
                lastSelectedFolder = fbFolderBrowser.SelectedPath;

                string nomeDoArquivo = Path.GetFileName(lastSelectedFolder).Replace(".ehp", "");
                string diretorio = Path.GetDirectoryName(lastSelectedFolder).Replace(".ehp", "");
                Ehp ehp = new Ehp(diretorio, nomeDoArquivo);
                bool resultado = ehp.ImportarArquivo();
                SalvarUltimoDiretorio(lastSelectedFolder);
                if (resultado)
                {
                    Mensagem(".ehp importadado com sucesso!", "Sucesso!", MessageBoxIcon.Information);
                }


            }

    

            


        }

        private void importarEmLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string lastSelectedFolder = CarregarUltimoDiretorio();
            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                var ehpCompasta = new List<string>();
                lastSelectedFolder = fbFolderBrowser.SelectedPath;
                string[] arquivos = Directory.GetFiles(lastSelectedFolder, "*.ehp");
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

                SalvarUltimoDiretorio(lastSelectedFolder);

                if (resultado)
                {
                    Mensagem("Ehps importados com sucesso! ", "Sucesso!", MessageBoxIcon.Information);
                }
               

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

            string lastSelectedFolder = CarregarUltimoDiretorio();
            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Save Last selected folder.
                lastSelectedFolder = fbFolderBrowser.SelectedPath;
                TagForceTextos tft = new TagForceTextos();

                string[] arquivos = Directory.GetFiles(lastSelectedFolder, "*.bin");


                foreach (var diretorioArquivo in arquivos)
                {
                    binarioAtual = diretorioArquivo;
                    tft.ExportarParaTxtPonteirosInternosIndiretos(diretorioArquivo);
                }

                SalvarUltimoDiretorio(lastSelectedFolder);

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
            string lastSelectedFolder = CarregarUltimoDiretorio();
            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Save Last selected folder.
                lastSelectedFolder = fbFolderBrowser.SelectedPath;
                TagForceTextos tft = new TagForceTextos();

                string[] arquivos = Directory.GetFiles(lastSelectedFolder, "*.bin");


                foreach (var diretorioArquivo in arquivos)
                {
                    binarioAtual = diretorioArquivo;
                    tft.ExportarParaTxtPonteirosInternosDiretos(diretorioArquivo);
                }
                SalvarUltimoDiretorio(lastSelectedFolder);

            }           
           

           
        }

        private void únicoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Selecione os arquivos na seguinte ordem com Ctrl:\n1º Binário da tabela de ponteiros\n2º Binário contendo os textos", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ExportarComPonteirosExternos(true);


        }

        


        private void únicoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Selecione a pasta contendo os arquivos do CardInfo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string lastSelectedFolder = CarregarUltimoDiretorio();
            ExportarCartasComDir(lastSelectedFolder, TagForce.TagForce2);


        }

        private void ExportarCartasComDir(string lastSelectedFolder, TagForce tagForce)
        {

            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Save Last selected folder.
                lastSelectedFolder = fbFolderBrowser.SelectedPath;
                ExportarCartas(lastSelectedFolder, tagForce);
                MessageBox.Show(this, "Exportação concluída.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportarCartas(string dir, TagForce tagForce)
        { 
            TagForceTextos tf = new TagForceTextos();
            tf.ExportarCartasParaTxt(dir, tagForce);
            SalvarUltimoDiretorio(dir);

        }

        private void SalvarUltimoDiretorio(string dir)
        {
            if (!File.Exists("ygtool.cfg"))
            {
                File.Create("ygtool.cfg").Close();
            }

            List<string> linhas = File.ReadAllLines("ygtool.cfg").ToList();
            bool existeLinha = false;
            int index = 0;
            for (int i = 0; i < linhas.Count; i++)
            {
                if (linhas[i].Contains("LastDir="))
                {
                    existeLinha = true;
                    index = i;
                    break;
                }
            }

            string dirModificar = "LastDir=" + dir;

            if (existeLinha)
            {               
                linhas[index] = dirModificar;

            }
            else
            {
                linhas.Add(dirModificar);
            }

            File.WriteAllLines("ygtool.cfg", linhas);
        }

        private string CarregarUltimoDiretorio()
        {
            if (!File.Exists("ygtool.cfg"))
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            }
            else
            {
                List<string> linhas = File.ReadAllLines("ygtool.cfg").ToList();

                for (int i = 0; i < linhas.Count; i++)
                {
                    if (linhas[i].Contains("LastDir="))
                    {
                        string dirR = linhas[i].Split('=')[1].Replace("\r", "").Replace("\n", "");
                        if (Directory.Exists(dirR))
                        {
                            return dirR;
                        }
                        else
                        {
                            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        }
                        
                    }
                }

                return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }

            
           
        }

        private void dLGToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show(this, "Selecione os arquivos na seguinte ordem com Ctrl:\n1º Binário da tabela de ponteiros\n2º Binário contendo os textos", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ExportarComPonteirosExternos(false);


        }

        private void ExportarComPonteirosExternos(bool mutiplica)
        {

            string tabelaPOnteiros = "";
            string dirBinario = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "Arquivo CardIdx (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    tabelaPOnteiros = openFileDialog.FileName;

                    using (OpenFileDialog openFileDialog2 = new OpenFileDialog())
                    {

                        openFileDialog2.Filter = "Arquivo CardName (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";

                        if (openFileDialog2.ShowDialog() == DialogResult.OK)
                        {
                            dirBinario = openFileDialog2.FileName;

                            TagForceTextos tf = new TagForceTextos();
                            tf.ExportarParaTxtPonteirosExternos(dirBinario, tabelaPOnteiros, 4, 0, mutiplica);


                        }
                    }
                }
            }
        }

        private void importarTextosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivos txt (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {                       
                        TagForceTextos tf = new TagForceTextos();
                        tf.ImportarTexto(openFileDialog.FileName, false);
                    }
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
            string lastSelectedFolder = CarregarUltimoDiretorio();
            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Save Last selected folder.
                lastSelectedFolder = fbFolderBrowser.SelectedPath;
                string[] arquivos = Directory.GetFiles(lastSelectedFolder, "*.gim");


                foreach (var diretorioArquivo in arquivos)
                {
                    Gim gim = new Gim(diretorioArquivo);
                    Bitmap imagemE = gim.GimParaBmp();
                    imagemE.Save(diretorioArquivo.Replace(".gim", ".png"), ImageFormat.Png);
                    imagemE.Dispose();
                    gim = null;


                }

                Mensagem("Imagens exportadas com sucesso!", "Sucesso!", MessageBoxIcon.Information);
                SalvarUltimoDiretorio(lastSelectedFolder);

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

                        Mensagem("Imagem importada com sucesso!", "Sucesso", MessageBoxIcon.Information);

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
            string lastSelectedFolder = CarregarUltimoDiretorio();
            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Save Last selected folder.
                lastSelectedFolder = fbFolderBrowser.SelectedPath;
                var arquivos = Directory.GetFiles(lastSelectedFolder, "*.png").ToList();
                arquivos.AddRange(Directory.GetFiles(lastSelectedFolder, "*.BMP"));


                foreach (var diretorioArquivo in arquivos)
                {
                    string nomeArquivo = diretorioArquivo.Replace(Path.GetExtension(diretorioArquivo), ".gim");

                    if (File.Exists(nomeArquivo))
                    {
                        Gim gim = new Gim(nomeArquivo);
                        gim.BmpParaGim(diretorioArquivo);

                    }

                }

                Mensagem("Imagens importadas com sucesso!", "Sucesso!", MessageBoxIcon.Information);
                SalvarUltimoDiretorio(lastSelectedFolder);

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
            string lastSelectedFolder = CarregarUltimoDiretorio();
            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Save Last selected folder.
                lastSelectedFolder = fbFolderBrowser.SelectedPath;
                string[] arquivos = Directory.GetFiles(lastSelectedFolder, "*.gzip");


                foreach (var diretorioArquivo in arquivos)
                {
                    Gzip gz = new Gzip();
                    gz.Descomprimir(diretorioArquivo);
                }

                Mensagem("Gzips descomprimidos com sucesso! ", "Sucesso", MessageBoxIcon.Information);
                SalvarUltimoDiretorio(lastSelectedFolder);

            }
           
           
        }

        private void emLoteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string lastSelectedFolder = CarregarUltimoDiretorio();
            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Save Last selected folder.
                lastSelectedFolder = fbFolderBrowser.SelectedPath;

                string[] arquivos = Directory.GetFiles(lastSelectedFolder, "*");


                foreach (var diretorioArquivo in arquivos)
                {
                    Gzip gz = new Gzip();
                    gz.Comprimir(diretorioArquivo);
                }

                Mensagem("Gzips comprimidos com sucesso!", "Sucesso", MessageBoxIcon.Information);
                SalvarUltimoDiretorio(lastSelectedFolder);

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

            string lastSelectedFolder = CarregarUltimoDiretorio();
            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Save Last selected folder.
                lastSelectedFolder = fbFolderBrowser.SelectedPath;
                string[] arquivos = Directory.GetFiles(lastSelectedFolder, "*.sp");


                foreach (var diretorioArquivo in arquivos)
                {
                    SP sp = new SP();
                    sp.ExporteAudiosDeDentroDoPacote(diretorioArquivo);

                }
                SalvarUltimoDiretorio(lastSelectedFolder);
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
            string lastSelectedFolder = CarregarUltimoDiretorio();
            FolderBrowserDialog fbFolderBrowser = new FolderBrowserDialog();
            fbFolderBrowser.SelectedPath = lastSelectedFolder;

            if (fbFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Save Last selected folder.
                lastSelectedFolder = fbFolderBrowser.SelectedPath;
                SP sp = new SP();
                sp.SpRenamer(lastSelectedFolder);
                SalvarUltimoDiretorio(lastSelectedFolder);

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

        private void cardSortEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor_de_Ordem_de_Cartas editor = new Editor_de_Ordem_de_Cartas();
            editor.Show();
        }

        private void pastaCardInfoTag4ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cardInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       

        private void pastaCardinfoTag4ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Selecione a pasta contendo os arquivos do CardInfo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string lastSelectedFolder = CarregarUltimoDiretorio();
            ExportarCartasComDir(lastSelectedFolder, TagForce.TagForce4);
        }

        private void pastaCardinfoTag6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Selecione a pasta contendo os arquivos do CardInfo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string lastSelectedFolder = CarregarUltimoDiretorio();
            ExportarCartasComDir(lastSelectedFolder, TagForce.TagForce6);
        }
    }
}
