using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_egyszemelyes_beadando.Allapotter
{
    /// <summary>
    /// Állapotok osztálya
    /// </summary>

    class Allapot
    {
        public static int BABUKSZAMA = 2;
        public Babuk[] Babuk = new Babuk[BABUKSZAMA];
        private Babuk celMezo = new Babuk(0, 6); // ide kell eljuttatni egy bábut

        // Init, kezdőállapot
        public Allapot()
        {
            Babuk[0] = new Babuk(2, 1); // király
            Babuk[1] = new Babuk(2, 2); // huszár
        }

        // Célfeltétel eldöntése (van-e bábu a célmezőn (0, 6)?)
        public bool Celfeltetel()
        {
            for (int i = 0; i < BABUKSZAMA; i++)
            {
                if (Babuk[i].Sor == celMezo.Sor && Babuk[i].Oszlop == celMezo.Oszlop)
                {
                    return true;
                }
            }

            return false;
        }

        // Sakkfigurák helyzetének kiíratása
        public override string ToString()
        {
            string babu;
            StringBuilder builder = new StringBuilder();
            builder.Append("(");

            for (int i = 0; i < BABUKSZAMA; i++)
            {
                if (i == 0) babu = "K";
                else babu = "H";

                builder.Append(babu);
                builder.Append("-s");
                builder.Append(Babuk[i].Sor);
                builder.Append("-o");
                builder.Append(Babuk[i].Oszlop);
                builder.Append("; ");
            }

            builder.Append(")");
            return builder.ToString();
        }

        // Egyenlőségvizsgálat override
        public override bool Equals(object obj)
        {
            Allapot vizsgaltAllapot = (Allapot)obj;

            for (int i = 0; i < this.Babuk.Length; i++)
            {
                if (this.Babuk[i] != vizsgaltAllapot.Babuk[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
