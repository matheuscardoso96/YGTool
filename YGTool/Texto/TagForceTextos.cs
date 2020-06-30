using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGTool.Texto
{
    public class TagForceTextos
    {
        public void ExportarParaTxtPonteirosInternos(string dirTextoBin)
        {
            Stream bin = new MemoryStream(File.ReadAllBytes(dirTextoBin));

            int quantidaDePonteiros = 0;
            int tamanhoDoHeader = 0;
            int tamanhoDaTabela = 0;
            int posicaoTabela = 0;
            int ponteiro = 0;
            List<string> textos = new List<string>();

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
                    ponteiro = br.ReadInt32() + tamanhoDaTabela;                   

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
                    textos.Add("<" + "PONTEIRO: " + posicaoTabela + ">\n" + "<TEXTO>" + textoEmString + "<TEXTO/>\n" + "<FIM/>\n\n" );
                    posicaoTabela += 4;
                }


                File.WriteAllLines(dirTextoBin.Replace(".bin", ".txt"), textos);
            }
        }
    }
}
