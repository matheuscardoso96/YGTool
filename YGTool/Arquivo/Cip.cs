using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGTool.Arquivo
{
    public class Cip
    {
        public async Task<int> ProcurarGim(string dirSndDat)
        {
            int header = 0x2E47494D;
            List<string> sndInfo = new List<string>();
            string dirSnd = dirSndDat.Replace(".cip", "");
            Directory.CreateDirectory(dirSnd);
            int tamanhoArq = (int)new FileInfo(dirSndDat).Length;
            int contador = 0;

            await Task.Run(() =>
            {
                
                using (BinaryReader br = new BinaryReader(File.Open(dirSndDat, FileMode.Open)))
                {
                    
                    while (br.BaseStream.Position < tamanhoArq)
                    {
                        int valor = br.ReadInt32();

                        if (valor == header)
                        {
                            int indexInt = (int)br.BaseStream.Position - 4;
                            br.BaseStream.Position += 0x10;
                            int tamanhoDoGim = (br.ReadInt32() + 0x10) - 0x80;
                            br.BaseStream.Position = indexInt;
                            byte[] ehpEmbyte = br.ReadBytes(tamanhoDoGim);
                            string index = indexInt + "";
                            string nomeAt3 = contador.ToString().PadLeft(5, '0');
                            contador++;
                            File.WriteAllBytes(dirSnd + "\\" + nomeAt3 + ".gim", ehpEmbyte);
                            sndInfo.Add(index + "," + dirSnd + "\\" + nomeAt3 + ".gim");


                        }
                    }

                    File.WriteAllLines(dirSndDat.Replace(".cip", ".txt"), sndInfo);

                }



            });

            return contador;
        }


        public void InsiraNoCip(string dirSndDat)
        {
            string[] arquivos = File.ReadAllLines(dirSndDat);
         
                using (BinaryWriter writer = new BinaryWriter(File.Open(dirSndDat.Replace(".txt",".cip"), FileMode.Open)))
                {

                    for (int i = 0; i < arquivos.Length; i++)
                    {
                        string info = arquivos[i];

                        if (info != "")
                        {
                            string[] fileInfo = info.Split(',');
                            int posicaoArquivo = int.Parse(fileInfo[0]);
                            string diretorio = fileInfo[1];
                            byte[] arquivo = File.ReadAllBytes(diretorio);
                            writer.BaseStream.Seek(posicaoArquivo,SeekOrigin.Begin);
                            writer.Write(arquivo);

                        }
                    }

                    

                }

  

            
        }
    }
}
