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
        public byte[] TranduzirComDicionario(string dictEL, string cardIndxL, List<short> descDescmp, string cardIntIDL, string cardNameL)
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

            int contador = 0;

            try
            {
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
                                    contador = 0;
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

                                            int teste = 0;

                                            if (int.TryParse(valorFinal, out teste))
                                            {

                                            }
                                            else
                                            {
                                                string erro = "errrrrr";
                                            }
                                            if (contador == 3109)
                                            {

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
                                        else if (descricaoDescomprimida[contador + 1] == 0x6D)
                                        {
                                            contador += 2;
                                            if (contador == 0x6F0E)
                                            {

                                            }
                                            int miniconta = 0;
                                            string valorFinal = "";
                                            descricaoFinal.Add(0x3C);
                                            descricaoFinal.Add(0x4D);

                                            while (miniconta < 4)
                                            {
                                                if (descricaoDescomprimida[contador] != 0x24)
                                                {
                                                    descricaoFinal.Add(descricaoDescomprimida[contador]);
                                                    valorFinal += descricaoDescomprimida[contador] - 0x30;
                                                    miniconta++;
                                                    contador++;
                                                }
                                                else { break; }


                                            }


                                            if (valorFinal == "")
                                            {
                                                descricaoFinal.Add(0x24);
                                                descricaoFinal.Add(0x6D);

                                                continue;
                                            }

                                            descricaoFinal.Add(0x3E);
                                            string valorString = valorFinal;

                                            int teste = 0;
                                            if (int.TryParse(valorString, out teste))
                                            {

                                            }
                                            else
                                            {
                                                string erro = "errrrrr";
                                            }


                                            posicaoPonterioXCardIDInt = int.Parse(valorString);


                                            posicaoPonterioXCardIDInt = posicaoPonterioXCardIDInt * 2;

                                            if (posicaoPonterioXCardIDInt >= 0x1D10)
                                            {
                                                posicaoPonterioXCardIDInt -= 0x1D10;
                                            }



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


                                            descricaoFinal.Add(0x3C);
                                            descricaoFinal.Add(0x4D);
                                            descricaoFinal.Add(0x3E);

                                        }
                                        else
                                        {
                                            descricaoFinal.Add(descricaoDescomprimida[contador]);
                                            contador++;
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
            }
            catch (Exception)
            {

                int erro = contador;
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

            /* byte[] descc = new byte[descDescmp.Count * 2];
             MemoryStream mm = new MemoryStream(descc);

             using (BinaryWriter bw = new BinaryWriter(mm))
             {
                 foreach (var item in descDescmp)
                 {
                     bw.Write(item);
                 }

                 descc = mm.ToArray();
             }*/

            File.WriteAllBytes("desc.bin", convertido.ToArray());
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

            List<Termo> termos = ObtenhaTermosMaisUsados(texto);
            List<Termo> termosOrdenadosPorTamanho = termos.OrderByDescending(o => o.Texto.Length).ThenBy(o => o.Quatidade).ToList();



            var dicionarioTermosFinal = CriarNovoDict(dirDict, termosOrdenadosPorTamanho);

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
                    dicionarioTermosFinal.Add("$d" + id, termoo.Replace("\0", ""));
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
            //
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
            List<Termo> termosTemp = new List<Termo>();
            List<Termo> grupoDeTermo = new List<Termo>();
            List<string> partes = new List<string>();
            StringBuilder build = new StringBuilder();
            // int[] qtdGrupo = new int[] { 140, 130, 116, 84, 76, 60, 56, 42, 30, 30, 26, 24, 22, 20, 18, 16, 12, 10, 10, 8, 8, 6, 4, 4, 14, 24, 10};
            int[] qtdGrupo = new int[] { 140, 106, 102, 80, 80, 76, 50, 46, 40, 38, 36, 34, 20, 18, 16, 14, 12, 10, 10, 8, 8, 6, 4, 4, 14, 14, 14 };
            //int[] qtdGrupo = new int[] { 190, 155, 120, 90, 77, 60, 55, 40, 38, 34, 26, 24, 15, 14, 12, 9, 7, 8, 5, 5, 4, 3, 3, 2, 2, 1, 1 };
            int[] tamanhoElementos = new int[] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 31, 34 };
            int conta = qtdGrupo.Length - 1;
            int objetivo = tamanhoElementos[conta];
            int qtdGrupoAlvo = qtdGrupo[conta];

            foreach (string frase in frases)
            {

                string parteDaFrase = "";
                int c = 0;


                while (c < frase.Length)
                {
                    build.Append(frase[c]);
                    c++;

                    if (build.Length == objetivo)// objetivo)
                    {
                        parteDaFrase = build.ToString();

                        parteDaFrase = Regex.Escape(parteDaFrase);
                        ocorrencias = Regex.Matches(texto, parteDaFrase).Count;
                        parteDaFrase = Regex.Unescape(parteDaFrase);
                        if (ocorrencias > 2)
                        {
                            bool jaExisteTermo = termos.Any(o => o.Texto.Contains(parteDaFrase));
                            if (!jaExisteTermo)
                            {
                                termosTemp.Add(new Termo(ocorrencias, parteDaFrase));
                            }


                        }


                        parteDaFrase = string.Empty;
                        build.Clear();

                        termosTemp = RemovaDuplicatas(termosTemp);

                        if (termosTemp.Count > 10)
                        {
                            termosTemp.Sort();
                            grupoDeTermo.Add(termosTemp[0]);
                            grupoDeTermo.Add(termosTemp[1]);
                            texto = texto.Replace(termosTemp[0].Texto, " ");
                            texto = texto.Replace(termosTemp[1].Texto, " ");
                            termosTemp.Clear();

                            if (grupoDeTermo.Count == qtdGrupoAlvo)
                            {
                                termos.AddRange(grupoDeTermo);

                                conta--;
                                if (conta > -1)
                                {
                                    objetivo = tamanhoElementos[conta];
                                    qtdGrupoAlvo = qtdGrupo[conta];
                                    grupoDeTermo.Clear();
                                }


                                if (termos.Count == 1000)
                                {
                                    break;
                                }
                            }

                        }
                    }

                    if (termos.Count == 1000)
                    {
                        break;
                    }
                }

                if (termos.Count == 1000)
                {
                    break;
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

    public class Termo : IComparable<Termo>
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
