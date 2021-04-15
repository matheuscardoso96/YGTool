using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YGTool.Compressao
{
    public class Huffman
    {

        public List<string> Descomprimir(string dirDescricao, string dirCardHuff, string dirCardIndx)
        {
            Stream descricao = new MemoryStream(File.ReadAllBytes(dirDescricao));
            Stream cardIndx = new MemoryStream(File.ReadAllBytes(dirCardIndx));
            Stream cardHuff = new MemoryStream(File.ReadAllBytes(dirCardHuff));
            List<string> descricoes = new List<string>();
            
            int ponteiroDescricao = 0;
            int ponteiroArquivoComprimido;
            int valorHuff = 0;
            int quantidadeDePonteiros = (int)((cardIndx.Length / 4) / 2);
            int enderecoDoPonteiro = 0x4;
            int tamanhoEmBits = (int)descricao.Length * 8;
            int ponteiro = 0;

            using (BinaryReader leitorDePonteiro = new BinaryReader(cardIndx))
            {

                using (BinaryReader leitorCardHuff = new BinaryReader(cardHuff))
                {
                    using (BinaryReader leitorDeDescricao = new BinaryReader(descricao))
                    {
                        string descricaoDescomprimida = "";

                        for (int i = 0; i < quantidadeDePonteiros; i++)
                        {
                            if (enderecoDoPonteiro == 0x2594)
                            {
                           
                            }
                            leitorDePonteiro.BaseStream.Seek(enderecoDoPonteiro, SeekOrigin.Begin);
                            ponteiroDescricao = leitorDePonteiro.ReadInt32();
                            ponteiro = ponteiroDescricao;
                            ponteiroArquivoComprimido = ponteiroDescricao >> 3;
                            int quantidadeDeBitsANaoUsar = ponteiroDescricao & 7;
                            int numeroDeBits = 7;
                            int quantidadeDeBitsAUsar = numeroDeBits - quantidadeDeBitsANaoUsar;

                            int valorComprimido = 0;
                            int posicaoSalvaDentroDaArvore = 0;

                            while (quantidadeDeBitsAUsar >= 0)
                            {
                                leitorDeDescricao.BaseStream.Seek(ponteiroArquivoComprimido, SeekOrigin.Begin);
                                if (ponteiroArquivoComprimido * 8 == tamanhoEmBits)
                                {
                                    break;
                                }
                                valorComprimido = leitorDeDescricao.ReadByte();
                                int proximoBit = valorComprimido >> quantidadeDeBitsAUsar;
                                int posicaoNaArvoreHuffman = (proximoBit & 0x1) * 2;

                                posicaoNaArvoreHuffman += posicaoSalvaDentroDaArvore;


                                leitorCardHuff.BaseStream.Seek(posicaoNaArvoreHuffman, SeekOrigin.Begin);
                                valorHuff = leitorCardHuff.ReadInt16();

                                if (valorHuff == 0)
                                {
                                    valorHuff = posicaoSalvaDentroDaArvore;
                                    leitorCardHuff.BaseStream.Seek(valorHuff + 4, SeekOrigin.Begin);
                                    string valorDoCaractere = Encoding.Unicode.GetString(leitorCardHuff.ReadBytes(2));
                                    descricaoDescomprimida += valorDoCaractere;

                                    if (valorDoCaractere.Contains("\0"))
                                        break;

                                    else
                                        posicaoSalvaDentroDaArvore = 0;


                                }
                                else
                                {
                                    quantidadeDeBitsAUsar -= 1;
                                    posicaoSalvaDentroDaArvore = valorHuff;

                                    if (quantidadeDeBitsAUsar < 0)
                                    {

                                        ponteiroArquivoComprimido += 1;
                                        quantidadeDeBitsAUsar = 7;
                                    }



                                }

                            }

                         //   descricoes.Add("[Conta:"+i+"]"+descricaoDescomprimida.ToString());
                            descricoes.Add(descricaoDescomprimida.ToString());
                           // bufferTotal.Add(0);
                          //  bufferTotal.Add(0x45);
                          //  bufferTotal.Add(0x4E);
                          //  bufferTotal.Add(0x44);
                          //  bufferTotal.Add(0x3E);
                            enderecoDoPonteiro += 8;
                            descricaoDescomprimida = "";

                        }

                    }
                }



                return descricoes;

            }




        }

        public void Comprimir(string dirCardDesc, string dirCardHuff, string dirCardIndx)
        {
            List<No> tabelaDeNosOrdenada = ObtenhaNos(dirCardDesc).OrderBy(no => no.Frequencia).ToList();
            List<No> arvore = CrieArvore(tabelaDeNosOrdenada);
            Dictionary<char, string> tabelaDeCodigos = ObtenhaTabelaDeCodigos(arvore[0]);
            CrieArvoreNoFormatoDoJogo(tabelaDeCodigos, dirCardHuff);
            List<int> ponteiros = ComprimaComTabelaDeCodigosERetornePonteiros(tabelaDeCodigos, dirCardDesc);
            EscrevaNaTabelaDePOnteiros(ponteiros, dirCardIndx);

        }

        private void EscrevaNaTabelaDePOnteiros(List<int> ponteiros, string dirCardIndx)
        {
            int posicaoInicial = 4;

            using (BinaryWriter escrito = new BinaryWriter(File.Open(dirCardIndx, FileMode.Open)))
            {
                foreach (int ponteiro in ponteiros)
                {
                    escrito.BaseStream.Seek(posicaoInicial, SeekOrigin.Begin);
                    escrito.Write(ponteiro);
                    posicaoInicial += 8;
                }
            }
        }

        private List<No> ObtenhaNos(string diretorioCardDesc)
        {
            List<char> caracteresDoArquivo = ObtenhaCaracteresDoArquivo(diretorioCardDesc);

            var frequenciaDosNos = caracteresDoArquivo.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            List<No> frequenciaNos = new List<No>();

            foreach (var no in frequenciaDosNos)
            {
                frequenciaNos.Add(new No(no.Key, no.Value));
            }

            return frequenciaNos;
        }

        private List<char> ObtenhaCaracteresDoArquivo(string diretorioCardDesc)
        {
            Stream cardDesc = new MemoryStream(File.ReadAllBytes(diretorioCardDesc));

            List<char> caracteresDoArquivo = new List<char>();

            using (BinaryReader br = new BinaryReader(cardDesc))
            {
                while (br.BaseStream.Position < cardDesc.Length)
                {
                    caracteresDoArquivo.Add(Convert.ToChar(br.ReadInt16()));
                }
            }

            return caracteresDoArquivo;
        }

        private List<No> CrieArvore(List<No> tabelaDeNosOrdenada)
        {
            while (tabelaDeNosOrdenada.Count > 1)
            {
                No no1 = tabelaDeNosOrdenada[0];
                tabelaDeNosOrdenada[0] = null;

                No no2 = tabelaDeNosOrdenada[1];
                tabelaDeNosOrdenada[1] = null;

                tabelaDeNosOrdenada.Add(new No(no1.Frequencia + no2.Frequencia, no1, no2));
                tabelaDeNosOrdenada = RemovaNoNuloDaLista(tabelaDeNosOrdenada);
            }

            return tabelaDeNosOrdenada;
        }

        private List<No> RemovaNoNuloDaLista(List<No> tabelaDeNosOrdenada)
        {
            return tabelaDeNosOrdenada.Where(no => no != null).OrderBy(no => no.Frequencia).ToList();
        }

        private Dictionary<char, string> ObtenhaTabelaDeCodigos(No noRaiz)
        {
            var tabela = new Dictionary<char, string>();

            AuxilieConstrucaoDeTabela(noRaiz, "", tabela);

            return tabela;

        }

        private void AuxilieConstrucaoDeTabela(No no, string bit, Dictionary<char, string> tabela)
        {
            if (!no.EhFolha())
            {
                AuxilieConstrucaoDeTabela(no.NoEsquerdo, bit + "0", tabela);
                AuxilieConstrucaoDeTabela(no.NoDireito, bit + "1", tabela);
            }
            else
                tabela.Add(no.Caractere, bit);

        }

        private void CrieArvoreNoFormatoDoJogo(Dictionary<char, string> tabela, string dirCardHuff)
        {

            byte[] bufferNovaArvore = new byte[0x500];
            Stream novaArvore = new MemoryStream(bufferNovaArvore);
            MemoryStream backupNovaArvore = new MemoryStream();
            short proximoIndereco = 0;
            short ultimoEnderecoDeEscrita = 0;
            int padding = 0;
            short inderecoLeitura = 0;
            short resultadoValor = 0;
            bool caminhoAlternativo = false;

            using (BinaryWriter escritor = new BinaryWriter(novaArvore))
            {
                foreach (var caractere in tabela)
                {
                    for (int i = 0; i < caractere.Value.Length; i++)
                    {
                        backupNovaArvore = new MemoryStream();
                        escritor.BaseStream.Position = 0;
                        novaArvore.CopyTo(backupNovaArvore);

                        if (caractere.Value[i] == '1')
                        {
                            proximoIndereco += 2;
                            caminhoAlternativo = true;
                        }

                        escritor.BaseStream.Seek(proximoIndereco, SeekOrigin.Begin);
                        inderecoLeitura = (short)escritor.BaseStream.Position;
                        resultadoValor = VerificarSeTemvalorEscrito(backupNovaArvore, inderecoLeitura);

                        if (resultadoValor == 0)
                        {
                            if (caminhoAlternativo)
                            {
                                proximoIndereco = ultimoEnderecoDeEscrita;
                                caminhoAlternativo = false;
                            }
                            else
                            {
                                proximoIndereco += 4;
                            }

                            escritor.Write(proximoIndereco);

                        }
                        else
                        {
                            escritor.BaseStream.Seek(resultadoValor, SeekOrigin.Begin);
                            proximoIndereco = resultadoValor;
                        }

                    }

                    escritor.BaseStream.Seek(proximoIndereco, SeekOrigin.Begin);
                    escritor.Write(padding);
                    escritor.Write((short)caractere.Key);
                    ultimoEnderecoDeEscrita = (short)escritor.BaseStream.Position;
                    proximoIndereco = 0;

                }

                backupNovaArvore = new MemoryStream();
                escritor.BaseStream.Position = 0;
                novaArvore.CopyTo(backupNovaArvore);
            }

            byte[] arvoreFinal = new byte[ultimoEnderecoDeEscrita];
            Array.Copy(backupNovaArvore.ToArray(), 0, arvoreFinal, 0, arvoreFinal.Length);
            File.WriteAllBytes(dirCardHuff, arvoreFinal);

            // string caminho = tabela["$"];
            // bool resultadoTeste = TestarAvoreNova(arvoreFinal, caminho, '$');
        }

        private short VerificarSeTemvalorEscrito(Stream huff, int offset)
        {
            short resultado = 0;

            using (BinaryReader br = new BinaryReader(huff))
            {
                br.BaseStream.Seek(offset, SeekOrigin.Begin);
                resultado = br.ReadInt16();


            }

            return resultado;
        }

        private List<int> ComprimaComTabelaDeCodigosERetornePonteiros(Dictionary<char, string> tabelaDeCodigos, string diretorioCardDesc)
        {
            List<char> caracteresDoArquivo = ObtenhaCaracteresDoArquivo(diretorioCardDesc);

           /* StringBuilder tudaoooo = new StringBuilder(); 

            foreach (var item in caracteresDoArquivo)
            {
                tudaoooo.Append(item);
            }

            File.WriteAllText("_hufcomp.txt", tudaoooo.ToString());*/

            StringBuilder fluxoComprimido = new StringBuilder();

            fluxoComprimido.Append(",");

            foreach (var caractere in caracteresDoArquivo)
            {
                string codigoHuffman = tabelaDeCodigos[caractere];
                if (caractere == '\0')
                    fluxoComprimido.Append(codigoHuffman + ",");
                else
                    fluxoComprimido.Append(codigoHuffman);
            }

            Dictionary<StringBuilder, List<int>> fluxoEPonteiros = CalculePonteiros(fluxoComprimido.ToString());

            fluxoComprimido = fluxoEPonteiros.Keys.First();

            if (fluxoComprimido.Length % 8 != 0)
            {
                while (fluxoComprimido.Length % 8 != 0)
                {
                    fluxoComprimido.Append("0");
                }
            }

            byte[] arquivoComprimido = StringParaBytes(fluxoComprimido.ToString());
            File.WriteAllBytes(diretorioCardDesc, arquivoComprimido);
            List<int> ponteiros = fluxoEPonteiros.Values.First();

            return ponteiros;

        }

        private Dictionary<StringBuilder, List<int>> CalculePonteiros(string fluxo)
        {
            Dictionary<StringBuilder, List<int>> fluxoComprimidoComPonteiros = new Dictionary<StringBuilder, List<int>>();
            int contadorDeBits = 0;
            StringBuilder fluxoComprimido = new StringBuilder();
            List<int> ponteiros = new List<int>();

            for (int i = 0; i < fluxo.Length; i++)
            {

                if (fluxo[i] == ',')
                    ponteiros.Add(contadorDeBits);
                else
                {
                    contadorDeBits++;
                    fluxoComprimido.Append(fluxo[i]);
                }


            }

            fluxoComprimidoComPonteiros.Add(fluxoComprimido, ponteiros);

            return fluxoComprimidoComPonteiros;
        }

        private byte[] StringParaBytes(string fluxoEmBinario)
        {
            List<string> bytesEmbinario = (from Match binario in Regex.Matches(fluxoEmBinario, @"\d{8}")
                                           select binario.Value).ToList();

            List<byte> arquivoComprimido = new List<byte>();

            foreach (string binario in bytesEmbinario)
            {
                byte byteConvertido = Convert.ToByte(binario, 2);
                arquivoComprimido.Add(byteConvertido);
            }

            return arquivoComprimido.ToArray();
        }

        /* private bool TestarAvoreNova(byte[] arvore, string caminho, char letraAComparar)
         {
             MemoryStream ms = new MemoryStream(arvore);
             short valor = 0;
             char letra;
             using (BinaryReader br = new BinaryReader(ms))
             {
                 for (int i = 0; i < caminho.Length; i++)
                 {
                     if (caminho[i] == '0')
                     {
                         br.BaseStream.Seek(valor, SeekOrigin.Begin);
                     }
                     else
                     {
                         br.BaseStream.Seek(valor + 2, SeekOrigin.Begin);
                     }

                     valor = br.ReadInt16();
                 }

                 br.BaseStream.Seek(valor + 4, SeekOrigin.Begin);
                 letra = Convert.ToChar(br.ReadInt16());
             }

             if (letra == letraAComparar)
                 return true;
             else
                 return false;

         }
         */


    }

    public class No
    {

        public char Caractere { get; set; }
        public int Frequencia { get; set; }
        public No NoEsquerdo { get; set; }
        public No NoDireito { get; set; }

        public No(char caractere)
        {
            Caractere = caractere;
        }

        public No(char caractere, int frequencia)
        {
            Frequencia = frequencia;
            Caractere = caractere;
        }

        public No(int frequencia, No noEsquerdo, No noDireito)
        {
            Frequencia = frequencia;
            NoEsquerdo = noEsquerdo;
            NoDireito = noDireito;
        }

        public No(No noEsquerdo, No noDireito)
        {
            NoEsquerdo = noEsquerdo;
            NoDireito = noDireito;
        }

        public bool EhFolha()
        {
            return NoEsquerdo == null && NoDireito == null;
        }


    }
}
