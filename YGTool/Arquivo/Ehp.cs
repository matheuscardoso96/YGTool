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
        public int Magico { get; set; }
        public int TamanhoEhp { get; set; }
        public byte[] Magico2 { get; set; }
        public int QuantidadeDeArquivos { get; set; }
        public string DiretorioArquivo { get; set; }
        public string NomeDoArquivo { get; set; }

        public Ehp()
        {

        }

        public Ehp(string diretorioArquivo, string nomeDoArquivo)
        {
            DiretorioArquivo = diretorioArquivo;
            NomeDoArquivo = nomeDoArquivo;

            string diretorioEhp = diretorioArquivo + "\\" + nomeDoArquivo + ".ehp";
            Stream ArquivoEhp = new MemoryStream(File.ReadAllBytes(diretorioEhp));

            using (BinaryReader br = new BinaryReader(ArquivoEhp))
            {
                Magico = br.ReadInt32();
                TamanhoEhp = br.ReadInt32();
                Magico2 = br.ReadBytes(4);
                QuantidadeDeArquivos = br.ReadInt32();
            }

            if (Magico != 0x03504845)
            {
                throw new Exception("Este não é um arquivo ehp!");
            }
        }


        public bool ExportarArquivo()
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

            return true;
        }

        public bool ImportarArquivo()
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

            return true;
            
        }

        public bool ProcurarEHPNoBootBin(string dirBootBin)
        {
            Stream boot = new MemoryStream(File.ReadAllBytes(dirBootBin));
            int header = 0x03504845;
            List<string> ehpInfo = new List<string>();
                      

            using (BinaryReader br = new BinaryReader(boot))
            {
                int contador = 0;

                int verificaHeaderElf = br.ReadInt32();
                if (verificaHeaderElf != 0x464C457F)
                {
                    throw new Exception("Não é um arquivo Eboot válido ou está criptografado!");
                }
                string dirExtBoot = Path.GetFileName(dirBootBin.Replace(".BIN",""));
                Directory.CreateDirectory(dirExtBoot).Create();

                br.BaseStream.Position = 0;

                while (br.BaseStream.Position < boot.Length - 4)
                {
                    int valor = br.ReadInt32();

                    if (valor == header)
                    {
                        int indexInt = (int)br.BaseStream.Position - 4;
                        int tamanhoEhp = br.ReadInt32();
                        br.BaseStream.Position = indexInt;
                        byte[] ehpEmbyte = br.ReadBytes(tamanhoEhp);
                        string index = indexInt + "";
                        string nomeEhp = contador + "";
                        contador++;
                        File.WriteAllBytes(dirExtBoot + "\\" + nomeEhp + ".ehp", ehpEmbyte);
                        ehpInfo.Add(index + "," + dirExtBoot + "\\" + nomeEhp + ".ehp");
                        Ehp ehp = new Ehp(dirExtBoot, nomeEhp);
                        ehp.ExportarArquivo();
                    }
                }

                File.WriteAllLines(dirBootBin.ToUpper().Replace(".BIN",".txt"), ehpInfo);                
            }

            return true;
        }

        public bool InsiraNoEboot(string dirSndDat)
        {
            string[] arquivos = File.ReadAllLines(dirSndDat);

            using (BinaryWriter writer = new BinaryWriter(File.Open(dirSndDat.Replace(".txt", ".BIN"), FileMode.Open)))
            {

                for (int i = 0; i < arquivos.Length; i++)
                {
                    string info = arquivos[i];

                    if (info != "")
                    {
                        string[] fileInfo = info.Split(',');
                        int posicaoArquivo = int.Parse(fileInfo[0]);
                        string diretorio = fileInfo[1];
                        if (Path.GetFileName(diretorio) == "3.ehp" || Path.GetFileName(diretorio) == "4.ehp" || Path.GetFileName(diretorio) == "5.ehp")
                        {
                            InsiraEHPEspecial(diretorio);
                        }
                        byte[] arquivo = File.ReadAllBytes(diretorio);
                        writer.BaseStream.Seek(posicaoArquivo, SeekOrigin.Begin);
                        writer.Write(arquivo);

                    }
                }


            }

            return true;


        }

        private void InsiraEHPEspecial(string dir)
        {
            int ponteiroNome = 0;
            int ponteiroArquivo = 0;
            int idxTamanhoArquivo = 0;

            using (BinaryReader br = new BinaryReader(File.Open(dir,FileMode.Open)))
            {
                br.BaseStream.Position = 0x10;
                ponteiroNome = br.ReadInt32();
                ponteiroArquivo = br.ReadInt32();

                br.BaseStream.Position = ponteiroNome;
                byte valor = br.ReadByte();
                while (valor != 0)
                {
                    valor = br.ReadByte();
                }

                idxTamanhoArquivo = (int)br.BaseStream.Position;
            }

            string[] arquivos = Directory.GetFiles(dir.Replace(".ehp",""),"*.bin");
            byte[] arquivoIngles = File.ReadAllBytes(arquivos[0]);

            using (BinaryWriter bw = new BinaryWriter(File.Open(dir,FileMode.Open)))
            {
                bw.BaseStream.Position = idxTamanhoArquivo;
                bw.Write((int)arquivoIngles.Length);
                bw.BaseStream.Position = ponteiroArquivo;
                bw.Write(arquivoIngles);
            }
        }
    }
}
