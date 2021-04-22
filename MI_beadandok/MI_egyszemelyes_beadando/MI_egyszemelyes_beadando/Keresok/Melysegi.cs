using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MI_egyszemelyes_beadando.Allapotter;

namespace MI_egyszemelyes_beadando.Keresok
{
    /// <summary>
    /// Mélységi keresés
    /// </summary>

    class Melysegi : Kereso
    {
        // Init
        public Melysegi()
        {
            Kereses();
        }

        // Keresés
        public override void Kereses()
        {
            Stack<Csomopont> nyiltCsucsok = new Stack<Csomopont>();
            List<Csomopont> zartCsucsok = new List<Csomopont>();

            nyiltCsucsok.Push(new Csomopont(new Allapot(), null));

            // Meddig
            while (nyiltCsucsok.Count > 0 && nyiltCsucsok.Peek().Allapot.Celfeltetel() == false)
            {
                Csomopont aktualisCsomopont = nyiltCsucsok.Pop();
                foreach (Operator op in Operatorok)
                {
                    if (op.Elofeltetel(aktualisCsomopont.Allapot))
                    {
                        Allapot ujAllpot = op.Lepes(aktualisCsomopont.Allapot);
                        Csomopont ujCsomopont = new Csomopont(ujAllpot, aktualisCsomopont);

                        if (nyiltCsucsok.Contains(ujCsomopont) == false && zartCsucsok.Contains(ujCsomopont) == false)
                        {
                            nyiltCsucsok.Push(ujCsomopont);
                        }
                    }
                }
                zartCsucsok.Add(aktualisCsomopont);
            }

            // Útvonal eltárolása
            if (nyiltCsucsok.Count > 0)
            {
                Csomopont celCsomopont = nyiltCsucsok.Peek();
                while (celCsomopont != null)
                {
                    this.Utvonal.Add(celCsomopont.Allapot);
                    celCsomopont = celCsomopont.Szulo;
                }
                this.Utvonal.Reverse();
            }
        }
    }
}
