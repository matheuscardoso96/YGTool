using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGTool.Arquivo
{
    public class SndDat
    {
        public void ExportePacotesDeAudio(string dirSndDat, string eboot, int qtdPonteiros, int posicaoEboot, int posTabelaTamanhoDePacotes)
        {
            
            List<string> sndInfo = new List<string>();
            string dirSnd = dirSndDat.Replace(".bin", "");
            Directory.CreateDirectory(dirSnd);
            int tamanhoArq = (int)new FileInfo(dirSndDat).Length;
            List<int> ponteiros = new List<int>();
            sndInfo.Add("Eboot:"+ posicaoEboot + ":" + posTabelaTamanhoDePacotes);
            using (BinaryReader br = new BinaryReader(File.Open(eboot,FileMode.Open)))
            {
                int verificaHeaderElf = br.ReadInt32();

                if (verificaHeaderElf != 0x464C457F)
                {
                    throw new Exception("Não é um arquivo Eboot válido ou está criptografado!");
                }

                br.BaseStream.Seek(posicaoEboot,SeekOrigin.Begin);

                for (int i = 0; i < qtdPonteiros; i++)
                {
                     ponteiros.Add(br.ReadInt32());
                }
            }

            using (BinaryReader br = new BinaryReader(File.Open(dirSndDat, FileMode.Open)))
            {



                for (int i = 0; i < ponteiros.Count; i++)
                {
                    br.BaseStream.Position = ponteiros[i];

                    br.BaseStream.Seek(br.BaseStream.Position + 4, SeekOrigin.Begin);
                    int numerodoPacote = i;
                    int tamanhoDoPacote = br.ReadInt32();
                    int indexInt = (int)br.BaseStream.Position - 8;
                    br.BaseStream.Position = indexInt;
                    byte[] pacoteDeAudio = br.ReadBytes(tamanhoDoPacote);
                    string index = indexInt + "";
                    File.WriteAllBytes(dirSnd + "\\" + numerodoPacote.ToString() + ".sp", pacoteDeAudio);
                    sndInfo.Add(posicaoEboot + "," + dirSnd + "\\" + numerodoPacote.ToString() + ".sp");
                    posicaoEboot += 4;
                }
                    
                    
                

                File.WriteAllLines(dirSndDat.Replace(".bin", ".txt"), sndInfo);


            }
        }

        public void RemontarSdat(string dirSndDat, string dirEboot)
        {

            List<string> listaDeSps = new List<string>();
            listaDeSps = File.ReadAllLines(dirSndDat).ToList();
            int offsetTAbela = int.Parse(listaDeSps[0].Split(':')[1]);
            int offsetTabelaTamanhoPacotes = int.Parse(listaDeSps[0].Split(':')[2]);
            listaDeSps = listaDeSps.Skip(1).Take(listaDeSps.Count - 1).ToList();
            File.Create(dirSndDat.Replace(".txt", ".bin")).Close();
            List<int> ponteiros = new List<int>();
            List<int> tamanhosDePacote = new List<int>();
            int ponteiro = 0;
            using (BinaryWriter bw = new BinaryWriter(File.Open(dirSndDat.Replace(".txt", ".bin"), FileMode.Append)))
            {

                foreach (var dirSp in listaDeSps)
                {
                    ponteiros.Add(ponteiro);
                    byte[] sp = File.ReadAllBytes(dirSp.Split(',').Last());
                    tamanhosDePacote.Add(sp.Length);
                    bw.Write(sp);
                    ponteiro += sp.Length;
                }

                
            }


           // ConfiraPOnteiros(ponteiros, dirSndDat.Replace(".txt", ".bin"));

            AtualizePonteirosNoEboot(dirEboot, offsetTAbela, ponteiros, offsetTabelaTamanhoPacotes, tamanhosDePacote);
        }

        public void ConfiraPOnteiros(List<int> ponteiros, string dirSdtNovo)
        {

            using (BinaryReader br = new BinaryReader(File.Open(dirSdtNovo,FileMode.Open)))
            {
                foreach (var item in ponteiros)
                {
                    br.BaseStream.Seek(item,SeekOrigin.Begin);
                    ushort header = br.ReadUInt16();
                    if (header != 0x5053)
                    {
                        throw new Exception("Erradooooo");
                    }
                }
            }
        }

        public void AtualizePonteirosNoEboot(string dirEboot,int offsetTabela, List<int> ponteiros, int offsetTabelaTamanhosDePacote, List<int> tamanhosPacote)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(dirEboot,FileMode.Open)))
            {
                foreach (int ponteiro in ponteiros)
                {
                    bw.BaseStream.Seek(offsetTabela, SeekOrigin.Begin);
                    bw.Write(ponteiro);
                    offsetTabela += 4;
                }

                foreach (int tamanhoDePacote in tamanhosPacote)
                {
                    bw.BaseStream.Seek(offsetTabelaTamanhosDePacote, SeekOrigin.Begin);
                    bw.Write(tamanhoDePacote);
                    offsetTabelaTamanhosDePacote += 16;
                }
            }
        }
    }
}
