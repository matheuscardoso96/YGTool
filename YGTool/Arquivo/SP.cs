using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGTool.Arquivo
{
    public class SP
    {
        public void ExporteAudiosDeDentroDoPacote(string dirSp)
        {
            List<string> sndInfo = new List<string>();
            string novoDirSp = dirSp.Replace(Path.GetFileName(dirSp), "") + "SPS_Extraidos\\";

            if (!Directory.Exists(novoDirSp))
            {
                Directory.CreateDirectory(novoDirSp);
            }

            

            int tamanhoArq = (int)new FileInfo(dirSp).Length;
            int valorBase = 0;
            int tamanhoDaTabela = 0;
            int quantidaDeAudios = 0;
            int tamanhoDoAudioEmBytes = 0;
            int posicaoTabela = 0x30;
            int ponteiro = 0;
            bool ehAtrac = true;
            string descritivo = "";
            int indexBase = 0x14;
            using (BinaryReader br = new BinaryReader(File.Open(dirSp, FileMode.Open)))
            {
                br.BaseStream.Seek(0x20, SeekOrigin.Begin);
                byte[] desc = br.ReadBytes(16);
                descritivo = Encoding.ASCII.GetString(desc);

                if (descritivo.Contains("YU"))
                {
                    indexBase = 0xC;
                }

                br.BaseStream.Seek(indexBase, SeekOrigin.Begin);
                valorBase = br.ReadInt32();
                tamanhoDaTabela = valorBase - 0x30;
                quantidaDeAudios = tamanhoDaTabela / 32;

                int contador = 0;

                while (br.BaseStream.Position < tamanhoArq)
                {

                    ushort EhHeader = br.ReadUInt16();

                    if (EhHeader == 0x4952)
                    {
                        int posTamanho = (int)br.BaseStream.Position - 6;
                        br.BaseStream.Position = br.BaseStream.Position + 6;
                        EhHeader = br.ReadUInt16();
                        if (EhHeader == 0x4157)
                        {
                            br.BaseStream.Position = posTamanho;
                            tamanhoDoAudioEmBytes = br.ReadInt32();
                            byte[] riff = br.ReadBytes(tamanhoDoAudioEmBytes);

                            File.WriteAllBytes(novoDirSp + "\\" + Path.GetFileName(dirSp).Replace(".sp", "_") + contador + ".at3", riff);
                            sndInfo.Add(tamanhoDoAudioEmBytes + "," + novoDirSp + "\\" + Path.GetFileName(dirSp).Replace(".sp", "_") + contador + ".at3");
                            contador++;
                        }
                    }


                }               


                File.WriteAllLines(dirSp.Replace(".sp", ".txt"), sndInfo);
                

                
            }



        }

        public void SpRenamer(string dirSps)
        {
            List<string> sps = Directory.GetFiles(dirSps,"*.sp").ToList();
            if (!Directory.Exists(dirSps + "\\Sps_renomeados\\"))
            {
                Directory.CreateDirectory(dirSps + "\\Sps_renomeados\\");
            }
            
            foreach (var item in sps)
            {
                ushort id = 0;
                using (BinaryReader br = new BinaryReader(File.Open(item,FileMode.Open)))
                {
                    br.BaseStream.Position = 2;
                    id =(ushort)(br.ReadUInt16() - 1);
                }

                byte[] ssp = File.ReadAllBytes(item);
                int nomeArquivo = int.Parse(Path.GetFileName(item.Replace(".sp",""))) - 1;
                MemoryStream tempSp = new MemoryStream(ssp);
                using (BinaryWriter bw = new BinaryWriter(tempSp))
                {
                    bw.BaseStream.Position = 2;
                    bw.Write(id);
                    ssp = tempSp.ToArray();
                    File.WriteAllBytes(dirSps + "\\Sps_renomeados\\" + nomeArquivo + ".sp", ssp);
                }

            }
        }
    }
}
