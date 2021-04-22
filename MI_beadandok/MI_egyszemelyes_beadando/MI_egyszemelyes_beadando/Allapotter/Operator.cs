using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MI_egyszemelyes_beadando.Allapotter;

namespace MI_egyszemelyes_beadando.Allapotter
{
    /// <summary>
    /// Állapotváltozásokat leíró függvények osztálya
    /// </summary>

    class Operator
    {
        // Babuk tömbben az egyik bábu indexe, a 0. a király, az 1. a huszár
        private int mit;
        public int Mit { get => mit; set => mit = value; }

        // Ez itt a sakktábla-mezőt reprezentálja, ahova épp lépünk
        private Babuk hova;
        public Babuk Hova { get => hova; set => hova = value; }

        // Init - mit és hova
        public Operator(int mit, Babuk hova)
        {
            this.Mit = mit;
            this.Hova = hova;
        }

        // Állapotváltás (lépünk egy bábuval)
        public Allapot Lepes(Allapot allapot)
        {
            // Lokális változó, melybe eltároljuk a paraméter értékeit
            Allapot ujAllapot = new Allapot();
            for (int i = 0; i < Allapot.BABUKSZAMA; i++)
            {
                ujAllapot.Babuk[i] = allapot.Babuk[i];
            }
            ujAllapot.Babuk[mit] = hova; // állapotváltás

            return ujAllapot;
        }

        // Előfeltétel (ha nem teljesül mind, nem léphetünk)
        public bool Elofeltetel(Allapot allapot)
        {
            // Ha a királlyal lépünk
            if (mit == 0)
            {
                // Ha 1 mezőnél többet lépünk (király csak 1-et léphet bármely irányba)
                if (allapot.Babuk[mit].Sor + 1 < hova.Sor || allapot.Babuk[mit].Sor - 1 > hova.Sor ||
                    allapot.Babuk[mit].Oszlop + 1 < hova.Oszlop || allapot.Babuk[mit].Oszlop - 1 > hova.Oszlop)
                {
                    return false;
                }
            }

            // Segédváltozók
            int segedSor = -2;
            int segedOszlop = -2;

            // Ha a huszárral lépünk
            if (mit == 1)
            {
                // Ha van L-alakban lépés azt eltároljuk
                bool isThereValidMove = false;

                for (int i = allapot.Babuk[mit].Sor - 2; i <= allapot.Babuk[mit].Sor + 2; i++)
                {
                    for (int j = allapot.Babuk[mit].Oszlop - 2; j <= allapot.Babuk[mit].Oszlop + 2; j++)
                    {
                        // Ha a vizsgált állapot nem esik a játéktáblán kívülre
                        if (i >= 0 && i <= 7 && j >= 0 && j <= 7)
                        {
                            if (i != allapot.Babuk[mit].Sor && j != allapot.Babuk[mit].Oszlop &&
                            Math.Abs(segedSor) != Math.Abs(segedOszlop))
                            {
                                if (hova.Sor == i && hova.Oszlop == j)
                                {
                                    isThereValidMove = true;
                                    break;
                                }
                            }
                        }

                        segedOszlop++;
                    }

                    segedSor++;
                    segedOszlop = -2;
                }
                
                // Ha nem volt L-alakban lépés
                if (isThereValidMove == false)
                {
                    return false;
                }
            }

            // Segédváltozó a másik (mit-ben nem eltárolt) bábu indexének
            int masikBabu = 1 - mit;
            
            segedSor = -2;
            segedOszlop = -2;

            // Ha egyik bábu üti a másikat, azt eltároljuk
            bool utiE = false;
            for (int i = allapot.Babuk[masikBabu].Sor - 2; i <= allapot.Babuk[masikBabu].Sor + 2; i++)
            {
                for (int j = allapot.Babuk[masikBabu].Oszlop - 2; j <= allapot.Babuk[masikBabu].Oszlop + 2; j++)
                {
                    // Ha a király üti a huszárt
                    if (Math.Abs(segedSor) == 1 && Math.Abs(segedOszlop) == 1 && hova.Sor == i && hova.Oszlop == j)
                    {
                        utiE = true;
                        break;
                    }

                    // Ha a huszár üti a királyt
                    if ((Math.Abs(segedSor) == 1 && Math.Abs(segedOszlop) == 2 || 
                        Math.Abs(segedSor) == 2 && Math.Abs(segedOszlop) == 1) && 
                        hova.Sor == i && hova.Oszlop == j)
                    {
                        utiE = true;
                        break;
                    }

                    segedOszlop++;
                }

                segedSor++;
                segedOszlop = -2;
            }

            // Ha nem ütötte egyik bábu sem a másikat
            if (utiE == false)
            {
                return false;
            }

            // Ha ugyanoda lépünk, ahol volt a bábu
            if (allapot.Babuk[mit].Sor == hova.Sor && allapot.Babuk[mit].Oszlop == hova.Oszlop)
            {
                return false;
            }

            // Ha ugyanoda lépünk, ahol a másik bábu áll
            if (hova.Sor == allapot.Babuk[masikBabu].Sor && hova.Oszlop == allapot.Babuk[masikBabu].Oszlop)
            {
                return false;
            }

            return true;
        }
    }
}
