using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGTool.Arquivo
{
    public class Ehf
    {
        public void Teste(string fEhf)
        {
            Stream ehf = new MemoryStream(File.ReadAllBytes(fEhf));
            int a0 = 0; //pos leitura
            int v1 = 0;
            int v0 = 0;
            int s1 = 0;
            int s0 = 0;
            int s2 = 0x30;
            int a1 = 0;
            int a2 = 0;
            MemoryStream backup = new MemoryStream();
            ehf.CopyTo(backup);
            byte[] arqManipulado = backup.ToArray();
            while (true)
            {

                v1 = Lw(a0, new MemoryStream(arqManipulado));

                if (v1 != 0x1A464845)
                {
                    break;
                }


                v0 = Lw(a0 + 0x38, new MemoryStream(arqManipulado));

                if (v0 != 0)
                {

                }

                v1 = a0 + 0x10;

                v0 = Lw(v1 + 0x08, new MemoryStream(arqManipulado));

                v0 += a0;



                arqManipulado = Sh(v0 + 0x020, s1, new MemoryStream(arqManipulado)).ToArray();
                s0 = v0;

                arqManipulado = Sh(v0 + 0x022, s2, new MemoryStream(arqManipulado)).ToArray();

                if (s1 != 0)
                {

                }

       
                s2 = Lh(s0 + 0x1A, new MemoryStream(arqManipulado));              


                a1 = Lw(s0, new MemoryStream(arqManipulado));

                a0 = s0;

                arqManipulado = Sw(a0, a1, new MemoryStream(arqManipulado)).ToArray();

                v0 = a1 + 0x10;
                arqManipulado = Sw(a0 + 4, v0, new MemoryStream(arqManipulado)).ToArray();
                v0 = Lw(a1 + 0x10, new MemoryStream(arqManipulado));


                s2 = a0;
                s1 = a2;
                v0 += a1;
                arqManipulado = Sw(a0 + 8, v0, new MemoryStream(arqManipulado)).ToArray();
                arqManipulado = Sw(a0 + 0xC, 0, new MemoryStream(arqManipulado)).ToArray();
                arqManipulado = Sw(a0 + 0x4A, 0, new MemoryStream(arqManipulado)).ToArray();
                v0 = Lh(a0 + 4 , new MemoryStream(arqManipulado));
                s0 = s2 + 0x28;

                v0 = Lw(v0 + 0x10, new MemoryStream(arqManipulado));

            }
        }

        public MemoryStream Sh(int offset, int valor, MemoryStream a)
        {
            using (BinaryWriter bw = new BinaryWriter(a))
            {
                bw.BaseStream.Position = offset;
                bw.Write((short)valor);
            }

            File.WriteAllBytes("ehf.bin", a.ToArray());
            return a;
        }

        public MemoryStream Sw(int offset, int valor, MemoryStream a)
        {
            using (BinaryWriter bw = new BinaryWriter(a))
            {
                bw.BaseStream.Position = offset;
                bw.Write((short)valor);
            }

            File.WriteAllBytes("ehf.bin", a.ToArray());
            return a;
        }

        public int Lw(int offset, MemoryStream a)
        {
            using (BinaryReader br = new BinaryReader(a))
            {
                br.BaseStream.Position = offset;
                return br.ReadInt32();
            }
        }

        public int Lh(int offset, MemoryStream a)
        {
            using (BinaryReader br = new BinaryReader(a))
            {
                br.BaseStream.Position = offset;
                return br.ReadInt16();
            }
        }
    }
}
