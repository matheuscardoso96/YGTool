using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGTool.Som
{
    public class At3
    {
        public void ProcurarAt3(string dirSndDat)
        {
            int header = 0x46464952;
            List<string> sndInfo = new List<string>();
            string dirSnd = dirSndDat.Replace(".bin", "");
            Directory.CreateDirectory(dirSnd);
            int tamanhoArq = (int)new FileInfo(dirSndDat).Length;
            using (BinaryReader br = new BinaryReader(File.Open(dirSndDat,FileMode.Open)))
            {
                int contador = 0;

                while (br.BaseStream.Position < tamanhoArq)
                {
                    int valor = br.ReadInt32();

                    if (valor == header)
                    {
                        int indexInt = (int)br.BaseStream.Position - 4;
                        int tamanhoEhp = br.ReadInt32();
                        br.BaseStream.Position = indexInt;
                        byte[] ehpEmbyte = br.ReadBytes(tamanhoEhp);
                        string index = indexInt + "";
                        string nomeAt3 = contador.ToString().PadLeft(5, '0');
                        contador++;
                        File.WriteAllBytes(dirSnd + "\\" + nomeAt3 + ".at3", ehpEmbyte);
                        sndInfo.Add(index + "," + dirSnd + "\\" + nomeAt3 + ".at3");

                    }
                }

                File.WriteAllLines(dirSndDat.Replace(".bin", ".txt"), sndInfo);


            }
        }
    }
}
