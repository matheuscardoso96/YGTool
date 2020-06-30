using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGTool.Arquivo
{
    public class Ehp : IArquivos
    {
        public byte[] Magico { get; set; }
        public int TamanhoEhp { get; set; }
        public byte[] Magico2 { get; set; }
        public int QuantidadeDeArquivos { get; set; }
        public string DiretorioArquivo { get; set; }
        public string NomeDoArquivo { get; set; }

        public Ehp(string diretorioArquivo, string nomeDoArquivo)
        {
            DiretorioArquivo = diretorioArquivo;
            NomeDoArquivo = nomeDoArquivo;

            string diretorioEhp = diretorioArquivo + "\\" + nomeDoArquivo + ".ehp";
            Stream ArquivoEhp = new MemoryStream(File.ReadAllBytes(diretorioEhp));

            using (BinaryReader br = new BinaryReader(ArquivoEhp))
            {
                Magico = br.ReadBytes(4);
                TamanhoEhp = br.ReadInt32();
                Magico2 = br.ReadBytes(4);
                QuantidadeDeArquivos = br.ReadInt32();
            }
        }


        public void ExportarArquivo()
        {
            string diretorioEhp = DiretorioArquivo + "\\" + NomeDoArquivo + ".ehp";
            Stream st = new MemoryStream(File.ReadAllBytes(diretorioEhp));

            int posicaoLeituraTabela = 16;
            int ponteiroNomeArquivo = 0;
            int ponteiroArquivo = 0;
            string nomeArquivo = string.Empty;
            int tamanhoArquivo = 0;
   
            Directory.CreateDirectory(DiretorioArquivo + "\\" + NomeDoArquivo + "\\");

            using (BinaryReader br = new BinaryReader(st))
            {
              
                for (int i = 0; i < QuantidadeDeArquivos; i++)
                {
                    nomeArquivo = string.Empty;

                    br.BaseStream.Seek(posicaoLeituraTabela, SeekOrigin.Begin);
                    ponteiroNomeArquivo = br.ReadInt32();
                    ponteiroArquivo = br.ReadInt32();
                    posicaoLeituraTabela += 8;

                    br.BaseStream.Seek(ponteiroNomeArquivo, SeekOrigin.Begin);

                    byte b = br.ReadByte();
                    nomeArquivo += Convert.ToChar(b);

                    while (b != 0)
                    {
                        b = br.ReadByte();
                        if (b != 0)
                        {
                            nomeArquivo += Convert.ToChar(b);
                        }


                    }

                    br.BaseStream.Seek(br.BaseStream.Position, SeekOrigin.Begin);

                    tamanhoArquivo = br.ReadInt32();
                    br.BaseStream.Seek(ponteiroArquivo, SeekOrigin.Begin);
                    byte[] arquivo = br.ReadBytes(tamanhoArquivo);
                    string diretorioFinal = DiretorioArquivo + "\\" + NomeDoArquivo + "\\" + nomeArquivo;
                    File.WriteAllBytes(diretorioFinal, arquivo);

                }
            }
        }

        public void ImportarArquivo()
        {
            string diretorioEhp = DiretorioArquivo + "\\" + NomeDoArquivo + ".ehp";
            byte[] arquivo = File.ReadAllBytes(diretorioEhp);  
            Stream ehpOriginal = new MemoryStream(arquivo);
            Array.Resize(ref arquivo, arquivo.Length * 2);
            Stream ehpCopia = new MemoryStream(arquivo);
            MemoryStream ehpEditado = new MemoryStream(arquivo);

            int posicaoLeituraTabela = 16;
            int ponteiroNomeArquivo = 0;
            int ponteiroArquivo = 0;
            string nomeArquivo = string.Empty;
            int posicaoPonteiroArquivo = 0;
            int posicaoTamanhoArquivo = 0;
            int ponteiroAtualizado = 0;


            using (BinaryReader leitor = new BinaryReader(ehpOriginal))
            {
                using (BinaryWriter escritor = new BinaryWriter(ehpCopia))
                {
                    for (int i = 0; i < QuantidadeDeArquivos; i++)
                    {
                        nomeArquivo = string.Empty;

                        if (i == 0)
                        {
                            
                            leitor.BaseStream.Seek(posicaoLeituraTabela, SeekOrigin.Begin);
                            ponteiroNomeArquivo = leitor.ReadInt32();
                            posicaoPonteiroArquivo = (short)leitor.BaseStream.Position;
                            ponteiroArquivo = leitor.ReadInt32();
                            posicaoLeituraTabela += 8;
                            leitor.BaseStream.Seek(ponteiroNomeArquivo, SeekOrigin.Begin);

                            byte b = leitor.ReadByte();
                            nomeArquivo += Convert.ToChar(b);

                            while (b != 0)
                            {
                                b = leitor.ReadByte();
                                if (b != 0)
                                {
                                    nomeArquivo += Convert.ToChar(b);
                                }


                            }

                            posicaoTamanhoArquivo = (short)leitor.BaseStream.Position;                           
                            escritor.BaseStream.Seek(ponteiroArquivo, SeekOrigin.Begin);
                        }
                        else
                        {
                            leitor.BaseStream.Seek(posicaoLeituraTabela, SeekOrigin.Begin);
                            ponteiroNomeArquivo = leitor.ReadInt32();
                            escritor.Seek((int)leitor.BaseStream.Position, SeekOrigin.Begin);
                            escritor.Write(ponteiroAtualizado);
                            posicaoLeituraTabela += 8;
                            leitor.BaseStream.Seek(ponteiroNomeArquivo, SeekOrigin.Begin);
                            byte b = leitor.ReadByte();
                            nomeArquivo += Convert.ToChar(b);

                            while (b != 0)
                            {
                                b = leitor.ReadByte();
                                if (b != 0)
                                {
                                    nomeArquivo += Convert.ToChar(b);
                                }


                            }

                            posicaoTamanhoArquivo = (short)leitor.BaseStream.Position;
                            escritor.BaseStream.Seek(ponteiroAtualizado, SeekOrigin.Begin);
                        }
                                                                   

                        byte[] arquivoEditado = File.ReadAllBytes(DiretorioArquivo + "\\" + NomeDoArquivo + "\\" + nomeArquivo);
                        escritor.Write(arquivoEditado);
                        int posicaoUltimaEscrita = (int)escritor.BaseStream.Position;

                        byte padding = 0;

                        if (posicaoUltimaEscrita % 16 != 0)
                        {
                            while (posicaoUltimaEscrita % 16 != 0)
                            {
                                escritor.Write(padding);
                                posicaoUltimaEscrita++;
                            }
                        }

                        ponteiroAtualizado = (int)escritor.BaseStream.Position;
                        escritor.BaseStream.Seek(posicaoTamanhoArquivo, SeekOrigin.Begin);
                        escritor.Write((int)arquivoEditado.Length);
                        

                    }

                    escritor.BaseStream.Position = 0;
                    escritor.Write(Magico);
                    escritor.Write(ponteiroAtualizado);
                    escritor.BaseStream.Position = 0;
                    ehpCopia.CopyTo(ehpEditado);
                }
            }

            byte[] ehpEditadoEmbytes = new byte[ponteiroAtualizado];
            Array.Copy(ehpEditado.ToArray(),ehpEditadoEmbytes,ponteiroAtualizado);
            File.WriteAllBytes(diretorioEhp, ehpEditadoEmbytes);
            
        }

    }
}
