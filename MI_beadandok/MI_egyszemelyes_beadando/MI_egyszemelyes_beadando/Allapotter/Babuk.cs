using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_egyszemelyes_beadando.Allapotter
{
    /// <summary>
    /// Osztály a sakkfigurák pozíciójának és a mezők meghatározásához
    /// </summary>

    class Babuk
    {
        private int sor;
        public int Sor { get => sor; set => sor = value; }

        private int oszlop;
        public int Oszlop { get => oszlop; set => oszlop = value; }

        // Init
        public Babuk(int sor, int oszlop)
        {
            Sor = sor;
            Oszlop = oszlop;
        }
    }
}
