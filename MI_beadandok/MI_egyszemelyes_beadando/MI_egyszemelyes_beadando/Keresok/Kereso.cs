﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MI_egyszemelyes_beadando.Allapotter;

namespace MI_egyszemelyes_beadando.Keresok
{
    /// <summary>
    /// Kereső base class, ebből kerülnek származtatásra a keresők
    /// </summary>

    abstract class Kereso
    {
        private List<Allapot> utvonal = new List<Allapot>();
        internal List<Allapot> Utvonal { get => utvonal; set => utvonal = value; }

        private List<Operator> operatorok = new List<Operator>();
        internal List<Operator> Operatorok { get => operatorok; set => operatorok = value; }

        // Operátorok: bábuk minden lehetséges elhelyezése a játéktáblán
        private void operatorokGeneralasa()
        {
            for (int b = 0; b < Allapot.BABUKSZAMA; b++) //bábuk száma
            {
                for (int s = 0; s < 8; s++) //játéktábla sorai
                {
                    for (int o = 0; o < 8; o++) //játéktábla oszlopai
                    {
                        Babuk babuPozicio = new Babuk(s, o);
                        Operator ujOperator = new Operator(b, babuPozicio);
                        operatorok.Add(ujOperator);
                    }
                }
            }
        }

        // Init
        public Kereso()
        {
            operatorokGeneralasa();
            Operatorok.Reverse();
        }

        // Keresés absztrakt metódusa
        abstract public void Kereses();

        // Útvonal kiíratása
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Allapot allapot in utvonal)
            {
                builder.Append(allapot).Append("\n");
            }
            builder.Append(utvonal.Count);
            return builder.ToString();
        }
    }
}
