using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGTool.Compressao;

namespace YGTool.Texto
{
    public class TagForceTextos
    {
        public void ExportarParaTxtPonteirosInternos(string dirTextoBin)
        {
            int quantidaDePonteiros = 0;
            int tamanhoDoHeader = 0;
            int tamanhoDaTabela = 0;
            int posicaoTabela = 0;
            int ponteiro = 0;
            List<string> textos = new List<string>();
            int somaPonteiro = 0;

            Stream bin = new MemoryStream(File.ReadAllBytes(dirTextoBin));
            textos.Add("<Tipo de Ponteiro: Interno Indireto>\n\n");



            using (BinaryReader br = new BinaryReader(bin))
            {
                quantidaDePonteiros = br.ReadInt32();
                tamanhoDoHeader = br.ReadInt32();
                tamanhoDaTabela = br.ReadInt32();
                posicaoTabela = (int)br.BaseStream.Position;

                for (int i = 0; i < quantidaDePonteiros; i++)
                {


                    int tamanhoTexto = 0;
                    br.BaseStream.Seek(posicaoTabela, SeekOrigin.Begin);
                    ponteiro = br.ReadInt32();

                    if (ponteiro > bin.Length)
                    {
                        if (ponteiro > 0x10000000 && ponteiro < 0x1FFFFFFF)
                        {
                            ponteiro -= 0x10000000;
                            somaPonteiro = 0x10000000;
                        }
                        else if (ponteiro > 0x20000000 && ponteiro < 0x2FFFFFFF)
                        {
                            ponteiro -= 0x20000000;
                            somaPonteiro = 0x20000000;
                        }
                        else if (ponteiro > 0x30000000 && ponteiro < 0x3FFFFFFF)
                        {
                            ponteiro -= 0x30000000;
                            somaPonteiro = 0x30000000;
                        }
                        else if (ponteiro > 0x40000000 && ponteiro < 0x4FFFFFFF)
                        {
                            ponteiro -= 0x40000000;
                            somaPonteiro = 0x40000000;
                        }
                        else if (ponteiro > 0x50000000 && ponteiro < 0x5FFFFFFF)
                        {
                            ponteiro -= 0x50000000;
                            somaPonteiro = 0x50000000;
                        }
                        else
                        {
                            throw new Exception("Não compatível");
                        }
                    }

                    ponteiro += tamanhoDaTabela;

                    if (ponteiro == bin.Length)
                    {
                        break;
                    }



                    br.BaseStream.Seek(ponteiro, SeekOrigin.Begin);
                    byte valor = (byte)br.ReadInt16();

                    tamanhoTexto += 2;

                    while (valor != 0)
                    {
                        valor = (byte)br.ReadInt16();
                        tamanhoTexto += 2;
                    }

                    br.BaseStream.Seek(ponteiro, SeekOrigin.Begin);
                    byte[] textoEmUnicode = br.ReadBytes(tamanhoTexto - 2);

                    var textoEmString = Encoding.Unicode.GetString(textoEmUnicode);
                    textos.Add("<" + "PONTEIRO: " + posicaoTabela + "," + somaPonteiro + ">\n" + "<TEXTO>" + textoEmString.Replace("$CA", "<COR: $CA>").Replace("$C0", "<COR: $C0>").Replace("$C5", "<COR: $C5>").Replace("$C8", "<COR: $C8>").Replace("\n", "<b>\n").Replace("$0", "<JOGADOR: $0>") + "<TEXTO/>\n" + "<FIM/>\n\n");
                    posicaoTabela += 4;
                    somaPonteiro = 0;
                }


                File.WriteAllLines(dirTextoBin.Replace(".bin", ".txt"), textos);
            }

        }

        public void ExportarParaTxtPonteirosInternosDiretos(string dirTextoBin)
        {
            int quantidaDePonteiros = 0;
            int posicaoTabela = 0;
            int ponteiro = 0;
            List<string> textos = new List<string>();
            int somaPonteiro = 0;

            Stream bin = new MemoryStream(File.ReadAllBytes(dirTextoBin));



            using (BinaryReader br = new BinaryReader(bin))
            {
                quantidaDePonteiros = br.ReadInt32();
                quantidaDePonteiros = ((quantidaDePonteiros / 4) / 2);
                posicaoTabela = 0;
                int tamanhoTexto = 0;

                for (int i = 0; i < quantidaDePonteiros; i++)
                {

                    br.BaseStream.Seek(posicaoTabela, SeekOrigin.Begin);
                    ponteiro = br.ReadInt32();
                    tamanhoTexto = br.ReadInt32();

                    br.BaseStream.Seek(ponteiro, SeekOrigin.Begin);
                    byte[] textoEmUnicode = br.ReadBytes(tamanhoTexto);

                    var textoEmString = Encoding.Unicode.GetString(textoEmUnicode);
                    textos.Add("<" + "PONTEIRO: " + posicaoTabela + "," + somaPonteiro + ">\n" + "<TEXTO>" + textoEmString.Replace("\0", "").Replace("@2", "<Cor: @2>").Replace("@0", "<Cor: @0>").Replace("\n", "<b>\n") + "<TEXTO/>\n" + "<FIM/>\n\n");
                    posicaoTabela += 8;
                    somaPonteiro = 0;
                }


                File.WriteAllLines(dirTextoBin.Replace(".bin", ".txt"), textos);
            }

        }

