using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;


namespace YGTool.Telas
{
    public partial class EditorDeNomeDeCartas : Form
    {
        public EditorDeNomeDeCartas()
        {
            InitializeComponent();
            VerificarRecursos();
            AdicionarItemsAComboBox();           
            CarregueListaDeFundos();
            PopuleFundoAtributo();
            comboFundoAtributo.SelectedIndex = 0;
            comboCorFonte.SelectedIndex = 0;
            if (File.Exists("ygtool.ini"))
            {
                CarregueIni();
            }

            
            

        }

      

        private void PopuleFundoAtributo()
        {
            foreach (var item in _listaDeFundos)
            {
                comboFundoAtributo.Items.Add(item.Split(' ')[0].Replace(".png",""));
            }
        }

        private List<string> ArquivosBMP = new List<string>();
        private Font _fonteNomeCarta = new Font("YuGiOh_Cartas", 9.0f);//
        private Bitmap _fundoSelecionado = null;
        private List<string> _listaDeFundos = new List<string>();

        private void selecionarPastaComPNGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog commonOpenFile = new FolderBrowserDialog())
                {
                   

                    if (commonOpenFile.ShowDialog() == DialogResult.OK)
                    {
                        Iniciar(commonOpenFile.SelectedPath);

                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Iniciar(string diretorio)
        {
            ArquivosBMP = Directory.GetFiles(diretorio, "*.png").ToList();
            AdicionarNaListBox();

            if (!(File.Exists("ygtool.ini")))
            {
                CrieIni(diretorio);
            }

        }


        private void CrieIni(string diretorio)
        {
            File.Create("ygtool.ini").Close();
            File.WriteAllText("ygtool.ini", "Diretorio_Graficos_Cartas=" + diretorio);
        }

        private void CarregueIni()
        {
            string[] argumentos = File.ReadAllLines("ygtool.ini");
            string arg = string.Empty;

            foreach (var argumento in argumentos)
            {
                if (argumento.Contains("Diretorio_Graficos_Carta"))
                {
                    arg = argumento.Split('=').Last();
                }
            }

            Iniciar(arg);
        }

        private void AdicionarNaListBox()
        {
            foreach (var item in ArquivosBMP)
            {
                listaDeImagens.Items.Add(Path.GetFileName(item));
            }
        }

        private void listaDeImagens_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listaDeImagens.SelectedIndex;
            Bitmap imgSelecionada = new Bitmap(ArquivosBMP[index]);
            painelImgCarta.Image = new Bitmap(imgSelecionada);           
            ObtenhaNomeDaCartaEOrganize(imgSelecionada);
            imgSelecionada.Dispose();
            //textBox1.Text = "";

        }

        private void ObtenhaNomeDaCartaEOrganize(Bitmap imgSelecionada)
        {

            Rectangle rect = new Rectangle(80,40,48,12);
            Bitmap parte1 = new Bitmap(imgSelecionada.Clone(rect,imgSelecionada.PixelFormat));
            rect = new Rectangle(80, 52, 48, 8);
            Bitmap parte2 = new Bitmap(imgSelecionada.Clone(rect, imgSelecionada.PixelFormat));
            rect = new Rectangle(0, 60, 48, 4);
            Bitmap parte3 = new Bitmap(imgSelecionada.Clone(rect, imgSelecionada.PixelFormat));
            Bitmap final = new Bitmap(96,12);

            Graphics g = Graphics.FromImage(final);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(parte1, 0,0, parte1.Width, parte1.Height);
            g.DrawImage(parte2, 48, 0, parte2.Width, parte2.Height);
            g.DrawImage(parte3, 48, 8, parte3.Width, parte3.Height);

            g.Dispose();
            parte1.Dispose();
            parte2.Dispose();
            parte3.Dispose();

            pictureNomeOrdemCerta.Image = new Bitmap(final);

            pictureBoxEditaval.Image = new Bitmap (_fundoSelecionado);


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            GerarPrevia(textBox1.Text);

        }

        private void GerarPrevia(string texto)
        {
            string[] selecionado = _listaDeFundos[comboFundoAtributo.SelectedIndex].Split(' ');
            Bitmap bmp = new Bitmap("fundos_NomeDeCartas\\" + selecionado[0]);

            if (texto != "")
            {

                Bitmap bmp1 = new Bitmap("fundos_NomeDeCartas\\" + selecionado[1]);

                RectangleF rectf = new RectangleF(0, 1, bmp1.Width, bmp1.Height);

                Graphics g = Graphics.FromImage(bmp1);

                string cor = comboCorFonte.SelectedItem.ToString();

                switch (cor)
                {
                    case "Branco":
                        g.DrawString(texto.ToUpper(), _fonteNomeCarta, Brushes.White, rectf);
                        break;
                    case "Preto":
                        g.DrawString(texto.ToUpper(), _fonteNomeCarta, Brushes.Black, rectf);
                        break;
                    case "Amarelo":
                        g.DrawString(texto.ToUpper(), _fonteNomeCarta, Brushes.Gold, rectf);
                        break;
                    default:
                        break;
                }



                Graphics gg = Graphics.FromImage(bmp1);
                SizeF sizeOfString = new SizeF();
                sizeOfString = gg.MeasureString(texto.ToUpper(), _fonteNomeCarta);
                Rectangle rect = new Rectangle(0, 0, (int)sizeOfString.Width + 3 , 12);

                if (rect.Width != 0 && rect.Height != 0 && rect.Width <= 256)
                {
                    Bitmap parte1 = bmp1.Clone(rect, bmp1.PixelFormat);

                    if (parte1.Width > 80)
                    {
                        parte1 = new Bitmap(ResizeImage(parte1, 78, 12));
                       
                    }

                    Graphics g2 = Graphics.FromImage(bmp);
                    g2.DrawImage(parte1, 0, 0, parte1.Width, parte1.Height);
                    parte1.Dispose();
                    g2.Dispose();
                    pictureBoxEditaval.Image = bmp;
                    
                }

                g.Dispose();
                gg.Dispose();
            }
            else
            {
                pictureBoxEditaval.Image = bmp;
            }

            
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }

                graphics.Dispose();
            }

