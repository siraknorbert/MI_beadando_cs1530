using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_ketszemelyes_beadando.Allapotter
{
    /// <summary>
    /// Operátor osztály
    /// </summary>

    class Operator
    {
        int[] miket = new int[3];
        int[] mikre = new int[3];
        int suly;

        public int[] Miket { get => miket; set => miket = value; }
        public int[] Mikre { get => mikre; set => mikre = value; }
        public int Suly { get => suly; set => suly = value; }

        // Init: mit és mire
        public Operator(int[] miket, int[] mikre)
        {
            this.Miket = miket;
            this.Mikre = mikre;
        }

        // Előfeltétel
        public bool Elofeltetel(Allapot jelenlegiAllapot)
        {
            bool voltEAtforditas = false;
            for (int i = 0; i < Miket.Length; i++)
            {
                // Ha egy érmét ugyanarra fordítanánk át, amin van
                if (jelenlegiAllapot.Ermek[Miket[i]] == Mikre[i])
                {
                    return false;
                }

                // Ha akár csak egy érmét is átfordíanánk
                if (Mikre[i] > -1)
                {
                    voltEAtforditas = true;
                }
            }

            // Ha egy érmét sem fordítanánk át
            if (!voltEAtforditas)
            {
                return false;
            }

            return true;
        }

        // Állapotváltás
        public Allapot Atfordit(Allapot jelenlegiAllapot)
        {
            Allapot ujAllapot = new Allapot();
            ujAllapot.Ermek = (int[])jelenlegiAllapot.Ermek.Clone();
            ujAllapot.ElozoAllapot = jelenlegiAllapot; // szülő beállítása

            // Kiválasztott érmék átfordítása
            for (int i = 0; i < Miket.Length; i++)
            {
                if (Miket[i] > -1 && Mikre[i] > -1)
                {
                    ujAllapot.Ermek[Miket[i]] = Mikre[i];
                }
            }
            

            // Játékosváltás
            if (jelenlegiAllapot.Jatekos == 1)
            {
                ujAllapot.Jatekos = 4;
            }
            else
            {
                ujAllapot.Jatekos = 1;
            }

            return ujAllapot;
        }
    }
}