        public void ExportarParaTxtPonteirosExternos(string dirTextoBin, string dirIdx, int quatidadeOffsetSeek, int inicio, bool mutiplica)
        {
            int quantidaDePonteiros = 0;
            int posicaoTabela = inicio;

            List<string> textos = new List<string>();
            int somaPonteiro = 0;

            Stream bin = new MemoryStream(File.ReadAllBytes(dirTextoBin));
            Stream idx = new MemoryStream(File.ReadAllBytes(dirIdx));

            List<int> ponteiros = new List<int>();
            List<int> posicoesTabela = new List<int>();

            if (mutiplica)
            {
                textos.Add("<Tipo de Ponteiro: ExternosX2 =" + Path.GetFileName(dirIdx) + ">");
            }
           else
            {
                textos.Add("<Tipo de Ponteiro: ExternosX1 =" + Path.GetFileName(dirIdx) + ">");
            }
            

            using (BinaryReader br = new BinaryReader(idx))
            {
                quantidaDePonteiros = (int)(idx.Length / quatidadeOffsetSeek);
                posicaoTabela = 0;

                for (int i = 0; i < quantidaDePonteiros; i++)
                {
                    posicoesTabela.Add(posicaoTabela);
                    br.BaseStream.Seek(posicaoTabela,SeekOrigin.Begin);
                    if (mutiplica)
                        ponteiros.Add(br.ReadInt32() * 2);
                    else
                        ponteiros.Add(br.ReadInt32());

                    posicaoTabela += quatidadeOffsetSeek;
                }
            }


            using (BinaryReader br = new BinaryReader(bin))
            {
                int tamanhoTexto = 0;

                for (int i = 0; i < ponteiros.Count; i++)
                {
                    if (ponteiros[i] == bin.Length)
                    {
                        break;
                    }

                    br.BaseStream.Seek(ponteiros[i], SeekOrigin.Begin);

                    if (i < ponteiros.Count - 1)
                    {
                        tamanhoTexto = ponteiros[i + 1] - ponteiros[i];
                    }
                    else
                    {
                        tamanhoTexto = (int)bin.Length - ponteiros[i];
                    }
                    

                   

                    br.BaseStream.Seek(ponteiros[i], SeekOrigin.Begin);

                    byte[] textoEmUnicode = br.ReadBytes(tamanhoTexto);

                    var textoEmString = Encoding.Unicode.GetString(textoEmUnicode);
                    textos.Add("<" + "PONTEIRO: " + posicoesTabela[i] + "," + somaPonteiro + ">\n" + "<TEXTO>" + textoEmString
                        .Replace("$CA", "<COR: $CA>")
                        .Replace("$C0", "<COR: $C0>")
                        .Replace("$C2", "<COR: $C2>")
                        .Replace("$C3", "<COR: $C3>")
                        .Replace("$C5", "<COR: $C5>")
                        .Replace("$C8", "<COR: $C8>")
                        .Replace("\n", "<b>\n").Replace("$0", "<JOGADOR: $0>")
                        .Replace("\0", "<NULL>") + "<TEXTO/>\n" + "<FIM/>\n\n");
                    posicaoTabela += 8;
                    somaPonteiro = 0;
                }


                File.WriteAllLines(dirTextoBin.Replace(".bin", ".txt"), textos);
            }

        }

