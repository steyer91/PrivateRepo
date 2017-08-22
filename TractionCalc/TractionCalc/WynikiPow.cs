using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TractionCalc
{
    class WynikiPow
    {
        public int id { get; set; }
        public int ai { get; set; }
        public int aj { get; set; }
        public double wynik_aprim { get; set; }
        public double wynik_lprim { get; set; }

        public double wynik_lij { get; set; }
        public double alfaprim { get; set; }
    }
    class AlfaList : WynikiPow
    {
        
        public double alfa { get; set; }
    }

    class Wyniki
    {
        public int id { get; set; }
        public int ai { get; set; }
        public int aj { get; set; }
        public double alfaij { get; set; }
        public double alfa1 { get; set; }
        public double alfa2 { get; set; }
        public double _lAij { get; set; }
        public double cosAlfaIJ { get; set; }
    }
    class LTij
    {
        public int id { get; set; }
        public double dltij { get; set; }
        public double wynik_ltij { get; set; }
        public double jtij { get; set; }
        public double F_xij { get; set; }
        public double Rn_xij { get; set; }
        public double F_d { get; set; }
        public double Fz_ij { get; set; }
        public double Fnz_ij { get; set; }
    }
}
