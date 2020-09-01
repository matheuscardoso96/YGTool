using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGTool.Compressao
{
    public class Gzip
    {
        public void Descomprimir(string diretorio)
        {

            Stream arquivoComprimido = new MemoryStream(File.ReadAllBytes(diretorio));

            using (FileStream decompressedFileStream = File.Create(diretorio.Replace(".gz", "")))
            {
                using (GZipStream decompressionStream = new GZipStream(arquivoComprimido, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(decompressedFileStream);

                }
            }
        }


        public void Comprimir(string diretorio)
        {
            string nomeArquivo = Path.GetFileName(diretorio);
            Stream arquivoDescomprimido = new MemoryStream(File.ReadAllBytes(diretorio));
            MemoryStream novoComp = null;

            using (MemoryStream outFile = new MemoryStream())
            {
                using (MemoryStream inFile = new MemoryStream(File.ReadAllBytes(diretorio)))
                using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                {
                    inFile.CopyTo(Compress);
                }

                

                int unixTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                MemoryStream comp = new MemoryStream(outFile.ToArray());


                using (BinaryReader br = new BinaryReader(comp))
                {
                    byte[] header = br.ReadBytes(0xA);
                    byte[] comprimido = br.ReadBytes((int)comp.Length - 0xA);

                    byte[] novoCompHeaderCerto = new byte[0xA + nomeArquivo.Length + 1 + comprimido.Length];


                    novoComp = new MemoryStream(novoCompHeaderCerto);

                    using (BinaryWriter bw = new BinaryWriter(novoComp))
                    {
                        bw.Write(header);
                        bw.BaseStream.Seek(0x3, SeekOrigin.Begin);
                        byte b = 8;
                        bw.Write(b);
                        bw.Write(unixTimestamp);
                        byte[] idGizp = new byte[] { 0x00, 0x0B };
                        bw.Write(idGizp);
                        byte[] asc = Encoding.ASCII.GetBytes(nomeArquivo);
                        bw.Write(asc);
                        b = 0;
                        bw.Write(b);
                        bw.Write(comprimido);
                    }

                    
                }

                diretorio += ".gz";

                File.WriteAllBytes(diretorio, novoComp.ToArray());
            }
        }
    }
}