        public void ExportarCartasParaTxt(string dirCard)
        {
            List<string> arquivosNecessariosComprimir = ArquivosNecessarios(dirCard).OrderBy(x => x).ToList();

            string cardDesc = arquivosNecessariosComprimir[0];
            string cardHuff = arquivosNecessariosComprimir[1];
            string cardIdx = arquivosNecessariosComprimir[2];
            string cardIntID = arquivosNecessariosComprimir[3];
            string cardName = arquivosNecessariosComprimir[4];                      
            string Dict = arquivosNecessariosComprimir[5];
            List<string> descricaoTexto = new List<string>();

            Huffman huff = new Huffman();
            List<short> descomp = huff.Descomprimir(cardDesc,cardHuff,cardIdx);
            Dicionario conversorDeCodigos = new Dicionario();
            byte[] descdescomprimida = conversorDeCodigos.TranduzirComDicionario(Dict,cardIdx, descomp, cardIntID,cardName);
            string[] tex = ConvertaDescricaoParaString(descdescomprimida).Replace("\0", "<NULL>\0").Split('\0');

            foreach (var textoo in tex)
            {
                descricaoTexto.Add("<DESCRICAO>" + textoo + "<DESCRICAO/>\n\n");
            }

            int quantidaDePonteiros = 0;
            int posicaoTabela = 0;
            int quatidadeOffsetSeek = 8;
            List<string> textos = new List<string>();
            int somaPonteiro = 0;

            Stream bin = new MemoryStream(File.ReadAllBytes(cardName));
            Stream idx = new MemoryStream(File.ReadAllBytes(cardIdx));

            List<int> ponteiros = new List<int>();
            List<int> posicoesTabela = new List<int>();

            using (BinaryReader br = new BinaryReader(idx))
            {
                quantidaDePonteiros = (int)(idx.Length / quatidadeOffsetSeek);
                posicaoTabela = 0;

                for (int i = 0; i < quantidaDePonteiros; i++)
                {
                    posicoesTabela.Add(posicaoTabela);
                    br.BaseStream.Seek(posicaoTabela, SeekOrigin.Begin);
                    ponteiros.Add(br.ReadInt32());
                    posicaoTabela += quatidadeOffsetSeek;
                }
            }


            using (BinaryReader br = new BinaryReader(bin))
            {
                int tamanhoTexto = 0;

                for (int i = 0; i < ponteiros.Count; i++)
                {
                    if (ponteiros[i] == bin.Length)
                    {
                        break;
                    }

                    br.BaseStream.Seek(ponteiros[i], SeekOrigin.Begin);

                    if (i < ponteiros.Count - 1)
                    {
                        tamanhoTexto = ponteiros[i + 1] - ponteiros[i];
                    }
                    else
                    {
                        tamanhoTexto = (int)bin.Length - ponteiros[i];
                    }




                    br.BaseStream.Seek(ponteiros[i], SeekOrigin.Begin);

                    byte[] textoEmUnicode = br.ReadBytes(tamanhoTexto);

                    var textoEmString = Encoding.Unicode.GetString(textoEmUnicode);
                    textos.Add("<" + "PONTEIRO: " + posicoesTabela[i] + "," + somaPonteiro + ">\n" + "<NOME>" + textoEmString.Replace("$CA", "<COR: $CA>").Replace("$C0", "<COR: $C0>").Replace("$C5", "<COR: $C5>").Replace("$C8", "<COR: $C8>").Replace("\n", "<b>\n").Replace("$0", "<JOGADOR: $0>").Replace("\0", "<NULL>") + "<NOME/>\n");
                    posicaoTabela += 8;
                    somaPonteiro = 0;
                }


                
            }

            List<string> textosCartaFinal = new List<string>();

            for (int i = 0; i < descricaoTexto.Count - 1; i++)
            {
                textosCartaFinal.Add(textos[i] + descricaoTexto[i]);
            }

            File.WriteAllLines(cardIdx.Replace(".bin", ".txt"), textosCartaFinal);
        }

        private List<string> ArquivosNecessarios(string diretorio)
        {
            List<string> arquivosParaComprimir = new List<string> { "CARD_Name", "CARD_Desc", "CARD_Indx", "CARD_Huff", "DICT_E" , "CARD_IntID" };
            string[] arquivosDaPasta = Directory.GetFiles(diretorio);
            List<string> arquivosVerificados = new List<string>();

            for (int i = 0; i < arquivosDaPasta.Length; i++)
            {
                for (int y = 0; y < arquivosParaComprimir.Count; y++)
                {
                    if (arquivosDaPasta[i].Contains(arquivosParaComprimir[y]))
                    {
                        arquivosVerificados.Add(arquivosDaPasta[i]);
                        arquivosParaComprimir[y] += "<v>";

                        break;
                    }
                }

            }

            if (arquivosVerificados.Count < 5)
            {
                string arquivosFaltantes = "";
                for (int i = 0; i < arquivosParaComprimir.Count; i++)
                {
                    if (!arquivosParaComprimir[i].Contains("<v>"))
                    {
                        arquivosFaltantes += arquivosParaComprimir[i] + "\n";
                    }
                }

                throw new Exception("Não foram encontrados os seguintes arquivos: " + arquivosFaltantes);
            }

            return arquivosVerificados;
        }

        private string ConvertaDescricaoParaString(byte[] textoDescomprimido)
        {
            var textao = Encoding.Unicode.GetString(textoDescomprimido);

            return textao;
        }
    }

}  

    