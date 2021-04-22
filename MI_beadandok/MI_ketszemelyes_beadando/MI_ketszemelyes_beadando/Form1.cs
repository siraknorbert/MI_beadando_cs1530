using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MI_ketszemelyes_beadando.Allapotter;
using MI_ketszemelyes_beadando.Keresok;
using MI_ketszemelyes_beadando.Ermek;

namespace MI_ketszemelyes_beadando
{
    /// <summary>
    /// Main form logikája
    /// </summary>

    public partial class Form1 : Form
    {
        Allapot allapot;
        Allapot elozoAllapot;
        Erme[] jatekbanLevoErmek = new Erme[Allapot.maxErmeSzam];
        Erme[] osszesErme = new Erme[10];
        int osszesKivalasztottSzam = 0;

        // Init
        public Form1()
        {
            InitializeComponent();
            allapot = new Allapot();
            ErmekInit();
            label_msg2.Text = " ";
            label_result.Text = " ";
        }

        // Játék végén
        public bool vegeVanE()
        {
            switch (allapot.Celfeltetel())
            {
                case 1:
                    label_result.ForeColor = Color.Red;
                    label_result.Text = "VERESÉG!";
                    GombokLetiltasa();
                    return true;
                    break;
                case 2:
                    label_result.ForeColor = Color.Green;
                    label_result.Text = "GYŐZELEM!";
                    GombokLetiltasa();
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }
        public void vegeVanEVoid()
        {
            switch (allapot.Celfeltetel())
            {
                case 1:
                    label_result.ForeColor = Color.Red;
                    label_result.Text = "VERESÉG!";
                    GombokLetiltasa();
                    break;
                case 2:
                    label_result.ForeColor = Color.Green;
                    label_result.Text = "GYŐZELEM!";
                    GombokLetiltasa();
                    break;
                default:
                    break;
            }
        }

        // Gombok disable
        void GombokLetiltasa()
        {
            foreach (Erme erme in jatekbanLevoErmek)
            {
                erme.Gomb.Enabled = false;
            }
            button_atfordit.Enabled = false;
        }

        // Ermek inicializálása:
        void ErmekInit()
        {
            // Összes érme listába gyűjtése
            osszesErme[0] = new Erme(button1, false);
            osszesErme[1] = new Erme(button2, false);
            osszesErme[2] = new Erme(button3, false);
            osszesErme[3] = new Erme(button4, false);
            osszesErme[4] = new Erme(button5, false);
            osszesErme[5] = new Erme(button6, false);
            osszesErme[6] = new Erme(button7, false);
            osszesErme[7] = new Erme(button8, false);
            osszesErme[8] = new Erme(button9, false);
            osszesErme[9] = new Erme(button10, false);

            // Játékban lévő érmék listába gyűjtése, ...
            for (int i = 0; i < Allapot.maxErmeSzam; i++)
            {
                jatekbanLevoErmek[i] = osszesErme[i];
            }

            // ...a többi letiltása
            for (int i = 0; i < (10 - Allapot.maxErmeSzam); i++)
            {
                osszesErme[osszesErme.Length - 1 - i].Gomb.Enabled = false;
            }
        }

        #region Gombokra kattintás

        private void Form1_Load(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            ErmereKattintas(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ErmereKattintas(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ErmereKattintas(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ErmereKattintas(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ErmereKattintas(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ErmereKattintas(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ErmereKattintas(6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ErmereKattintas(7);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ErmereKattintas(8);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ErmereKattintas(9);
        }

        #endregion

        // Érmére kattintás
        void ErmereKattintas(int index)
        {
            label_msg2.Text = " ";

            if (!jatekbanLevoErmek[index].IsChecked)
            {
                jatekbanLevoErmek[index].Gomb.BackColor = Color.Black;
                jatekbanLevoErmek[index].IsChecked = true;
                osszesKivalasztottSzam++;
            }
            else
            {
                if (jatekbanLevoErmek[index].Gomb.Text == "FEJ")
                {
                    jatekbanLevoErmek[index].Gomb.BackColor = Color.Blue;
                }
                else
                {
                    jatekbanLevoErmek[index].Gomb.BackColor = Color.Red;
                }
                jatekbanLevoErmek[index].IsChecked = false;
                osszesKivalasztottSzam--;
            }
        }

        // Érmék átfordítása
        private void button_atfordit_Click(object sender, EventArgs e)
        {
            int[] miket = new int[3];
            int[] mikre = new int[3];
            Random rnd = new Random();
            int randomSzam;
            int n = 0;

            // Miket és Mikre feltöltése
            for (int i = 0; i < jatekbanLevoErmek.Length; i++)
            {
                if (n < 3 && jatekbanLevoErmek[i].IsChecked)
                {
                    miket[n] = i;
                    if (jatekbanLevoErmek[i].Gomb.Text == "FEJ")
                    {
                        mikre[n] = 0; // fejről írásra
                    }
                    else
                    {
                        mikre[n] = 1;
                    }
                    n++;
                }  
            }

            // Miket és Mikre kibővítése -1-esekkel
            if (n < 3)
            {
                for (int i = 0; i < 3-n; i++)
                {
                    do
                    {
                        randomSzam = rnd.Next(0, Allapot.maxErmeSzam);
                    } while(miket.Contains(randomSzam));

                    miket[i + n] = randomSzam;
                    mikre[i + n] = -1;
                }
            }

            // Nem választhatunk 3-nál több érmét
            if (osszesKivalasztottSzam <= 3)
            {
                osszesKivalasztottSzam = 0;
                Operator op = new Operator(miket, mikre);
                if (op.Elofeltetel(allapot))
                {
                    // Játékos köre
                    foreach (Erme erme in jatekbanLevoErmek)
                    {
                        if (erme.IsChecked)
                        {
                            erme.IsChecked = false;
                            if (erme.Gomb.Text == "FEJ")
                            {
                                erme.Gomb.Text = "ÍRÁS";
                                erme.Gomb.BackColor = Color.Red;
                            }
                            else
                            {
                                erme.Gomb.Text = "FEJ";
                                erme.Gomb.BackColor = Color.Blue;
                            }
                        }
                    }

                    allapot = op.Atfordit(allapot);
                    elozoAllapot = allapot; // játékos ezt lépte legutoljára

                    // TESZT
                    Console.WriteLine("JÁTÉKOS LÉPÉSE");
                    foreach (int item in elozoAllapot.Ermek)
                    {
                        Console.Write(item + ", ");
                    }
                    Console.WriteLine(" ");

                    if (!vegeVanE())
                    {
                        //System.Threading.Thread.Sleep(5000);

                        // Gép köre:
                        List<int> gepAtforditasai = new List<int>();
                        Negamax negamax = new Negamax();
                        Operator opGep = negamax.Ajanl(allapot);

                        allapot = opGep.Atfordit(allapot);

                        // TESZT
                        Console.WriteLine("GÉP LÉPÉSE");
                        foreach (int item in allapot.Ermek)
                        {
                            Console.Write(item + ", ");
                        }
                        Console.WriteLine(" ");

                        // Gép átfordításainak megjelenítése:
                        for (int i = 0; i < allapot.Ermek.Length; i++)
                        {
                            // Átfordítási indexek eltárolása
                            if (allapot.Ermek[i] != elozoAllapot.Ermek[i])
                            {
                                gepAtforditasai.Add(i);
                            }

                            // Lépések megjelenítése
                            if (allapot.Ermek[i] == 1)
                            {
                                jatekbanLevoErmek[i].Gomb.Text = "FEJ";
                                jatekbanLevoErmek[i].Gomb.BackColor = Color.Blue;
                            }
                            else
                            {
                                jatekbanLevoErmek[i].Gomb.Text = "ÍRÁS";
                                jatekbanLevoErmek[i].Gomb.BackColor = Color.Red;
                            }
                        }

                        label_msg.Text = "Az ellenfél iménti körben átfordított érméi: ";
                        foreach (int index in gepAtforditasai)
                        {
                            label_msg.Text = label_msg.Text + " | " + (index + 1).ToString();
                        }

                        gepAtforditasai.Clear();
                        vegeVanEVoid();
                    }
                }
                else
                {
                    label_msg2.Text = "Nem teljesült egy előfeltétel!";
                }
            }
            else
            {
                label_msg2.Text = "Maximum 3 érme fordítható át!";
            }
        }
    }
}
