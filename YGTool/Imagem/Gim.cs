using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGTool.Imagem
{
    public enum FormatoDaImagem
    {
        RGB565 = 0x00,
        //RGBA5551 =  0x01,
        //RGBA4444 =  0x02,
        RGBA8888 = 0x03,
        Index4 = 0x04,
        Index8 = 0x05,
        //Index16 =   0x06,
        //Index32 =   0x07
        DXT1 = 0x08

    }

    public class ImagemGim
    {
        public FormatoDaImagem FormatoDaImagem { get; set; }
        public short Largura { get; set; }
        public short Altura { get; set; }
        public bool Swizzle { get; set; }
        public int OffsetImg { get; set; }
        public byte[] Imagem { get; set; }
        public int TamanhoDoGrafico { get; set; }
        public bool EhFormatoComPaleta()
        {
            switch (FormatoDaImagem)
            {
                case FormatoDaImagem.Index4:
                    return true;
                case FormatoDaImagem.Index8:
                    return true;
                default:
                    return false;
            }
        }
    }

    public class PaletaGim
    {
        public FormatoDaImagem FormatoPaleta { get; set; }
        public byte[] Paleta { get; set; }
        public List<Color> PaletaDeCores { get; set; }

        public PaletaGim()
        {
            PaletaDeCores = new List<Color>();
        }

        public void AdicionarCor(Color cor)
        {
            PaletaDeCores.Add(cor);
        }
    }

    public class Gim
    {
        public string Diretorio { get; private set; }
        public ImagemGim ImagemGim { get; private set; }
        public PaletaGim PaletaGim { get; private set; }
        public bool TemPaleta { get; private set; } = false;
        private bool _leuPaleta = false;
        public byte[] GimBytes { get; private set; }



        public Gim(string diretorio)
        {

            Diretorio = diretorio;
            byte[] gim = File.ReadAllBytes(diretorio);
            GimBytes = gim;
            MemoryStream gimM = new MemoryStream(gim);

            using (BinaryReader br = new BinaryReader(gimM))
            {
                int valorBase = 0x10;
                short valor = 0;

                while (true)
                {
                    br.BaseStream.Seek(valorBase, SeekOrigin.Begin);
                    valor = br.ReadInt16();

                    if (valor == 0x04 || valor == 0x05)
                    {
                        if (valor == 0x04)
                        {
                            ImagemGim = new ImagemGim();
                            br.BaseStream.Position = valorBase + 0x4;
                            int tamanhoGrafico = br.ReadInt32();

                            br.BaseStream.Position = valorBase + 0xC;
                            valorBase = br.ReadInt32() + valorBase;
                            br.BaseStream.Position = valorBase + 4;

                            ImagemGim.FormatoDaImagem = (FormatoDaImagem)br.ReadInt16();
                            ImagemGim.Swizzle = br.ReadInt16() == 1 ? true : false;
                            ImagemGim.Largura = br.ReadInt16();
                            ImagemGim.Altura = br.ReadInt16();

                            br.BaseStream.Position = valorBase + 0x1C;
                            int offset = br.ReadInt32();
                            tamanhoGrafico -= offset + 0x10;
                            ImagemGim.TamanhoDoGrafico = tamanhoGrafico;
                            offset += valorBase;
                            ImagemGim.OffsetImg = offset;
                            ImagemGim.Imagem = ObtenhaImagem(ImagemGim.OffsetImg, tamanhoGrafico, gim);
                            valorBase = tamanhoGrafico + offset;
                            valor = 0;
                            TemPaleta = ImagemGim.EhFormatoComPaleta();

                            if (!TemPaleta)
                            {
                                break;
                            }
                        }

                        if (valor == 0x05)
                        {

                            PaletaGim = new PaletaGim();
                            br.BaseStream.Position = valorBase + 0x4;
                            int tamanhoGrafico = br.ReadInt32();
                            br.BaseStream.Position = valorBase + 0xC;
                            valorBase = br.ReadInt32() + valorBase;
                            br.BaseStream.Position = valorBase + 4;

                            PaletaGim.FormatoPaleta = (FormatoDaImagem)br.ReadInt16();

                            br.BaseStream.Position = valorBase + 0x1C;
                            int offset = br.ReadInt32();
                            tamanhoGrafico -= offset + 0x10;
                            offset += valorBase;
                            PaletaGim.Paleta = ObtenhaImagem(offset, tamanhoGrafico, gim);
                            valorBase = tamanhoGrafico + offset;
                            valor = 0;

                            _leuPaleta = true;

                        }

                        if (TemPaleta)
                        {
                            if (_leuPaleta)
                            {
                                break;
                            }
                        }

                    }


                    if (valor != 0)
                    {
                        br.BaseStream.Position = valorBase + 0xC;
                        valorBase = br.ReadInt32() + valorBase;
                        br.BaseStream.Position = valorBase;
                    }


                }



            }
        }

        public Bitmap GimParaBmp()
        {
            return ExporteFormato(ImagemGim.FormatoDaImagem, ImagemGim.Imagem);
        }

        public void BmpParaGim(string dirImg)
        {
            ImportarGim(dirImg);
        }

        private byte[] ObtenhaImagem(int offset, int tamanhoGrafico, byte[] gim)
        {
            byte[] img = null;

            using (BinaryReader br = new BinaryReader(new MemoryStream(gim)))
            {
                br.BaseStream.Position = offset;
                if (offset + tamanhoGrafico <= gim.Length)
                {
                    img = br.ReadBytes(tamanhoGrafico);
                }
                else
                {
                    byte[] imgP1 = br.ReadBytes(tamanhoGrafico - offset);
                    br.BaseStream.Position = offset;
                    byte[] imgP2 = br.ReadBytes(offset - 8);
                    img = new byte[tamanhoGrafico];
                    ImagemGim.TamanhoDoGrafico = imgP1.Length;
                    Array.Copy(imgP1, img, imgP1.Length);
                    Array.Copy(imgP2, 0, img, imgP1.Length + 8, imgP2.Length);


                }

            }

            return img;
        }

        #region Exportar
        private Bitmap ExporteFormato(FormatoDaImagem formato, byte[] img)
        {
            switch (formato)
            {
                case FormatoDaImagem.RGBA8888:
                    return ConvertaABRG8888(img);
                case FormatoDaImagem.Index4:
                    return ConvertaIndex4();
                case FormatoDaImagem.Index8:
                    return ConvertaIndex8();
                case FormatoDaImagem.DXT1:
                    return ConvertaDXT1(img);
                case FormatoDaImagem.RGB565:
                    return ConvertaBGR565(img);
                default:
                    return null;
            }
        }

        private Bitmap ConvertaBGR565(byte[] entrada)
        {
            using (BinaryReader br = new BinaryReader(new MemoryStream(entrada)))
            {

                while (br.BaseStream.Position < entrada.Length)
                {
                    uint corAbgr = br.ReadUInt16();
                   
                    uint b = ((corAbgr >> 0) & 0x1F) * 0xFF / 0x1F;
                    uint g = ((corAbgr >> 5) & 0x3F) * 0xFF / 0x3F;
                    uint r = ((corAbgr >> 11) & 0x1F) * 0xFF / 0x1F;

                    PaletaGim.AdicionarCor(Color.FromArgb((int)r, (int)g, (int)b));

                }

                return null;
            }
        }

        private Bitmap ConvertaDXT1(byte[] img)
        {
            Bitmap imagemFinal = new Bitmap(ImagemGim.Largura, ImagemGim.Altura);
 

            using (Graphics g = Graphics.FromImage(imagemFinal))
            {
                using (BinaryReader br = new BinaryReader(new MemoryStream(img)))
                {
                    for (int y1 = 0; y1 < ImagemGim.Altura; y1 += 4)
                    {
                        for (int x1 = 0; x1 < ImagemGim.Largura; x1 += 4)
                        {
                            uint informacaoBloco = br.ReadUInt32();
                            byte[] cores = br.ReadBytes(4);
                            PaletaGim = new PaletaGim();
                            ConvertaBGR565(cores);
                            PaletaGim.PaletaDeCores = InterpoleCores(PaletaGim.PaletaDeCores);
                            Bitmap texel = new Bitmap(4, 4);
                            int valor = 0;

                            for (int y = 0; y < 4; y++)
                            {
                                for (int x = 0; x < 4; x++)
                                {
                                    informacaoBloco = informacaoBloco >> valor;
                                    int pixelInfo = (int)informacaoBloco & 3;
                                    texel.SetPixel(x, y, PaletaGim.PaletaDeCores[pixelInfo]);
                                    valor = 2;
                                }
                            }

                            g.DrawImage(texel, x1, y1, texel.Width, texel.Height);
                            texel.Dispose();

                        }


                    }
                }
            }

            return imagemFinal;
        }

        private Bitmap ConvertaIndex4()
        {
            ExporteFormato(PaletaGim.FormatoPaleta, PaletaGim.Paleta);
            Bitmap imagemFinal = new Bitmap(ImagemGim.Largura, ImagemGim.Altura);
            if (ImagemGim.Swizzle)
            {
                ImagemGim.Imagem = UnSwizzle(ImagemGim.Imagem, 0, ImagemGim.Largura, ImagemGim.Altura, 4);
            }


            using (BinaryReader br = new BinaryReader(new MemoryStream(ImagemGim.Imagem)))
            {

                for (int y = 0; y < ImagemGim.Altura; y++)
                {

                    for (int x = 0; x < ImagemGim.Largura; x += 2)
                    {
                        byte v = br.ReadByte();

                        var nibbleAlto = (v & 0xF0) >> 4;
                        var nibbleBaixo = (v & 0x0F);
                        imagemFinal.SetPixel(x, y, PaletaGim.PaletaDeCores[nibbleBaixo]);
                        imagemFinal.SetPixel(x + 1, y, PaletaGim.PaletaDeCores[nibbleAlto]);

                    }
                }


            }

            return imagemFinal;
        }

        private Bitmap ConvertaIndex8()
        {

            ExporteFormato(PaletaGim.FormatoPaleta, PaletaGim.Paleta);
            Bitmap imagemFinal = new Bitmap(ImagemGim.Largura, ImagemGim.Altura);
            if (ImagemGim.Swizzle)
            {
                ImagemGim.Imagem = UnSwizzle(ImagemGim.Imagem, 0, ImagemGim.Largura, ImagemGim.Altura, 8);
            }

            using (BinaryReader br = new BinaryReader(new MemoryStream(ImagemGim.Imagem)))
            {

                for (int y = 0; y < ImagemGim.Altura; y++)
                {

                    for (int x = 0; x < ImagemGim.Largura; x++)
                    {
                        byte v = br.ReadByte();
                        imagemFinal.SetPixel(x, y, PaletaGim.PaletaDeCores[v]);
                    }
                }


            }

            return imagemFinal;
        }

        private Bitmap ConvertaABRG8888(byte[] paleta)
        {

            using (BinaryReader br = new BinaryReader(new MemoryStream(paleta)))
            {

                while (br.BaseStream.Position < paleta.Length)
                {
                    uint corAbgr = br.ReadUInt32();

                    uint a = corAbgr >> 24;
                    uint b = (corAbgr >> 16) & 0xFF;
                    uint g = (corAbgr >> 8) & 0xFF;
                    uint r = corAbgr & 0xFF;

                    PaletaGim.AdicionarCor(Color.FromArgb((int)a, (int)r, (int)g, (int)b));

                }
            }

            return null;
        }
        #endregion

        #region Importar

        public void ImportarGim(string dirImg)
        {
            ImporteFormato(ImagemGim.FormatoDaImagem, dirImg);

        }

        private void ImporteFormato(FormatoDaImagem formato, string dirImg)
        {
            switch (formato)
            {
                case FormatoDaImagem.Index4:
                    ImporteIndex4(dirImg);
                    break;
                case FormatoDaImagem.Index8:
                    ImporteIndex8(dirImg);
                    break;
                case FormatoDaImagem.DXT1:
                    ImporteDxt1(dirImg);
                    break;
                default:
                    break;
            }
        }

        private void ImporteIndex4(string dirImg)
        {
            ExporteFormato(PaletaGim.FormatoPaleta, PaletaGim.Paleta);
            List<Color> coresImgEditada = ObtenhaCoresImagem(dirImg);
            byte[] imgEditada = new byte[coresImgEditada.Count / 2];
            int contador = 0;
            for (int i = 0; i < coresImgEditada.Count; i += 2)
            {
                int valor1 = ObtenhaIndexCorMaisProxima(coresImgEditada[i], PaletaGim.PaletaDeCores);
                int valor2 = ObtenhaIndexCorMaisProxima(coresImgEditada[i + 1], PaletaGim.PaletaDeCores) << 4;
                valor2 = valor2 + valor1;
                byte valorFinal = (byte)valor2;
                imgEditada[contador] = valorFinal;
                contador++;

            }

            if (ImagemGim.Swizzle)
            {
                imgEditada = Swizzle(imgEditada, 0, ImagemGim.Largura, ImagemGim.Altura, 8);
            }

            InsiraBytesNoGim(GimBytes, imgEditada);
            File.WriteAllBytes(Diretorio, GimBytes);

        }

        private void ImporteIndex8(string dirImg)
        {
            ExporteFormato(PaletaGim.FormatoPaleta, PaletaGim.Paleta);
            List<Color> coresImgEditada = ObtenhaCoresImagem(dirImg);
            byte[] imgEditada = new byte[coresImgEditada.Count];
            int contador = 0;
            for (int i = 0; i < coresImgEditada.Count; i++)
            {
                int valorFinal = ObtenhaIndexCorMaisProxima(coresImgEditada[i], PaletaGim.PaletaDeCores);
                imgEditada[contador] = (byte)valorFinal;
                contador++;

            }

            if (ImagemGim.Swizzle)
            {
                imgEditada = Swizzle(imgEditada, 0, ImagemGim.Largura, ImagemGim.Altura, 8);
            }

            InsiraBytesNoGim(GimBytes, imgEditada);
            File.WriteAllBytes(Diretorio, GimBytes);

        }

        private void ImporteDxt1(string dirImg)
        {

            List<Bitmap> texels = new List<Bitmap>();
            texels = ObtenhaTexels(dirImg);

            MemoryStream imgEditada = new MemoryStream(ImagemGim.TamanhoDoGrafico);

            using (BinaryWriter bw = new BinaryWriter(imgEditada))
            {
                

                foreach (var texel in texels)
                {


                    Dictionary<Color, int> coresPaletaFrq = ObtenhaCoresTexels(texel);
                    List<Color> coresPaleta = ObtenhaCorMinimaEMaxima(coresPaletaFrq);
                    coresPaleta = InterpoleCores(coresPaleta);

                    int contador = 0;
                    int pixelInfo = 0;

                    for (int y = 0; y < 4; y++)
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            Color pixelColor = texel.GetPixel(x, y);
                            int valorFinal = ObtenhaIndexCorMaisProxima(pixelColor, coresPaleta);
                            pixelInfo |= valorFinal << contador;
                            contador += 2;
                        }


                    }

                    bw.Write(pixelInfo);
                    ushort c0 = RGB888ParaRGB565(coresPaleta[0]);
                    ushort c1 = RGB888ParaRGB565(coresPaleta[1]);
                    bw.Write(c0);
                    bw.Write(c1);


                    if (bw.BaseStream.Position == imgEditada.Capacity)
                    {
                        break;
                    }

                    texel.Dispose();
                    coresPaletaFrq.Clear();
                    coresPaleta.Clear();


                }

                
                texels.Clear();
                InsiraBytesNoGim(GimBytes, imgEditada.ToArray());
                File.WriteAllBytes(Diretorio, GimBytes);
            }

        }

        #endregion


        #region Auxiliares

        private List<Color> ObtenhaCorMinimaEMaxima(Dictionary<Color, int> cores)
        {

            //cores = cores.OrderByDescending(pair => pair.Value).Take(4)
              // .ToDictionary(pair => pair.Key, pair => pair.Value);

            Dictionary<Color, double> intensidade = new Dictionary<Color, double>();

            foreach (var item in cores)
            {
                intensidade.Add(item.Key, (0.2125 * item.Key.R) + (0.7154 * item.Key.G) + (0.0721 * item.Key.B));
            }



            intensidade = intensidade.OrderBy(pair => (int)pair.Value)
               .ToDictionary(pair => pair.Key, pair => pair.Value);

            List<Color> coress = new List<Color>();

            ushort c0 = RGB888ParaRGB565(intensidade.Last().Key);
            ushort c1 = RGB888ParaRGB565(intensidade.First().Key);

            if (c0 > c1)
            {
                coress.Add(intensidade.Last().Key);
                coress.Add(intensidade.First().Key);
            }
            else {
                
                coress.Add(intensidade.First().Key);
                coress.Add(intensidade.Last().Key);
            }

            

            return coress;
        }


        private ushort RGB888ParaRGB565(Color cor)
        {
            //ushort corBGR = (ushort)(((cor.R & 0b11111000) << 8) | ((cor.G & 0b11111100) << 3) | (cor.B >> 3));
            ushort corBGR = (ushort) (((cor.R >> 3) << 11) | ((cor.G >> 2) << 5) | (cor.B >> 3));
            return corBGR;
        }

        //Código de swizzle do GimShap, todos os créditos para o autor: https://github.com/nickworonekin/puyotools/blob/master/src/GimSharp/GimTexture/GimDataCodec.cs
        private static byte[] UnSwizzle(byte[] source, int offset, int width, int height, int bpp)
        {
            int destinationOffset = 0;

            width = (width * bpp) >> 3;

            byte[] destination = new byte[width * height];

            int rowblocks = (width / 16);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int blockX = x / 16;
                    int blockY = y / 8;

                    int blockIndex = blockX + ((blockY) * rowblocks);
                    int blockAddress = blockIndex * 16 * 8;

                    destination[destinationOffset] = source[offset + blockAddress + (x - blockX * 16) + ((y - blockY * 8) * 16)];
                    destinationOffset++;
                }
            }

            return destination;
        }

        private static byte[] Swizzle(byte[] source, int offset, int width, int height, int bpp)
        {
            width = (width * bpp) >> 3;

            byte[] destination = new byte[width * height];

            int rowblocks = (width / 16);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int blockX = x / 16;
                    int blockY = y / 8;

                    int blockIndex = blockX + ((blockY) * rowblocks);
                    int blockAddress = blockIndex * 16 * 8;

                    destination[blockAddress + (x - blockX * 16) + ((y - blockY * 8) * 16)] = source[offset];
                    offset++;
                }
            }

            return destination;
        }


        private List<Color> InterpoleCores(List<Color> cores)
        {
            cores.Add(Color.FromArgb(255,
               (((2 * cores[0].R) + cores[1].R) / 3),
               (((2 * cores[0].G) + cores[1].G) / 3),
               (((2 * cores[0].B) + cores[1].B) / 3)));

            cores.Add(Color.FromArgb(255,
               ((cores[0].R + (cores[1].R * 2)) / 3),
               ((cores[0].G + (cores[1].G * 2)) / 3),
               ((cores[0].B + (cores[1].B * 2)) / 3)));

            return cores;
        }

        private List<Bitmap> ObtenhaTexels(string dirImg)
        {
            Bitmap imagem = new Bitmap(dirImg);
            List<Bitmap> texels = new List<Bitmap>();

            for (int y = 0; y < imagem.Height; y += 4)
            {
                for (int x = 0; x < imagem.Width; x += 4)
                {
                    Rectangle rect = new Rectangle(x, y, 4, 4);
                    texels.Add(imagem.Clone(rect, imagem.PixelFormat));
                }
            }

            imagem.Dispose();
            return texels;
        }



        private byte[] InvertaFormatoPSP(byte[] image)
        {
            byte[] final = new byte[image.Length];
            MemoryStream mFinal = new MemoryStream(final);
            using (BinaryReader br = new BinaryReader(new MemoryStream(image)))
            {
                using (BinaryWriter bw = new BinaryWriter(mFinal))
                {
                    while (br.BaseStream.Position < image.Length)
                    {
                        byte[] valor1 = br.ReadBytes(4);
                        byte[] valor2 = br.ReadBytes(4);
                        bw.Write(valor2);
                        bw.Write(valor1);
                    }
                }

                final = mFinal.ToArray();
            }

            return final;
        }



        private Dictionary<Color, int> ObtenhaCoresTexels(Bitmap texel)
        {

            Bitmap imagem = new Bitmap(texel);

            Dictionary<Color, int> freqcores = new Dictionary<Color, int>();
            List<Color> cores = new List<Color>();

            for (int y = 0; y < imagem.Height; y++)
            {
                for (int x = 0; x < imagem.Width; x++)
                {
                    Color pixelColor = imagem.GetPixel(x, y);

                    if (!freqcores.ContainsKey(pixelColor))
                    {
                        freqcores.Add(pixelColor, 1);
                    }
                    else
                    {
                        int count = freqcores[pixelColor];
                        freqcores[pixelColor] = count + 1;
                    }


                }


            }

            imagem.Dispose();
            return freqcores;
        }

        private List<Color> ObtenhaCoresImagem(string dirImg)
        {

            Bitmap imagem = new Bitmap(dirImg);


            List<Color> cores = new List<Color>();

            for (int y = 0; y < imagem.Height; y++)
            {
                for (int x = 0; x < imagem.Width; x++)
                {
                    Color pixelColor = imagem.GetPixel(x, y);
                    cores.Add(pixelColor);
                }


            }


            return cores;
        }

        public int ObtenhaIndexCorMaisProxima(Color c1, List<Color> paleta)
        {
            int eOmenor = 10000;
            Color c2;
            Color corSimilar = Color.FromArgb(0, 0, 0, 0);

            for (int x = 0; x < paleta.Count; x++)
            {
                c2 = paleta[x];

                int vl = DiferencaDeCores(c1, c2);

                if (eOmenor > vl)
                {
                    eOmenor = vl;
                    corSimilar = paleta[x];
                }

            }

            return paleta.IndexOf(corSimilar);
        }

        private int DiferencaDeCores(Color c1, Color c2)
        {
            return (int)Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R) + (c1.G - c2.G) * (c1.G - c2.G) + (c1.B - c2.B) * (c1.B - c2.B) + (c1.A - c2.A) * (c1.A - c2.A));
        }

        public void InsiraBytesNoGim(byte[] bytesGim, byte[] imgEditata)
        {
            MemoryStream bgim = new MemoryStream(bytesGim);
            using (BinaryWriter bw = new BinaryWriter(bgim))
            {
                bw.BaseStream.Seek(ImagemGim.OffsetImg, SeekOrigin.Begin);
                bw.Write(imgEditata);
            }

        }

        #endregion
    }


}
