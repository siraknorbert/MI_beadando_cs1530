using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MI_egyszemelyes_beadando.Allapotter;

namespace MI_egyszemelyes_beadando.Keresok
{
    /// <summary>
    /// Próba hiba random kereső (teszt jelleggel került be)
    /// </summary>

    class ProbaHibaRandom : Kereso
    {
        // Init
        public ProbaHibaRandom()
        {
            Kereses();
        }

        // Keresés
        public override void Kereses()
        {
            Allapot kezdoAllapot = new Allapot();
            List<Allapot> ut = new List<Allapot>();
            ut.Add(kezdoAllapot);
            Random random = new Random();

            // Meddig
            while (!ut.Last().Celfeltetel())
            {
                int randomIndex = random.Next(0, Operatorok.Count);
                Operator valasztottOperator = Operatorok[randomIndex];

                if (valasztottOperator.Elofeltetel(ut.Last()))
                {
                    Allapot ujAllapot = valasztottOperator.Lepes(ut.Last());
                    ut.Add(ujAllapot);
                }
            }

            // Állapotok hozzáadása az útvonalhoz
            foreach (Allapot allapot in ut)
            {
                Utvonal.Add(allapot);
            }

        }
    }
}
