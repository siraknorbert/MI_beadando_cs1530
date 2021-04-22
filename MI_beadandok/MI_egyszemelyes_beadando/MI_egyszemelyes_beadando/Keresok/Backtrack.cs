using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MI_egyszemelyes_beadando.Allapotter;

namespace MI_egyszemelyes_beadando.Keresok
{
    /// <summary>
    /// Backtrack keresés
    /// </summary>

    class Backtrack : Kereso
    {
        // Init
        public Backtrack()
        {
            Kereses();
        }

        // Keresés
        public override void Kereses()
        {
            Stack<Csomopont> ut = new Stack<Csomopont>();
            ut.Push(new Csomopont(new Allapot(), 0));

            int szamlalo = 0;

            // Meddig
            while (ut.Count > 0 && ut.Peek().Allapot.Celfeltetel() == false)
            {
                Csomopont aktualisCsomopont = ut.Peek();

                if (aktualisCsomopont.OperatorIndex < Operatorok.Count)
                {
                    Operator aktualisOperator = Operatorok[aktualisCsomopont.OperatorIndex];
                    if (aktualisOperator.Elofeltetel(aktualisCsomopont.Allapot))
                    {
                        Allapot ujAllapot = aktualisOperator.Lepes(aktualisCsomopont.Allapot);
                        Csomopont ujCsomopont = new Csomopont(ujAllapot, 0);
                        if (ut.Contains(ujCsomopont) == false)
                        {
                            ut.Push(ujCsomopont);
                        }
                    }
                    aktualisCsomopont.OperatorIndex++;
                }
                else
                {
                    // visszalépés
                    szamlalo++;
                    ut.Pop();
                }
            }

            // Útvonal eltárolása
            if (ut.Count > 0)
            {
                foreach (Csomopont csomopont in ut)
                {
                    Utvonal.Add(csomopont.Allapot);
                }
                Utvonal.Reverse();
            }
        }
    }
}
