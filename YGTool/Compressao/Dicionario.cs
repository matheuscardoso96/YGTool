using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YGTool.Compressao
{
    public class Dicionario
    {
        public byte[] TranduzirComDicionario(string dictEL, string cardIndxL, List<short> descDescmp ,string cardIntIDL, string cardNameL)
        {
            Stream cardIndx = new MemoryStream(File.ReadAllBytes(cardIndxL));
            Stream dictE = new MemoryStream(File.ReadAllBytes(dictEL));
            Stream cardIntID = new MemoryStream(File.ReadAllBytes(cardIntIDL));
            Stream cardName = new MemoryStream(File.ReadAllBytes(cardNameL));

            List<short> descricaoDescomprimida = descDescmp;


            int quantidaDePOnteiros = 0;
            int tamanhoDoHeader = 0;
            int tamanhoDaTabela = 0;
            int posicaoDoPonteiro = 0;
            int ponteiro = 0;
            int posicaoPonterioXCardIDInt = 0;
            int cardId = 0;
            int ponteiroCardName = 0;
            int posicaoPonteiroCardIndx = 0;
            int tamanho = descricaoDescomprimida.Count();
            bool resultadoRevisao = false;

            List<short> descricaoFinal = new List<short>();

            using (BinaryReader leitorDeCardName = new BinaryReader(cardName))
            {
                using (BinaryReader leitorDecardIntId = new BinaryReader(cardIntID))
                {
                    using (BinaryReader leitorDeCardIdx = new BinaryReader(cardIndx))
                    {
                        using (BinaryReader leitorDeDictE = new BinaryReader(dictE))
                        {
                            quantidaDePOnteiros = leitorDeDictE.ReadInt32();
                            tamanhoDoHeader = leitorDeDictE.ReadInt32();
                            tamanhoDaTabela = leitorDeDictE.ReadInt32();
                           
                            
                            while (resultadoRevisao == false)
                            {
                                int contador = 0;
                                descricaoFinal = new List<short>();

                                while (contador < tamanho)
                                {
                                    if (descricaoDescomprimida[contador] != 0x24)
                                    {
                                        descricaoFinal.Add(descricaoDescomprimida[contador]);
                                        contador++;
                                    }
                                    else if (descricaoDescomprimida[contador + 1] == 0x64)
                                    {

                                        string valorFinal = "";

                                        int miniconta = 0;

                                        if (tamanho == 0x72)
                                        {

                                        }

                                        contador += 2;

                                        while (miniconta < 3)
                                        {
                                            if (descricaoDescomprimida[contador] != 0x24)
                                            {
                                                valorFinal += descricaoDescomprimida[contador] - 0x30;
                                                miniconta++;
                                                contador++;
                                            }
                                            else { break; }


                                        }


                                        posicaoDoPonteiro = int.Parse(valorFinal) * 4;


                                        leitorDeDictE.BaseStream.Seek(posicaoDoPonteiro + tamanhoDoHeader, SeekOrigin.Begin);
                                        ponteiro = leitorDeDictE.ReadInt32();
                                        leitorDeDictE.BaseStream.Seek(ponteiro + tamanhoDaTabela, SeekOrigin.Begin);

                                        short valor = leitorDeDictE.ReadInt16();
                                        descricaoFinal.Add(valor);

                                        while (true)
                                        {
                                            valor = leitorDeDictE.ReadInt16();
                                            if (valor == 0)
                                            {
                                                break;
                                            }
                                            descricaoFinal.Add(valor);

                                        }



                                    }
                                    else
                                    {
                                        contador += 2;
                                        int valor1 = descricaoDescomprimida[contador] - 0x30;
                                        int valor2 = descricaoDescomprimida[contador + 1] - 0x30;
                                        int valor3 = descricaoDescomprimida[contador + 2] - 0x30;
                                        int valor4 = descricaoDescomprimida[contador + 3] - 0x30;
                                        string valorString = "" + valor1 + valor2 + valor3 + valor4;
                                        posicaoPonterioXCardIDInt = int.Parse(valorString);
                                        contador += 4;

                                        posicaoPonterioXCardIDInt = posicaoPonterioXCardIDInt * 2;
                                        posicaoPonterioXCardIDInt -= 0x1D10;

                                        leitorDecardIntId.BaseStream.Seek(posicaoPonterioXCardIDInt, SeekOrigin.Begin);
                                        cardId = leitorDecardIntId.ReadInt16();
                                        posicaoPonteiroCardIndx = cardId << 3;

                                        leitorDeCardIdx.BaseStream.Seek(posicaoPonteiroCardIndx, SeekOrigin.Begin);
                                        ponteiroCardName = leitorDeCardIdx.ReadInt32();

                                        leitorDeCardName.BaseStream.Seek(ponteiroCardName, SeekOrigin.Begin);

                                        short valor = leitorDeCardName.ReadInt16();
                                        descricaoFinal.Add(valor);

                                        while (true)
                                        {
                                            valor = leitorDeCardName.ReadInt16();
                                            if (valor == 0)
                                            {
                                                break;
                                            }
                                            descricaoFinal.Add(valor);
                                        }



                                    }



                                }

                                descricaoDescomprimida = descricaoFinal;
                                tamanho = descricaoDescomprimida.Count();
                                resultadoRevisao = ReviewDaDescricao(descricaoFinal);
                                
                            }
                            
                        }
                    }
                }
            }

            return ConvertaParaByte(descricaoFinal);
        }

        private byte[] ConvertaParaByte(List<short> descricaoDescomprimida)
        {
            byte[] conv = new byte[descricaoDescomprimida.Count * 2];
            MemoryStream convertido = new MemoryStream(conv);

            using (BinaryWriter bw = new BinaryWriter(convertido))
            {
                foreach (short valor in descricaoDescomprimida)
                {
                    bw.Write(valor);
                }
            }
            

            return convertido.ToArray();
        }

        public bool ReviewDaDescricao(List<short> descricaoFinal)
        {
            int tamanho = descricaoFinal.Count;
            int contador = 0;

            while (contador < tamanho)
            {

                if (descricaoFinal[contador] == 0x24 && descricaoFinal[contador + 1] == 0x64 || descricaoFinal[contador] == 0x24 && descricaoFinal[contador + 1] == 0x6D)
                {
                    return false;
                }

                contador++;
            }

            return true;

        }

        public string CriarUmDicionario(string texto, string dirDict)
        {                                                     
            List<Termo> termosFinais = new List<Termo>();                                  
            List<Termo> termos = ObtenhaTermosMaisUsados(texto);
            List<Termo> termosNaoDuplicados = RemovaDuplicatas(termos);
            termosNaoDuplicados.Sort();
            termosFinais = termosNaoDuplicados.Take(1000).ToList();
            Dictionary<string, string> dicionarioTermosFinal = new Dictionary<string, string>();
            return ConverterComDicionario(dicionarioTermosFinal, texto);
        }

        public Dictionary<string, string> CriarNovoDict(string dirDict, List<Termo> termosFinais)
        {
            Dictionary<string, string> dicionarioTermosFinal = new Dictionary<string, string>();
            int tamanhoHeader = 0xC;
            int contador = 0;
            int quatidadeDeTermos = 1000;
            int ponteiro = 0;
            byte[] tabelaDePonteiros = new byte[(quatidadeDeTermos * 4) + tamanhoHeader + 4];
            List<byte[]> termosEmbyte = new List<byte[]>();
            MemoryStream tabela = new MemoryStream(tabelaDePonteiros);
            MemoryStream tabelaFinal = new MemoryStream();
            using (BinaryWriter bw = new BinaryWriter(tabela))
            {
                bw.Write((int)termosFinais.Count());
                bw.Write(tamanhoHeader);
                bw.Write(tabelaDePonteiros.Length);
                contador = 0;

                foreach (var termo in termosFinais)
                {
                    bw.Write(ponteiro);
                    string termoo = termo.Texto + "\0";
                    byte[] termoEmbyte = Encoding.Unicode.GetBytes(termoo);
                    termosEmbyte.Add(termoEmbyte);
                    string id = contador.ToString().PadLeft(3, '0');
                    dicionarioTermosFinal.Add("$d" + id, termoo);
                    ponteiro += termoEmbyte.Length;
                    contador++;


                }

                bw.BaseStream.Position = 0;
                tabela.CopyTo(tabelaFinal);
            }

            byte[] arquivoFinal = new byte[tabelaDePonteiros.Length + ponteiro];
            MemoryStream final = new MemoryStream(arquivoFinal);

            using (BinaryWriter bw = new BinaryWriter(final))
            {
                bw.Write(tabelaFinal.ToArray());

                foreach (var item in termosEmbyte)
                {
                    bw.Write(item);
                }

                Array.Copy(final.ToArray(), arquivoFinal, final.Length);
            }

            File.WriteAllBytes(dirDict, arquivoFinal.ToArray());

            return dicionarioTermosFinal;
        }

        public string ConverterComDicionario(Dictionary<string, string> dicionario, string texto)
        {
            foreach (var termo in dicionario)
            {
                texto = texto.Replace(termo.Value.Replace("\0", ""), termo.Key);
            }
            return texto;
        }

        public List<Termo> ObtenhaTermosMaisUsados(string texto)
        {
            List<Termo> termos = new List<Termo>();
            int ocorrencias = 0;
            string[] frases = texto.Split(new[] { "\0" }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            foreach (string frase in frases)
            {
                string parteDaFrase = "";
                int contaEspaco = 0;

                for (int i = 0; i < frase.Length; i++)
                {
                    parteDaFrase += frase[i];

                    if (frase[i] == ' ')
                        contaEspaco += 1;

                    if (contaEspaco == 2)
                    {
                        parteDaFrase = Regex.Escape(parteDaFrase);
                        ocorrencias = Regex.Matches(texto,parteDaFrase).Count;
                        parteDaFrase = Regex.Unescape(parteDaFrase);

                        if (ocorrencias > 2)
                        {                            
                            termos.Add(new Termo(ocorrencias, parteDaFrase));
                            contaEspaco = 0;
                            parteDaFrase = string.Empty;
                        }
                        else
                            contaEspaco = 1;


                    }
                }
            }

            return termos;
        }

        public List<Termo> RemovaDuplicatas(List<Termo> termos)
        {
            List<Termo> naoDuplicados = new List<Termo>();

            foreach (var termo in termos)
            {
                bool existe = naoDuplicados.Any(x => x.Texto.Contains(termo.Texto));

                if (!existe)
                {
                    naoDuplicados.Add(termo);
                }
            }

            return naoDuplicados;
        }
        
    }

    public class Termo: IComparable<Termo>
    {
        public int Quatidade { get; set; }
        public string Texto { get; set; }

        public Termo(int quatidade, string texto)
        {
            Quatidade = quatidade;
            Texto = texto;
        }

        public int CompareTo(Termo other)
        {
            if (Quatidade > other.Quatidade)
            {
                return -1;
            }
            else if (Quatidade < other.Quatidade)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