            return destImage;
        }

        private static Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }



        private void AdicionarItemsAComboBox()
        {
            comboCorFonte.Items.Add("Branco");
            comboCorFonte.Items.Add("Preto");
            comboCorFonte.Items.Add("Amarelo");
        }

        private void comboCorFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
            GerarPrevia(textBox1.Text);
        }

        

        private void CarregueListaDeFundos()
        {
            _listaDeFundos = File.ReadAllLines("fundos_NomeDeCartas\\fundos.txt").ToList();
        }

        private void comboFundoAtributo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _fundoSelecionado = new Bitmap ("fundos_NomeDeCartas\\" + _listaDeFundos[comboFundoAtributo.SelectedIndex].Split(' ')[0]);
            pictureBoxEditaval.Image = new Bitmap(_fundoSelecionado);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listaDeImagens.SelectedIndex > 0)
            {
                listaDeImagens.SelectedIndex = listaDeImagens.SelectedIndex - 1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listaDeImagens.SelectedIndex < listaDeImagens.Items.Count -1)
            {
                listaDeImagens.SelectedIndex = listaDeImagens.SelectedIndex + 1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = listaDeImagens.SelectedIndex;
            string diretorio = ArquivosBMP[index];
            Bitmap imgEditada = new Bitmap(pictureBoxEditaval.Image);
            Rectangle rect = new Rectangle(0, 0, 48, 12);
            Bitmap parte1 = new Bitmap(imgEditada.Clone(rect, imgEditada.PixelFormat));
            rect = new Rectangle(48, 0, 48, 8);
            Bitmap parte2 = new Bitmap(imgEditada.Clone(rect, imgEditada.PixelFormat));
            rect = new Rectangle(48, 8, 48, 4);
            Bitmap parte3 = imgEditada.Clone(rect, imgEditada.PixelFormat);
            Bitmap final = new Bitmap(painelImgCarta.Image);

            Graphics g = Graphics.FromImage(final);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(parte1, 80, 40, parte1.Width, parte1.Height);
            g.DrawImage(parte2, 80, 52, parte2.Width, parte2.Height);
            g.DrawImage(parte3, 0, 60, parte3.Width, parte3.Height);

            g.Dispose();
            painelImgCarta.Image = new Bitmap(final);
            final.Save(diretorio);
        }


        private void VerificarRecursos()
        {
            bool existeFundos = Directory.Exists("fundos_NomeDeCartas");

            if (!existeFundos)
            {
                ExportarRecursos();
            }
        }

        private void ExportarRecursos()
        {
            byte[] arquivo = Properties.Resources.fundos_NomeDeCartas;
            File.WriteAllBytes("Fundos.zip", arquivo);
            ZipFile.ExtractToDirectory("Fundos.zip", Environment.CurrentDirectory);
            File.Delete("Fundos.zip");


        }
    }
}
