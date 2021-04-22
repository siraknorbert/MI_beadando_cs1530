using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_egyszemelyes_beadando.Allapotter
{
    /// <summary>
    /// Csomópontok osztálya
    /// </summary>

    class Csomopont
    {
        #region Mezők és propertyk

        // Mezők
        Allapot allapot = new Allapot();
        Csomopont szulo;
        int koltseg;
        int operatorIndex;
        int heurisztika;
        int osszkoltseg;

        // Propertyk
        internal Allapot Allapot { get => allapot; set => allapot = value; }
        internal Csomopont Szulo { get => szulo; set => szulo = value; }
        public int Koltseg { get => koltseg; set => koltseg = value; }
        public int OperatorIndex { get => operatorIndex; set => operatorIndex = value; }
        public int Heurisztika { get => heurisztika; set => heurisztika = value; }
        public int Osszkoltseg { get => osszkoltseg; set => osszkoltseg = value; }

        #endregion

        // Init #1
        public Csomopont(Allapot allapot, int operatorIndex)
        {
            this.allapot = allapot;
            this.operatorIndex = operatorIndex;
        }

        // Init #2
        public Csomopont(Allapot allapot, Csomopont szulo)
        {
            this.allapot = allapot;
            this.szulo = szulo;

            if (szulo == null)
            {
                koltseg = 0;
            }
            else
            {
                koltseg = szulo.koltseg + 1;
            }

            /// <summary>
            /// Heurisztika kiszámolása (a feladatban megadott kezdőállapothoz implementálva)
            /// <summary>
            
            this.heurisztika = 0;

            // Célsor és céloszlop
            int cS = 0;
            int cO = 6;

            // Ha a huszárral a célmezővel szomszédos mezőn állunk
            if (szulo != null && CelMezovelSzomszedosE(szulo.allapot.Babuk[1], cS, cO))
            {
                // Ha a huszár a célmező mellett áll, már csak a királyt mozgatjuk
                MinelKozelebbKerul(allapot.Babuk[0], cS, cO);

                // Ha a huszárral ellépünk a célmezővel szomszédos mezőről
                if (!CelMezovelSzomszedosE(allapot.Babuk[1], cS, cO))
                {
                    this.heurisztika -= 15;
                }
            }
            else
            {
                foreach (Babuk b in allapot.Babuk)
                {
                    MinelKozelebbKerul(b, cS, cO);
                }

                // Ha a király a huszár szomszédos sarokmezőjén áll
                if (szulo != null && allapot.Babuk[0].Sor != allapot.Babuk[1].Sor &&
                    allapot.Babuk[0].Oszlop == szulo.allapot.Babuk[0].Oszlop)
                {
                    this.heurisztika += 7;
                }
            }

            this.osszkoltseg = this.koltseg + (-1) * this.heurisztika;
        }

        // Célmezővel szomszédos-e egy bábu
        bool CelMezovelSzomszedosE(Babuk pBabu, int cS, int cO)
        {
            for (int i = cS - 1; i <= cS + 1; i++)
            {
                for (int j = cO - 1; j <= cO + 1; j++)
                {
                    if (pBabu.Sor == i && pBabu.Oszlop == j)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Minél közelebb kerül a bábu a célmezőhöz, annál nagyobb súlyozás
        void MinelKozelebbKerul(Babuk b, int cS, int cO)
        {
            // Minél közelebb kerül a bábu sora a célmező sorához, annál nagyobb súly
            if (b.Sor > cS)
            {
                this.heurisztika += (7 - (b.Sor - cS));
            }
            else if (b.Sor < cS)
            {
                this.heurisztika += (7 - (cS - b.Sor));
            }
            else this.heurisztika += 7;

            // Minél közelebb kerül a bábu oszlopa a célmező oszlopához, annál nagyobb súly
            if (b.Sor > cO)
            {
                this.heurisztika += (7 - (b.Sor - cO));
            }
            else if (b.Sor < cO)
            {
                this.heurisztika += (7 - (cO - b.Sor));
            }
            else this.heurisztika += 7;
        }

        // Egyenlőségvizsgálat override
        public override bool Equals(object obj)
        {
            Csomopont vizsgalandoCsomopont = (Csomopont)obj;
            return this.allapot.Equals(vizsgalandoCsomopont.Allapot);
        }
    }
}
