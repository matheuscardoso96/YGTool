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
        public void ExportarParaTxtPonteirosInternosIndiretos(string dirTextoBin)
        {
            int quantidaDePonteiros = 0;
            int tamanhoDoHeader = 0;
            int tamanhoDaTabela = 0;
            int posicaoTabela = 0;
            int ponteiro = 0;
            List<string> textos = new List<string>();
            int somaPonteiro = 0;

            Stream bin = new MemoryStream(File.ReadAllBytes(dirTextoBin));
            textos.Add("<Tipo de Ponteiro = Interno Indireto>\n\n");



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

                    var textoEmString = "";

                    if (ponteiro == bin.Length)
                    {
                        textos.Add("<" + "PONTEIRO: " + posicaoTabela + "," + somaPonteiro + ">\r\n" + "<TEXTO>" + textoEmString + "<TEXTO/>\n" + "<FIM/>\n\n");
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
                    byte[] textoEmUnicode = br.ReadBytes(tamanhoTexto);

                    textoEmString = Encoding.Unicode.GetString(textoEmUnicode);
                    textos.Add("<" + "PONTEIRO: " + posicaoTabela + "," + somaPonteiro + ">\n" + "<TEXTO>" + textoEmString.Replace("$CA", "<COR: $CA>").Replace("$C0", "<COR: $C0>").Replace("$C5", "<COR: $C5>").Replace("$C8", "<COR: $C8>").Replace("$0", "<JOGADOR: $0>").Replace("\0", "<NULL>") + "<TEXTO/>\n" + "<FIM/>\n\n");
                    posicaoTabela += 4;
                    somaPonteiro = 0;
                }


                File.WriteAllLines(dirTextoBin.Replace(".bin", ".txt"), textos);
            }

        }

        public void ExportarParaTxtPonteirosInternosIndiretosX2(string dirTextoBin)
        {
            int quantidaDePonteiros = 0;
            int tamanhoDaTabela = 0;
            int posicaoTabela = 0;
            int ponteiro = 0;
            List<string> textos = new List<string>();
            int somaPonteiro = 0;

            Stream bin = new MemoryStream(File.ReadAllBytes(dirTextoBin));
            textos.Add("<Tipo de Ponteiro = Interno IndiretoX2>\n\n");



            using (BinaryReader br = new BinaryReader(bin))
            {
                tamanhoDaTabela = br.ReadInt32();
                quantidaDePonteiros = tamanhoDaTabela / 4;
                posicaoTabela = (int)br.BaseStream.Position;

                for (int i = 0; i < quantidaDePonteiros; i++)
                {


                    int tamanhoTexto = 0;
                    br.BaseStream.Seek(posicaoTabela, SeekOrigin.Begin);
                    ponteiro = br.ReadInt32();

                    

                    ponteiro += tamanhoDaTabela;

                    var textoEmString = "";

                    if (ponteiro == bin.Length)
                    {
                        textos.Add("<" + "PONTEIRO: " + posicaoTabela + "," + somaPonteiro + ">\n" + "<TEXTO>" + textoEmString + "<TEXTO/>\n" + "<FIM/>\n\n");
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
                    byte[] textoEmUnicode = br.ReadBytes(tamanhoTexto);

                    textoEmString = Encoding.Unicode.GetString(textoEmUnicode);
                    textos.Add("<" + "PONTEIRO: " + posicaoTabela + "," + somaPonteiro + ">\n" + "<TEXTO>" + textoEmString.Replace("$CA", "<COR: $CA>").Replace("$C0", "<COR: $C0>").Replace("$C5", "<COR: $C5>").Replace("$C8", "<COR: $C8>").Replace("\n", "<b>\n").Replace("$0", "<JOGADOR: $0>").Replace("\0", "<NULL>") + "<TEXTO/>\n" + "<FIM/>\n\n");
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

            textos.Add("<Tipo de Ponteiro = Interno direto>\n\n");

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
                    textos.Add("<" + "PONTEIRO: " + posicaoTabela + "," + somaPonteiro + ">\n" + "<TEXTO>" + textoEmString.Replace("\0", "<NULL>").Replace("@2", "<Cor: @2>").Replace("@0", "<Cor: @0>").Replace("\n", "<b>\n") + "<TEXTO/>\n" + "<FIM/>\n\n");
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
                textos.Add("<Tipo de Ponteiro = ExternosX2 =" + dirIdx + ">");
            }
           else
            {
                textos.Add("<Tipo de Ponteiro = ExternosX1 =" + dirIdx + ">");
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
                        .Replace("$CA", "<COR:$CA>")
                        .Replace("$C0", "<COR:$C0>")
                        .Replace("$C2", "<COR:$C2>")
                        .Replace("$C3", "<COR:$C3>")
                        .Replace("$C5", "<COR:$C5>")
                        .Replace("$C8", "<COR:$C8>")
                        .Replace("$C9", "<COR:$C9>")
                        .Replace("\r", "<r>")
                        .Replace("\n", "<b>\n").Replace("$0", "<JOGADOR:$0>")
                        .Replace("\0", "<NULL>") + "<TEXTO/>\n" + "<FIM/>\n\n");
                    posicaoTabela += 8;
                    somaPonteiro = 0;
                }


                File.WriteAllLines(dirTextoBin.Replace(".bin", ".txt"), textos);
            }

        }

        public void ExportarCartasParaTxt(string dirCard,TagForce tagForce)
        {
            List<string> arquivosNecessariosComprimir = ArquivosNecessarios(dirCard).OrderBy(x => x).ToList();

            string cardDesc = arquivosNecessariosComprimir[0];
            string cardHuff = arquivosNecessariosComprimir[1];
            string cardIdx = arquivosNecessariosComprimir[2];
            string cardIntID = arquivosNecessariosComprimir[3];
            string cardName = arquivosNecessariosComprimir[4];                      
            string Dict = arquivosNecessariosComprimir[5];
            List<string> descricaoTexto = new List<string>();
            ExportarParaTxtPonteirosInternosIndiretos(Dict);
            Huffman huff = new Huffman();
            List<string> descomp = huff.Descomprimir(cardDesc,cardHuff,cardIdx);
          
            Dicionario conversorDeCodigos = new Dicionario();
          


            byte[] descdescomprimida = new byte[]  { };
            List<string> tex = conversorDeCodigos.TranduzirComDicionario(Dict, cardIdx, descomp, cardIntID, cardName, tagForce);

            //  File.WriteAllLines("desc.txt",tex);

            foreach (var textoo in tex)
            {
                if (textoo.Contains("\0"))
                {
                    descricaoTexto.Add("<DESCRICAO>" + textoo.Replace("\0", "<NULL>") + "<DESCRICAO/><FIM/>\n\n");
                }
                else
                {
                    descricaoTexto.Add("<DESCRICAO>" + textoo + "<NULL>" + "<DESCRICAO/><FIM/>\n\n");
                }
                
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
               

                for (int i = 0; i < ponteiros.Count; i++)
                {

                    if (ponteiros[i] == bin.Length || ponteiros[i] > bin.Length)
                    {
                        break;
                    }
                    

                    br.BaseStream.Seek(ponteiros[i], SeekOrigin.Begin);
                    string nomeCarta = "";
                    while (true)
                    {
                        string letra = Encoding.Unicode.GetString(br.ReadBytes(2));

                        if (letra.Contains("\0"))
                        {
                            nomeCarta += "<NULL>";
                            break;
                        }

                        nomeCarta += letra;

                    }

                

                    
                    textos.Add("<" + "PONTEIRO: " + posicoesTabela[i] + "," + somaPonteiro + ">\n" + "<NOME>" + nomeCarta.Replace("$CA", "<COR: $CA>").Replace("$C0", "<COR: $C0>").Replace("$C5", "<COR: $C5>").Replace("$C8", "<COR: $C8>").Replace("\n", "<b>\n").Replace("$0", "<JOGADOR: $0>").Replace("\0", "<NULL>") + "<NOME/>\n");
                    posicaoTabela += 8;
                    somaPonteiro = 0;
                }


                
            }

            List<string> textosCartaFinal = new List<string>();

            textosCartaFinal.Add("<Tipo de Ponteiro=Informação de Cartas=" + cardIdx + " = " + Dict + ">");

            for (int i = 0; i < textos.Count; i++)
            {
                textosCartaFinal.Add(textos[i] + descricaoTexto[i]);
            }

            File.WriteAllLines(cardDesc.Replace(".bin", ".txt"), textosCartaFinal);
        }

        private List<string> ArquivosNecessarios(string diretorio)
        {
            List<string> arquivosParaComprimir = new List<string> { "CARD_Name_", "CARD_Desc", "CARD_Indx", "CARD_Huff", "DICT" , "CARD_IntID" };
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

        public void ImportarTexto(string fileName, bool tagoForce4)
        {
            string obterTipoDeTexto = File.ReadAllText(fileName);

            if (obterTipoDeTexto.Contains("Interno Indireto"))
            {
                if (obterTipoDeTexto.Contains("X2"))
                {
                    InsiraInternoIndiretoX2(obterTipoDeTexto, fileName);
                }
                else
                {
                    InsiraInternoIndireto(obterTipoDeTexto, fileName);
                }
                
            }

            else if(obterTipoDeTexto.Contains("Interno direto"))
            {
                InsiraInternoDireto(obterTipoDeTexto, fileName);
            }
            else if (obterTipoDeTexto.Contains("Externos"))
            {
                InsiraExternos(obterTipoDeTexto, fileName);
            }
            else if (obterTipoDeTexto.Contains("Informação de Cartas"))
            {
                InsiraInformacoesDeCartas(obterTipoDeTexto, fileName, tagoForce4);
            }

        }

        private void InsiraInformacoesDeCartas(string texto, string nomeArquivo, bool tagForce4)
        {
            int tamanhoDoHeader = 0;
            int tamanhoDaTabela = 0;
            int posicaoTabela = 0;
            int ponteiro = 0;
            string idx = "";
            string dicte = "";
            string[] dividirTexto = texto.Replace("\r", "").Replace("\n", "").Split(new[] { "<FIM/>" }, StringSplitOptions.RemoveEmptyEntries);
            string[] informacoesTabela = ObtenhaTipoDePonteiro(dividirTexto.First()).Split(',');
            idx = informacoesTabela[1];
            dicte = informacoesTabela[2];
            Dicionario dicionario = new Dicionario();
           
            Stream bin = new MemoryStream(File.ReadAllBytes(idx));
            Stream backupTabela = new MemoryStream();
            List<byte[]> arquivoEmbytes = new List<byte[]>();
            tamanhoDaTabela = (int)bin.Length;

            using (BinaryReader br = new BinaryReader(bin))
            {

                byte[] tabela = br.ReadBytes(tamanhoDaTabela);
                backupTabela = new MemoryStream(tabela);

            }

            posicaoTabela = tamanhoDoHeader;
            MemoryStream tabelaNova = new MemoryStream();

            byte[] padding = new byte[4];
            arquivoEmbytes.Add(padding);

            StringBuilder descricaoDescomprimida = new StringBuilder();

            using (BinaryWriter bw = new BinaryWriter(backupTabela))
            {
                int tamanhoTotal = 0;
                tamanhoTotal = 4;
                ponteiro = 4;

                foreach (var item in dividirTexto)
                {
                    string[] textoSplit;
                   
                     textoSplit = item.Replace("<b>", "\r\n").Split(new[] { "<NOME>" }, StringSplitOptions.RemoveEmptyEntries);
                    
                    

                    int[] infoPoint = ObtenhaInformacaoDoPonteiro(textoSplit[0]);
                    posicaoTabela = infoPoint[0];
                    bw.BaseStream.Seek(posicaoTabela, SeekOrigin.Begin);
                    bw.Write(ponteiro + infoPoint[1]);

                    string[] nome = textoSplit[1].Split(new[] { "<NOME/>" }, StringSplitOptions.RemoveEmptyEntries);
                    string textoQueSeraConvertido = RemovaTagsDoTexto(nome[0]);
                    string textoDescricao = RemovaTagsDoTexto(nome[1].Replace("<DESCRICAO>", "").Replace("<DESCRICAO/>", ""));
                    descricaoDescomprimida.Append(textoDescricao);
                    byte[] textoEmbytes = Encoding.Unicode.GetBytes(textoQueSeraConvertido);
                    arquivoEmbytes.Add(textoEmbytes);
                    tamanhoTotal += textoEmbytes.Length;
                    ponteiro = tamanhoTotal;

                }

               
                bw.BaseStream.Position = 0;
                backupTabela.CopyTo(tabelaNova);
            }

            byte[] textoFinalEmByte = new byte[ponteiro];
            //string descricaoComdicionario =  descricaoDescomprimida.ToString();
            string descricaoComdicionario = dicionario.ObterTextoDoDicionarioESubstituir( dicte, descricaoDescomprimida.ToString());

            MemoryStream textoFinal = new MemoryStream(textoFinalEmByte);

            using (BinaryWriter bw = new BinaryWriter(textoFinal))
            {

                foreach (var item in arquivoEmbytes)
                {
                    bw.Write(item);
                }

                textoFinalEmByte = textoFinal.ToArray();
            }

            File.WriteAllBytes(nomeArquivo.Replace(".txt", ".bin").Replace("CARD_Desc", "CARD_Name"), textoFinalEmByte);

            File.WriteAllBytes(idx, tabelaNova.ToArray());
            byte[] descBytes = Encoding.Unicode.GetBytes(descricaoComdicionario);
            if (descBytes[0] != 0)
            {
              byte[] tmp = new byte[descBytes.Length - 2];
              Array.Copy(descBytes,2,tmp,0 ,descBytes.Length -2);
                descBytes = tmp;
            }
            File.WriteAllBytes(nomeArquivo.Replace(".txt", ".bin"), descBytes);

            Comprima(nomeArquivo.Replace(".txt", ".bin"), nomeArquivo.Replace(".txt", ".bin").Replace("CARD_Desc", "CARD_Huff"), idx);

            ImportarTexto(dicte.Replace(".bin",".txt"), false);

        }

        private void InsiraExternos(string texto, string nomeArquivo)
        {
            int quantidaDePonteiros = 0;
            int tamanhoDoHeader = 0;
            int tamanhoDaTabela = 0;
            int posicaoTabela = 0;
            int ponteiro = 0;
            int metade = 0;
            string idx = "";
            string[] dividirTexto = texto.Replace("\r", "").Replace("\n", "").Split(new[] { "<FIM/>" }, StringSplitOptions.RemoveEmptyEntries);
            string[] informacoesTabela = ObtenhaTipoDePonteiro(dividirTexto.First()).Split(',');
            idx = informacoesTabela[1];

            if (informacoesTabela[0].Contains("X2"))
            {
                metade = 1;
            }


            Stream bin = new MemoryStream(File.ReadAllBytes(idx));

            Stream backupTabela = new MemoryStream();
            List<byte[]> arquivoEmbytes = new List<byte[]>();

            tamanhoDaTabela = (int)bin.Length;

            using (BinaryReader br = new BinaryReader(bin))
            {
                
                byte[] tabela = br.ReadBytes(tamanhoDaTabela);
                backupTabela = new MemoryStream(tabela);
                quantidaDePonteiros = tamanhoDaTabela / 4;

            }

            posicaoTabela = tamanhoDoHeader;
            MemoryStream tabelaNova = new MemoryStream();

            using (BinaryWriter bw = new BinaryWriter(backupTabela))
            {
                int tamanhoTotal = 0;

                foreach (var item in dividirTexto)
                {
                    string[] textoSplit = item.Replace("<b>", "\r\n").Replace("<TEXTO/>", "").Split(new[] { "<TEXTO>" }, StringSplitOptions.RemoveEmptyEntries);

                    int[] infoPoint = ObtenhaInformacaoDoPonteiro(textoSplit[0]);
                    posicaoTabela = infoPoint[0];
                    bw.BaseStream.Seek(posicaoTabela, SeekOrigin.Begin);
                    bw.Write(ponteiro + infoPoint[1] >> metade);

                    string textoQueSeraConvertido = RemovaTagsDoTexto(textoSplit[1]);
                    byte[] textoEmbytes = Encoding.Unicode.GetBytes(textoQueSeraConvertido);
                    arquivoEmbytes.Add(textoEmbytes);
                    tamanhoTotal += textoEmbytes.Length;
                    ponteiro = tamanhoTotal;

                }

                bw.BaseStream.Position = 0;
                backupTabela.CopyTo(tabelaNova);
            }

            byte[] textoFinalEmByte = new byte[ponteiro];

            MemoryStream textoFinal = new MemoryStream(textoFinalEmByte);

            using (BinaryWriter bw = new BinaryWriter(textoFinal))
            {

                foreach (var item in arquivoEmbytes)
                {
                    bw.Write(item);
                }

                textoFinalEmByte = textoFinal.ToArray();
            }

            File.WriteAllBytes(nomeArquivo.Replace(".txt", ".bin"), textoFinalEmByte);

            File.WriteAllBytes(idx, tabelaNova.ToArray());

        }

        private void InsiraInternoDireto(string texto, string nomeArquivo)
        {
            int quantidaDePonteiros = 0;
            int tamanhoDoArquivoEmBytes = 0;
            int tamanhoDaTabela = 0;
            int posicaoTabela = 0;
            int ponteiro = 0;

            string[] dividirTexto = texto.Replace("\r", "").Replace("\n", "").Split(new[] { "<FIM/>" }, StringSplitOptions.RemoveEmptyEntries);
            Stream bin = new MemoryStream(File.ReadAllBytes(nomeArquivo.Replace(".txt", ".bin")));
            Stream backupTabela = new MemoryStream();
            List<byte[]> arquivoEmbytes = new List<byte[]>();


            using (BinaryReader br = new BinaryReader(bin))
            {
                tamanhoDaTabela = br.ReadInt32();
                quantidaDePonteiros = tamanhoDaTabela / 8;
               
                br.BaseStream.Position = 0;
                byte[] tabela = br.ReadBytes(tamanhoDaTabela);
                backupTabela = new MemoryStream(tabela);
            }

            posicaoTabela = 0;
            MemoryStream tabelaNova = new MemoryStream();

            using (BinaryWriter bw = new BinaryWriter(backupTabela))
            {
                ponteiro = tamanhoDaTabela;

                foreach (var item in dividirTexto)
                {
                    string[] textoSplit = item.Replace("<b>", "\r\n").Replace("<TEXTO/>", "").Split(new[] { "<TEXTO>" }, StringSplitOptions.RemoveEmptyEntries);

                    int[] infoPoint = ObtenhaInformacaoDoPonteiro(textoSplit[0]);
                    posicaoTabela = infoPoint[0];
                    bw.BaseStream.Seek(posicaoTabela, SeekOrigin.Begin);
                    bw.Write(ponteiro + infoPoint[1]);

                    string textoQueSeraConvertido = RemovaTagsDoTexto(textoSplit[1]);
                    byte[] textoEmbytes = Encoding.Unicode.GetBytes(textoQueSeraConvertido);
                    arquivoEmbytes.Add(textoEmbytes);
                    bw.Write(textoEmbytes.Length);

                    ponteiro += textoEmbytes.Length;
                    tamanhoDoArquivoEmBytes += textoEmbytes.Length;

                }

                bw.BaseStream.Position = 0;
                backupTabela.CopyTo(tabelaNova);
            }

            byte[] textoFinalEmByte = new byte[tamanhoDoArquivoEmBytes + tabelaNova.Length];

            MemoryStream textoFinal = new MemoryStream(textoFinalEmByte);

            using (BinaryWriter bw = new BinaryWriter(textoFinal))
            {

                bw.Write(tabelaNova.ToArray());

                foreach (var item in arquivoEmbytes)
                {
                    bw.Write(item);
                }

                textoFinalEmByte = textoFinal.ToArray();
            }

            File.WriteAllBytes(nomeArquivo.Replace(".txt", ".bin"), textoFinalEmByte);
        }

        private void InsiraInternoIndireto(string texto, string nomeArquivo)
        {
            int quantidaDePonteiros = 0;
            int tamanhoDoHeader = 0;
            int tamanhoDaTabela = 0;
            int posicaoTabela = 0;
            int ponteiro = 0;

            string[] dividirTexto = texto.Replace("\r", "").Replace("\n", "").Split(new[] { "<FIM/>" }, StringSplitOptions.RemoveEmptyEntries);
            Stream bin = new MemoryStream(File.ReadAllBytes(nomeArquivo.Replace(".txt", ".bin")));
            Stream backupTabela = new MemoryStream();
            List<byte[]> arquivoEmbytes = new List<byte[]>();
           

            using (BinaryReader br = new BinaryReader(bin))
            {
                quantidaDePonteiros = br.ReadInt32();
                tamanhoDoHeader = br.ReadInt32();
                tamanhoDaTabela = br.ReadInt32();
                br.BaseStream.Position = 0;
                byte[] tabela = br.ReadBytes(tamanhoDaTabela);
                backupTabela = new MemoryStream(tabela);
            }

            posicaoTabela = tamanhoDoHeader;
            MemoryStream tabelaNova = new MemoryStream();

            using (BinaryWriter bw = new BinaryWriter(backupTabela))
            {
                
               foreach (var item in dividirTexto)
                {                   
                    string[] textoSplit = item.Replace("<b>", "\r\n").Replace("<TEXTO/>", "").Split(new[] { "<TEXTO>" }, StringSplitOptions.RemoveEmptyEntries);

                    int[] infoPoint = ObtenhaInformacaoDoPonteiro(textoSplit[0]);
                    posicaoTabela = infoPoint[0];

                    bw.BaseStream.Seek(posicaoTabela, SeekOrigin.Begin);
                    bw.Write(ponteiro + infoPoint[1]);
                    if (textoSplit.Length > 1)
                    {
                        string textoQueSeraConvertido = RemovaTagsDoTexto(textoSplit[1]);
                        byte[] textoEmbytes = Encoding.Unicode.GetBytes(textoQueSeraConvertido);
                        arquivoEmbytes.Add(textoEmbytes);
                        ponteiro += textoEmbytes.Length;
                    }
                    
                    
                }

                bw.BaseStream.Position = 0;
                backupTabela.CopyTo(tabelaNova);
            }

            byte[] textoFinalEmByte = new byte[ponteiro + tabelaNova.Length];

            MemoryStream textoFinal = new MemoryStream(textoFinalEmByte);

            using (BinaryWriter bw = new BinaryWriter(textoFinal))
            {
                
                bw.Write(tabelaNova.ToArray());

                foreach (var item in arquivoEmbytes)
                {
                    bw.Write(item);
                }

                textoFinalEmByte = textoFinal.ToArray();
            }

            File.WriteAllBytes(nomeArquivo.Replace(".txt", ".bin"), textoFinalEmByte);
        }

        private void InsiraInternoIndiretoX2(string texto, string nomeArquivo)
        {
            int quantidaDePonteiros = 0;
            int tamanhoDaTabela = 0;
            int posicaoTabela = 0;
            int ponteiro = 0;

            string[] dividirTexto = texto.Replace("\r", "").Replace("\n", "").Split(new[] { "<FIM/>" }, StringSplitOptions.RemoveEmptyEntries);
            Stream bin = new MemoryStream(File.ReadAllBytes(nomeArquivo.Replace(".txt", ".bin")));
            Stream backupTabela = new MemoryStream();
            List<byte[]> arquivoEmbytes = new List<byte[]>();


            using (BinaryReader br = new BinaryReader(bin))
            {
                tamanhoDaTabela = br.ReadInt32();
                quantidaDePonteiros = tamanhoDaTabela / 4;
                byte[] tabela = br.ReadBytes(tamanhoDaTabela);
                backupTabela = new MemoryStream(tabela);
            }

            posicaoTabela = 4;

            MemoryStream tabelaNova = new MemoryStream();

            using (BinaryWriter bw = new BinaryWriter(backupTabela))
            {
                bw.Write((dividirTexto.Length * 4) + 4);
                foreach (var item in dividirTexto)
                {
                    string[] textoSplit = item.Replace("<b>", "\r\n").Replace("<TEXTO/>", "").Split(new[] { "<TEXTO>" }, StringSplitOptions.RemoveEmptyEntries);

                    int[] infoPoint = ObtenhaInformacaoDoPonteiro(textoSplit[0]);
                    posicaoTabela = infoPoint[0];

                    bw.BaseStream.Seek(posicaoTabela, SeekOrigin.Begin);
                    bw.Write(ponteiro + infoPoint[1]);
                    if (textoSplit.Length > 1)
                    {
                        string textoQueSeraConvertido = RemovaTagsDoTexto(textoSplit[1]);
                        byte[] textoEmbytes = Encoding.Unicode.GetBytes(textoQueSeraConvertido);
                        arquivoEmbytes.Add(textoEmbytes);
                        ponteiro += textoEmbytes.Length;
                    }


                }

                bw.BaseStream.Position = 0;
                backupTabela.CopyTo(tabelaNova);
            }

            byte[] textoFinalEmByte = new byte[ponteiro + tabelaNova.Length];

            MemoryStream textoFinal = new MemoryStream(textoFinalEmByte);

            using (BinaryWriter bw = new BinaryWriter(textoFinal))
            {

                bw.Write(tabelaNova.ToArray());

                foreach (var item in arquivoEmbytes)
                {
                    bw.Write(item);
                }

                textoFinalEmByte = textoFinal.ToArray();
            }

            File.WriteAllBytes(nomeArquivo.Replace(".txt", ".bin"), textoFinalEmByte);
        }

        private int[] ObtenhaInformacaoDoPonteiro(string texto)
        {
            texto = texto.Replace(" ", "").Replace(">","").Replace("<", "§");
            int[] informacoes = new int[2];
            string[] inf = texto.Split('§').Last().Split(':');
            string[] separaInfo = inf[1].Split(',');
            int posicaoPonteiro = int.Parse(separaInfo[0]);
            int valorASomarPonteiro = int.Parse(separaInfo[1]);

            informacoes[0] = posicaoPonteiro;
            informacoes[1] = valorASomarPonteiro;

            return informacoes;
        }

        private string RemovaTagsDoTexto(string texto)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] != '<')
                {
                    
                    sb.Append(texto[i]);
                }
                else
                {
                    string argumento = "";
                    if (texto[i + 1] == 'M')
                    {
                        i+= 2;
                        sb.Append("$m");

                        while (texto[i] != '>')
                        {
                            sb.Append(texto[i]);
                            i++;
                        }
                        i++;
                        string ignorado = "";
                        while (texto[i] != '>')
                        {
                            ignorado += texto[i];
                            i++;
                        }


                    }
                    else {

                        while (texto[i] != '>')
                        {
                            argumento += texto[i];

                            i++;
                        }

                        argumento += texto[i];
                        argumento = ConversorDeArgumentos(argumento);
                        sb.Append(argumento);
                    } 
                }
            }
            return sb.ToString();
        }

        private string ConversorDeArgumentos(string argumento)
        {
            if (argumento.Contains("<NULL>"))
            {
                return "\0";
            }

            if (argumento.Contains("<r>"))
            {
                return "\r";
            }

            if (argumento.ToUpper().Contains("<COR") || argumento.ToUpper().Contains("<JOGADOR"))
            {
                argumento = argumento.Replace(" ", "").Replace(">", "");
                string[] valorCor = argumento.Split(':');
                return valorCor.Last();
            }

            return null;
        }

        private string ObtenhaTipoDePonteiro(string texto)
        {
            string[] dividir = texto.Replace(".bin>", ".bin>~").Split('~');
            string[] outraDivi = dividir[0].Replace(" ", "").Replace("<", "").Replace(">", "").Split('=');
            if (texto.Contains("CARD_Indx"))
                return outraDivi[1] + "," + outraDivi[2] + "," + outraDivi[3];
            else
                return outraDivi[1] + "," + outraDivi[2];
            
            
        }

        private void Comprima(string cardDesc, string cardHuff, string cardIdx)
        {
            Huffman huffman = new Huffman();
            huffman.Comprimir(cardDesc, cardHuff, cardIdx);
        }
    }

}  

    