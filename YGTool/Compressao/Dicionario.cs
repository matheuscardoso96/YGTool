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
        public List<string> TranduzirComDicionario(string dictEL, string cardIndxL, List<string> descricoes, string cardIntIDL, string cardNameL, TagForce tagForce)
        {
            Stream cardIndx = new MemoryStream(File.ReadAllBytes(cardIndxL));
            Stream dictE = new MemoryStream(File.ReadAllBytes(dictEL));
            Stream cardIntID = new MemoryStream(File.ReadAllBytes(cardIntIDL));
            Stream cardName = new MemoryStream(File.ReadAllBytes(cardNameL));



            int quantidaDePOnteiros = 0;
            int tamanhoDoHeader = 0;
            int tamanhoDaTabela = 0;
            int contta = 0;
            List<string> descricoesFinaias = new List<string>();


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

                            for (int i = 0; i < descricoes.Count; i++)
                            {
                                contta = i;

                                string descricaoComCodigos = descricoes[i];

                                string descricaoSemCodigos = RemoverCodigos(descricaoComCodigos, tamanhoDoHeader, tamanhoDaTabela, tagForce, leitorDeDictE, leitorDecardIntId, leitorDeCardIdx, leitorDeCardName);

                                descricoesFinaias.Add(descricaoSemCodigos);

                            }



                        }
                    }
                }
            }





            return descricoesFinaias;
        }

        private string RemoverCodigos(string descricaoComCodigos, int tamanhoDoHeader, int tamanhoDaTabela, TagForce tagForce, BinaryReader leitorDeDictE, BinaryReader leitorDecardIntId, BinaryReader leitorDeCardIdx, BinaryReader leitorDeCardName)
        {
            int contador = 0;

            while (descricaoComCodigos.Contains("$"))
            {
                if (contador == descricaoComCodigos.Length)
                {
                    contador = 0;
                }
                if (descricaoComCodigos[contador] == '$')
                {
                    contador++;
                    string valor = "";
                    char identificador = descricaoComCodigos[contador];
                    if (identificador == 'd' || identificador == 'm')
                    {
                        contador++;
                        int contaTemp = identificador == 'd' ? 3 : 4;

                        for (int i = 0; i < contaTemp; i++)
                        {
                            if (descricaoComCodigos[contador] == '$')
                            {
                                break;
                            }
                            valor += descricaoComCodigos[contador];
                            contador++;
                        }

                        if (valor == "" || valor.Length < 3)
                        {
                            continue;
                        }

                        if (identificador == 'd')
                        {
                            descricaoComCodigos = descricaoComCodigos.Replace("$d" + valor, ObtenhaTextoDoDicionario(leitorDeDictE, valor, tamanhoDoHeader, tamanhoDaTabela));
                        }
                        else
                        {
                            int posicaoCardIdx = ObtenhaPosicaoPonteiroComCardIntId(leitorDecardIntId, valor, tagForce);
                            string nomeDaCarta = ObtenhaCardName(leitorDeCardIdx, leitorDeCardName, posicaoCardIdx);
                            descricaoComCodigos = descricaoComCodigos.Replace("$m" + valor, "<M" + valor + ">" + nomeDaCarta + "<M>");

                        }

                    }


                }

                contador++;
            }

            return descricaoComCodigos;
        }

        private string ObtenhaTextoDoDicionario(BinaryReader leitorDeDictE, string valor, int tamanhoDoHeader, int tamanhoDaTabela)
        {
            string sb = "";

            int posicaoDoPonteiro = Convert.ToInt32(valor) * 4;
            leitorDeDictE.BaseStream.Seek(posicaoDoPonteiro + tamanhoDoHeader, SeekOrigin.Begin);
            int ponteiro = leitorDeDictE.ReadInt32();
            leitorDeDictE.BaseStream.Seek(ponteiro + tamanhoDaTabela, SeekOrigin.Begin);

            while (true)
            {
                string letra = Encoding.Unicode.GetString(leitorDeDictE.ReadBytes(2));

                if (letra.Contains("\0"))
                {
                    break;
                }

                sb += letra;

            }


            return sb.ToString();

        }

        private int ObtenhaPosicaoPonteiroComCardIntId(BinaryReader leitorDecardIntId, string valor, TagForce tagForce)
        {

            int posicaoPonterioXCardIDInt = int.Parse(valor) * 2; ;

            if (tagForce == TagForce.TagForce4 || tagForce == TagForce.TagForce5)
            {
                if (posicaoPonterioXCardIDInt >= 0x1C20)
                {
                    posicaoPonterioXCardIDInt -= 0x1C20;
                }
            }
            else if (tagForce == TagForce.TagForce6)
            {
                if (posicaoPonterioXCardIDInt >= 0x1B06)
                {
                    posicaoPonterioXCardIDInt -= 0x1B06;
                }
            }
            else
            {
                if (posicaoPonterioXCardIDInt >= 0x1D10)
                {
                    posicaoPonterioXCardIDInt -= 0x1D10;
                }
            }

            leitorDecardIntId.BaseStream.Seek(posicaoPonterioXCardIDInt, SeekOrigin.Begin);
            int posicaoPonteiro = leitorDecardIntId.ReadInt16() << 3;

            return posicaoPonteiro;

        }

        private string ObtenhaCardName(BinaryReader leitorDeCardIdx, BinaryReader leitorDeCardName, int posicaoPonteiroCardIndx)
        {
            string sb = "";
            leitorDeCardIdx.BaseStream.Seek(posicaoPonteiroCardIndx, SeekOrigin.Begin);
            int ponteiroCardName = leitorDeCardIdx.ReadInt32();

            leitorDeCardName.BaseStream.Seek(ponteiroCardName, SeekOrigin.Begin);


            while (true)
            {
                string letra = Encoding.Unicode.GetString(leitorDeCardName.ReadBytes(2));

                if (letra.Contains("\0"))
                {
                    break;
                }

                sb += letra;

            }


            return sb.ToString();

        }

       

        public string ObterTextoDoDicionarioESubstituir(string dir, string descricoesCarta)
        {

            Dictionary<string, string> termos = RetorneListaDeCodigos(dir);
            StringBuilder stringBuilder = new StringBuilder(descricoesCarta);
            foreach (var item in termos)
            {
                stringBuilder.Replace(item.Value, item.Key);
           
            }

            return stringBuilder.ToString();
        }

        private Dictionary<string, string> RetorneListaDeCodigos(string dir)
        {
            string dic = File.ReadAllText(dir.Replace(".bin",".txt")).Replace("<PONTEIRO", "~<PONTEIRO");
            string[] dicSplit = dic.Split('~');
            Dictionary<string, string> termos = new Dictionary<string, string>();
            int contador = 0;
            for (int i = 0; i < dicSplit.Length; i++)
            {
               
                if (!dicSplit[i].Contains("<TEXTO>"))
                {
                    continue;
                }

                string termo = dicSplit[i].Replace("<TEXTO>", "<TEXTO>|").Replace("<TEXTO/>", "|<TEXTO/>").Split('|')[1].Replace("<NULL>","");

                if (termo.Length == 0)
                {
                    throw new Exception("Texto vazio encontrado no dicionário\r\n" + dicSplit[i]);
                }
                termos.Add("$d" + contador.ToString("000"), termo);
                contador++;

            }
            return termos;


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

    public enum TagForce
    {
        TagForce2,
        TagForce3,
        TagForce4,
        TagForce5,
        TagForce6


    }
}
