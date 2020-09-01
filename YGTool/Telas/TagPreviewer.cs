using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YGTool.Telas
{
    public partial class TagPreviewer : Form
    {
        public TagPreviewer()
        {
            InitializeComponent();
            

        }

        List<string> Cenas = new List<string>();
        List<string> scriptOriginal = new List<string>();
        List<string> scriptOEditado = new List<string>();
        List<Bitmap> Fonte = new List<Bitmap>();
        List<char> MapeamentoFonte = new List<char>();
        List<int> MapeamentoFonteVwf = new List<int>();
        List<int> Vwf = new List<int>();
        Bitmap CenaSelecionada;
        int indexListaSelecionado = 0;

        private void abrirtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "Arquivo txt (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        LerScript(openFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void LerScript(string fileName)
        {
            LimpeItems();
            LerMapeamentoDaFonte();
            CarregarFontes();
            PopuleListaDeCenas();
            PopuleListaDeDialogos(fileName);
            
            
        }

        private void PopuleListaDeDialogos(string fileName)
        {
            scriptOriginal = File.ReadAllText(fileName).Replace("\n", "").Replace("\r", "").Split(new[] { "<FIM/>" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var dialogo in scriptOriginal)
            {
                string[] split = dialogo.Split(new[] { "<P" }, StringSplitOptions.RemoveEmptyEntries);
                string[] split2 = split.Last().Split(new[] { ">" }, StringSplitOptions.RemoveEmptyEntries);
                listaDeDialogos.Items.Add("<P" + split2[0] + ">");
                scriptOEditado.Add(dialogo.Clone().ToString());
            }

            totalDePonteiros.Text = scriptOriginal.Count + "";
        }

        private void PopuleListaDeCenas()
        {
            cenaBox.Items.Add("Diálogo Tag Force 2");
            Cenas.Add("Cenas\\DLG_TAG_FORCE_2.png");
            cenaBox.SelectedIndex = 0;
            SelecioneCena(0);

        }

        private void SelecioneCena(int index)
        {
            Bitmap bitmap = new Bitmap(Cenas[index]);
            cenaPicture.Image = bitmap;
            CenaSelecionada = bitmap;
        }

        private void cenaBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cenaBox.SelectedIndex;
            SelecioneCena(index);

        }

        private void listaDeDialogos_SelectedValueChanged(object sender, EventArgs e)
        {
            indexListaSelecionado = listaDeDialogos.SelectedIndex;
            textoOriginal.Text = scriptOriginal[listaDeDialogos.SelectedIndex];
            textoParaEdicao.Text = scriptOEditado[listaDeDialogos.SelectedIndex];
            GerarNovaPrevia(textoParaEdicao.Text);
            
        }

        private void LimpeItems()
        {
            scriptOriginal = new List<string>();
            listaDeDialogos.Items.Clear();
            textoParaEdicao.Text = "";
            textoOriginal.Text = "";
            Fonte = new List<Bitmap>();
            MapeamentoFonte = new List<char>();
            Cenas = new List<string>();
            scriptOriginal = new List<string>();
            scriptOEditado = new List<string>();
            Fonte = new List<Bitmap>();
            MapeamentoFonteVwf = new List<int>();
            Vwf = new List<int>();
            indexListaSelecionado = 0;
           
        }

        private void CarregarFontes()
        {
            Bitmap fonte = new Bitmap("Fontes\\fonte_preta.png");

            for (int y = 0; y < 256; y += 32)
            {
                for (int x = 0; x < 512; x += 32)
                {
                    Rectangle rect = new Rectangle(x, y, 32, 32);                   
                    Fonte.Add(new Bitmap(fonte.Clone(rect, System.Drawing.Imaging.PixelFormat.Format32bppArgb)));
                }

            }


        }

        private void LerMapeamentoDaFonte()
        {
            string[] caracteres = File.ReadAllLines("Fontes\\Mapa_Da_Fonte.txt");

            foreach (string caractere in caracteres)
            {
                string[] informacao = caractere.Split('{');
                MapeamentoFonte.Add(Convert.ToChar(informacao[0]));
                MapeamentoFonteVwf.Add(int.Parse(informacao[1]));
            }

        }

        private void GerarNovaPrevia(string texto)
        {



            texto = texto.Replace("<b>", "\r\n");
            string[] textos = Regex.Replace(texto, "<.*?>", string.Empty).Replace("《PLAYER》", "(Jogador)").Replace("《BREAD》", "(PÃO)").Replace("《PARTNER》", "(PARCEIRO)").Replace("《", "(").Replace("》", ")").Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            Bitmap cenaSelecionada = new Bitmap(CenaSelecionada);
            Graphics g = Graphics.FromImage(cenaSelecionada);

            int y = 31;
            int x = 10;

            foreach (var item in textos)
            {
               
                for (int i = 0; i < item.Length; i++)
                {
                    if (x >= cenaSelecionada.Width)
                    {
                        break;
                    }

                    int posicaoCaractereNoMapa = MapeamentoFonte.IndexOf(item[i]);

                    if (posicaoCaractereNoMapa == -1)
                    {
                        posicaoCaractereNoMapa = 16;
                    }

                    Bitmap caractere = Fonte[posicaoCaractereNoMapa];
                    g.DrawImage(caractere, new Point(x, y));
                    x += MapeamentoFonteVwf[posicaoCaractereNoMapa];

                }

                cenaPicture.Image = cenaSelecionada;
                y += 20;
                x = 10;
            }

            
        }

        private void textoParaEdicao_TextChanged(object sender, EventArgs e)
        {
            scriptOEditado[indexListaSelecionado] = textoParaEdicao.Text;
            GerarNovaPrevia(textoParaEdicao.Text);
        }
    }
}
