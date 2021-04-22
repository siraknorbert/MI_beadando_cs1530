using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MI_egyszemelyes_beadando.Allapotter;
using MI_egyszemelyes_beadando.Keresok;

namespace MI_egyszemelyes_beadando
{
    /// <summary>
    /// Main form, grafikus megjelenítés a játékhoz
    /// </summary>
    
    public partial class Form1 : Form
    {
        // Változók
        List<Kereso> keresok = new List<Kereso>();
        List<Allapot> megoldasok = new List<Allapot>();
        int aktualisAllapotIndex = 0;

        // Init
        public Form1()
        {
            InitializeComponent();
            keresok.Add(new ProbaHibaRandom()); // ez teszt jelleggel került be
            keresok.Add(new Melysegi());
            keresok.Add(new Backtrack());
            keresok.Add(new BestFirst());

            foreach (Kereso kereso in keresok)
            {
                comboBox1.Items.Add(kereso.GetType().Name);
            }
            comboBox1.SelectedIndex = 0;

            // Teszt - útvonalak kiíratása a konzolon
            foreach (Kereso k in keresok)
            {
                Console.WriteLine(" \n----TEST----\n" + k.GetType().Name + "\n------------");
                foreach (Allapot a in k.Utvonal)
                {
                    Console.WriteLine(a.ToString());
                }
            }
        }

        // Prev
        private void button1_Click(object sender, EventArgs e)
        {
            if (aktualisAllapotIndex > 0) aktualisAllapotIndex--;
            Kirajzol();
        }

        // Next
        private void button2_Click(object sender, EventArgs e)
        {
            if (megoldasok.Count - 1 > aktualisAllapotIndex) aktualisAllapotIndex++;
            Kirajzol();
        }

        // Keresők legördülő listáján változik a kiválasztott elem
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            megoldasok = keresok[comboBox1.SelectedIndex].Utvonal;
            aktualisAllapotIndex = 0;
            Kirajzol();
        }

        // Sakktábla init
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Kirajzol();
        }

        // Grafikus megjelenítés
        private void Kirajzol()
        {
            int pB_W = pictureBox1.Width;
            int pB_H = pictureBox1.Height;
            Bitmap image = new Bitmap(pB_W, pB_H);
            pictureBox1.Image = image;
            Graphics g = Graphics.FromImage(image);
            Color color1, color2;
            SolidBrush blackBrush, whiteBrush;

            // Sakktábla kirajzolása
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    color1 = Color.Chocolate;
                    color2 = Color.PeachPuff;
                }
                else
                {
                    color1 = Color.PeachPuff;
                    color2 = Color.Chocolate;
                }

                blackBrush = new SolidBrush(color1);
                whiteBrush = new SolidBrush(color2);

                for (int j = 0; j < 8; j++)
                {
                    if (j % 2 == 0) g.FillRectangle(blackBrush, i * pB_W/8, j * pB_H/8, pB_W/8, pB_H/8);
                    else g.FillRectangle(whiteBrush, i * pB_W/8, j * pB_H/8, pB_W/8, pB_H/8);
                }
            }

            // Elemi lépésekhez
            Babuk[] babuk = megoldasok[aktualisAllapotIndex].Babuk;

            // Király megjelenítése
            Icon king = new Icon("King.ico");
            int kingX = (pB_W / 8) / 8 + (pB_W / 8) * babuk[0].Oszlop;
            int kingY = (pB_H / 8) / 8 + (pB_H / 8) * (7 - babuk[0].Sor);
            Rectangle rectKing = new Rectangle(kingX, kingY, 200, 200);
            g.DrawIconUnstretched(king, rectKing);

            // Huszár megjelenítése
            Icon knight = new Icon("Knight.ico");
            int knightX = (pB_W / 8) / 8 + (pB_W / 8) * babuk[1].Oszlop;
            int knightY = (pB_H / 8) / 8 + (pB_H / 8) * (7 - babuk[1].Sor);
            Rectangle rectKnight = new Rectangle(knightX, knightY, 200, 200);
            g.DrawIconUnstretched(knight, rectKnight);

            label1.Text = "Lépések száma (kezdőállapottal): " + megoldasok.Count;
        }
    }
}
