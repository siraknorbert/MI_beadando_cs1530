using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_ketszemelyes_beadando.Allapotter
{
    /// <summary>
    /// Állapot osztály
    /// </summary>

    class Allapot
    {
        public static int maxErmeSzam = 5; // futásidő csökkentése miatt nem 10
        int[] ermek; // 1: fej | 0: írás
        Allapot elozoAllapot = null;
        int jatekos;

        public int[] Ermek { get => ermek; set => ermek = value; }
        public Allapot ElozoAllapot { get => elozoAllapot; set => elozoAllapot = value; }
        public int Jatekos { get => jatekos; set => jatekos = value; }

        // Init: érmék és játékos(ok)
        public Allapot()
        {
            this.Ermek = new int[maxErmeSzam];
            for (int i = 0; i < Ermek.Length; i++)
            {
                Ermek[i] = 1; // minden érme fej az elején
            }

            this.ElozoAllapot = null;
            this.Jatekos = 1; //gép: 4
        }

        // Célfeltétel: Minden írás-e?
        public int Celfeltetel()
        {
            // 1: játékos nyert
            // 2: gép nyert
            // 0: mehet tovább
            for (int i = 0; i < ermek.Length; i++)
            {
                if (ermek[i] == 1)
                {
                    return 0;
                }
            }

            if (Jatekos == 1)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        // Heurisztika
        public int Heurisztika()
        {
            int suly = 0;
            int fejCounter = 0;

            // Fej érmék összeszámolása
            for (int i = 0; i < ermek.Length; i++)
            {
                if (ermek[i] == 1)
                {
                    fejCounter++;
                }
            }

            // Súly meghatározása: Hány érmét fordítson át és mire?
            // 3+ fej - a kör után ne legyen a fejek száma 4 alatt
            // 3 vagy kevesebb fej - átfordítani mindet írásra
            if (Celfeltetel() == 0)
            {
                if (fejCounter > 3)
                {
                    suly += 5;
                }
                if (fejCounter == 0)
                {
                    suly += 10;
                }
            }
            else if (Celfeltetel() == 1)
            {
                return 100;
            }
            else
            {
                return 80;
            }

            return suly;
        }
    }
}
