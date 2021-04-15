using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YGTool.Telas
{
    public partial class Editor_de_Ordem_de_Cartas : Form
    {
        public Editor_de_Ordem_de_Cartas()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dirCardIdx = "";
            string dirCardSort = "";
            string dirCardName = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "Arquivo CardIdx (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dirCardIdx = openFileDialog.FileName;

                    using (OpenFileDialog openFileDialog2 = new OpenFileDialog())
                    {

                        openFileDialog2.Filter = "Arquivo CardName (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";

                        if (openFileDialog2.ShowDialog() == DialogResult.OK)
                        {
                            dirCardName = openFileDialog2.FileName;

                            using (OpenFileDialog openFileDialog3 = new OpenFileDialog())
                            {

                                openFileDialog3.Filter = "Arquivo CardSort (*.bin)|*.bin|Todos os Arquivos (*.*)|*.*";

                                if (openFileDialog3.ShowDialog() == DialogResult.OK)
                                {
                                    dirCardSort = openFileDialog3.FileName;

                                    LerNomesExibir(dirCardIdx,dirCardName, dirCardSort);
                                }
                            }
                        }
                    }
                }
            }
        }


        List<CardInfo> cartas = new List<CardInfo>();
        List<CardInfo> cartas2Ingame = new List<CardInfo>();

        private void LerNomesExibir(string dirCardIdx, string dirCardName, string dirCardSort)
        {
            cartas = new List<CardInfo>();
            cartas2Ingame = new List<CardInfo>();
            dataGridView2.Rows.Clear();
            dataGridView1.Rows.Clear();
            byte[] cardIdx = File.ReadAllBytes(dirCardIdx);
            byte[] cardName = File.ReadAllBytes(dirCardName);
            byte[] cardSort = File.ReadAllBytes(dirCardSort);

            int quantidadeDeCartas = cardSort.Length / 2;

            using (BinaryReader brIdx = new BinaryReader(new MemoryStream(cardIdx)))
            {
                using (BinaryReader brName = new BinaryReader(new MemoryStream(cardName)))
                {
                    using (BinaryReader brSort = new BinaryReader(new MemoryStream(cardSort)))
                    {
                        int offsetNoIdx = 0;
                     //   int offsetNoSort = 0;

                        for (int i = 0; i < quantidadeDeCartas; i++)
                        {
                            brIdx.BaseStream.Position = offsetNoIdx;
                            int ponteiro = brIdx.ReadInt32();
                            offsetNoIdx += 8;
                           // brSort.BaseStream.Position = offsetNoSort;
                            ushort idSort = brSort.ReadUInt16();

                            brName.BaseStream.Position = ponteiro;
                            List<short> letras = new List<short>();
                            short valorUnicode = brName.ReadInt16();
                            letras.Add(valorUnicode);

                            while (valorUnicode != 0)
                            {
                                valorUnicode = brName.ReadInt16();
                                letras.Add(valorUnicode);
                            }

                            string nomeCarta = "";


                            foreach (var item in letras)
                            {
                                nomeCarta += Convert.ToChar(item);
                            }

                            cartas.Add(new CardInfo(ponteiro, nomeCarta, idSort));
                            cartas2Ingame.Add(new CardInfo(ponteiro, nomeCarta, idSort));
                        }
                    }
                }
            }

            PovoarLista();
            PovoarIngame();
        }

        private void PovoarLista()
        {
            foreach (var item in cartas)
            {
                dataGridView1.Rows.Add(item.Ponteiro,item.Nome, item.IdCardSort);
            }
        }

        private void PovoarIngame()
        {
            cartas2Ingame.Sort();
            int contador = 0;
            foreach (var item in cartas2Ingame)
            {
                dataGridView2.Rows.Add(contador,item.Nome, item.IdCardSort);
                contador++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GerarCardSort();
            MessageBox.Show("Cartas ordenadas com sucesso!");
        }

        private void GerarCardSort()
        {
            cartas2Ingame = cartas2Ingame.OrderBy(x => x.Nome).ToList();
            List<ushort> idSortNovo = new List<ushort>();
            List<ushort> backupId = new List<ushort>();

            foreach (var item in cartas2Ingame)
            {
                backupId.Add(item.IdCardSort);
            }

            for (int i = 0; i < cartas2Ingame.Count; i++)
            {
                cartas2Ingame[i].IdCardSort = (ushort)i;
            }

            byte[] novoCardSort = new byte[cartas2Ingame.Count * 2];
            MemoryStream ms = new MemoryStream(novoCardSort);
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                

                foreach (var item in cartas)
                {
                    ushort id = (ushort)backupId.FindIndex(x => x == item.IdCardSort);                   
                    bw.Write(cartas2Ingame[id].IdCardSort);

                }

                novoCardSort = ms.ToArray();
            }

            File.WriteAllBytes("CARD_Sort_E_novo.bin", novoCardSort);

            dataGridView2.Rows.Clear();
            PovoarIngame();
            dataGridView1.Rows.Clear();
            PovoarLista();

            
        }
    }

    public class CardInfo : IComparable<CardInfo>
    {
        public int Ponteiro { get; set; }
        public string Nome { get; set; }
        public ushort IdCardSort { get; set; }

        public CardInfo(int ponteiro, string nome, ushort idCardSort)
        {
            Ponteiro = ponteiro;
            Nome = nome;
            IdCardSort = idCardSort;
        }

        public int CompareTo(CardInfo other)
        {
            if (IdCardSort < other.IdCardSort)
            {
                return -1;
            }
            else if (IdCardSort > other.IdCardSort)
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
