using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace YGTool.Compressao
{
    public class Dicionario
    {
        public List<short> TranduzirComDicionario(string dictEL, List<short> descricaoDescomprimida, string cardIndxL, string cardIntIDL, string cardNameL)
        {
            Stream cardIndx = new MemoryStream(File.ReadAllBytes(cardIndxL));
            Stream dictE = new MemoryStream(File.ReadAllBytes(dictEL));
            Stream cardIntID = new MemoryStream(File.ReadAllBytes(cardIntIDL));
            Stream cardName = new MemoryStream(File.ReadAllBytes(cardNameL));

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
                            int contador = 0;

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

                                //review

                            }
                        }
                    }
                }
            }


            return descricaoFinal;
        }

        public bool ReviewDaDescricao(List<short> descricaoFinal)
        {
            int tamanho = descricaoFinal.Count;
            int contador = 0;

            while (contador < tamanho)
            {

                if (descricaoFinal[contador] == 0x24 && descricaoFinal[contador + 1] == 0x64 || descricaoFinal[contador] == 0x24 && descricaoFinal[contador + 1] == 0x6D)
                {
                    return true;
                }

                contador++;
            }

            return false;

        }
    }
}
