using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MI_ketszemelyes_beadando.Allapotter;

namespace MI_ketszemelyes_beadando.Keresok
{
    /// <summary>
    /// Negamax algoritmus
    /// </summary>

    class Negamax
    {
        int maxMelyseg = 3;

        // Hogy ne fusson túl sokáig
        int ajanlKorlat = 0;
        int bejarKorlat = 0;

        // Ajánl függvény
        public Operator Ajanl(Allapot allapot)
        {
            int atfordithatoErmekSzama = 3;
            int seged = 2;
            int[] ermekErtekei = new int[2];
            int[] miket = new int[3];
            int[] mikre = new int[3];
            List<Operator> ajanlottOperatorok = new List<Operator>();

            /* Magyarázat:
             * A "miket" tömbben meghatározunk minden lehetséges
             * indexkombinációt 3 index esetén.
             * Mindig vesszük az aktuális indexkombinációt, és
             * végigpróbáljuk rá az összes lehetséges érme-
             * átfordítási kombinációt, melyeket a "mikre"
             * tömbben tárolunk el.
             * Azok az esetek, amikor az átfordítások a következőképp történnének:
                        // egy érme fejről írásra
                        // egy érme írásról fejre
                        // egy érme semmi
                        // ...nincsenek implementálva, mert gyakorlatilag ilyenkor nem történne semmi
             */

            for (int i = 0; i < allapot.Ermek.Length; i++)
            {
                for (int j = 0; j < allapot.Ermek.Length; j++)
                {
                    for (int k = 0; k < allapot.Ermek.Length; k++)
                    {
                        // Miket tömb összes lehetséges indexkombinációi közül
                        // az éppen meghatározott, aktuális kombináció:
                        if ((i + j + k + 2) < allapot.Ermek.Length)
                        {
                            miket[0] = i;
                            miket[1] = i + j + 1;
                            miket[2] = i + j + k + 2;
                        }

                        // Mikre tömb lehetséges érmeátfordítási kombinációi:
                        // (a -1 azt jelenti, nem fordítottunk át érmét)
                        for (int elejerol = 0; elejerol < atfordithatoErmekSzama; elejerol++)
                        {
                            for (int vegerol = atfordithatoErmekSzama - 1; vegerol >= elejerol; vegerol--)
                            {
                                for (int kombinacio = 0; kombinacio < 2; kombinacio++)
                                {
                                    ermekErtekei = ErmeErtekekMeghatarozasa(kombinacio, elejerol, vegerol);

                                    for (int n = 0; n < 4; n++)
                                    {
                                        mikre = MikreMeghatarozas(n, ermekErtekei);

                                        ajanlKorlat++;

                                        // Aktuális operátor és bejárás
                                        Operator aktualisOperator = new Operator(miket, mikre);
                                        if (aktualisOperator.Elofeltetel(allapot))
                                        {
                                            Allapot ujallapot = aktualisOperator.Atfordit(allapot);
                                            Bejaras(ujallapot, aktualisOperator, 0, 1);
                                            
                                            // Futásidő korlát
                                            if (ajanlKorlat > 10000)
                                            {
                                                ajanlottOperatorok = ajanlottOperatorok.OrderByDescending(o => o.Suly).ToList();
                                                return ajanlottOperatorok[0];
                                            }

                                            ajanlottOperatorok.Add(aktualisOperator); // eltárolás
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Legnagyobb súllyal bíró operátor meghatározása és visszaadása:
            ajanlottOperatorok = ajanlottOperatorok.OrderByDescending(o => o.Suly).ToList();
            return ajanlottOperatorok[0];
        }

        // Bejárás metódus
        private void Bejaras(Allapot allapot, Operator eredetiOperator, int melyseg, int elojel)
        {
            if (allapot.Celfeltetel() == 1 || allapot.Celfeltetel() == 2)
            {
                eredetiOperator.Suly = elojel * allapot.Heurisztika();
            }
            else
            {
                if (melyseg < maxMelyseg)
                {
                    int max = Int32.MinValue;
                    int atfordithatoErmekSzama = 3;
                    int[] ermekErtekei = new int[2];
                    int[] miket = new int[3];
                    int[] mikre = new int[3];

                    for (int i = 0; i < allapot.Ermek.Length; i++)
                    {
                        for (int j = 0; j < allapot.Ermek.Length; j++)
                        {
                            for (int k = 0; k < allapot.Ermek.Length; k++)
                            {
                                if ((i + j + k + 2) < allapot.Ermek.Length)
                                {
                                    miket[0] = i;
                                    miket[1] = i + j + 1;
                                    miket[2] = i + j + k + 2;
                                }

                                for (int elejerol = 0; elejerol < atfordithatoErmekSzama; elejerol++)
                                {
                                    for (int vegerol = atfordithatoErmekSzama; vegerol >= elejerol; vegerol--)
                                    {
                                        for (int kombinacio = 0; kombinacio < 2; kombinacio++)
                                        {
                                            ermekErtekei = ErmeErtekekMeghatarozasa(kombinacio, elejerol, vegerol);

                                            for (int n = 0; n < 4; n++)
                                            {
                                                mikre = MikreMeghatarozas(n, ermekErtekei);

                                                bejarKorlat++;

                                                // Aktuális operátor és bejárás
                                                Operator aktualisOperator = new Operator(miket, mikre);
                                                if (aktualisOperator.Elofeltetel(allapot))
                                                {
                                                    Allapot ujallapot = aktualisOperator.Atfordit(allapot);
                                                    int aktSuly = elojel * ujallapot.Heurisztika();
                                                    if (aktSuly > max)
                                                    {
                                                        max = aktSuly;
                                                    }

                                                    // Futásidő korlátok
                                                    if (max >= 10 || max <= -10)
                                                    {
                                                        eredetiOperator.Suly += max;
                                                        return;
                                                    }
                                                    if (bejarKorlat > 10000)
                                                    {
                                                        return;
                                                    }

                                                    Bejaras(ujallapot, eredetiOperator, melyseg + 1, -elojel);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    eredetiOperator.Suly += max;
                }
            }
        }

        // Érmék értékeinek meghatározása
        int[] ErmeErtekekMeghatarozasa(int kombinacio, int elejerol, int vegerol)
        {
            int[] ermekErtekei = new int[2];

            if (kombinacio == 0)
            {
                ermekErtekei[0] = elejerol - 1;
                ermekErtekei[1] = vegerol - 2;
            }
            else
            {
                ermekErtekei[0] = vegerol - 2;
                ermekErtekei[1] = elejerol - 1;
            }

            return ermekErtekei;
        }

        // Mikre meghatározás
        int[] MikreMeghatarozas(int n, int[] ermekErtekei)
        {
            int[] mikre = new int[3];

            if (n == 0)
            {
                mikre[0] = ermekErtekei[0];
                mikre[1] = ermekErtekei[0];
                mikre[2] = ermekErtekei[0];
            }
            else if (n == 1)
            {
                mikre[0] = ermekErtekei[1];
                mikre[1] = ermekErtekei[0];
                mikre[2] = ermekErtekei[0];
            }
            else if (n == 2)
            {
                mikre[0] = ermekErtekei[0];
                mikre[1] = ermekErtekei[1];
                mikre[2] = ermekErtekei[0];
            }
            else
            {
                mikre[0] = ermekErtekei[0];
                mikre[1] = ermekErtekei[0];
                mikre[2] = ermekErtekei[1];
            }

            return mikre;
        }
    }
}
