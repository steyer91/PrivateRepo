using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Schema;
using TractionCalc.Models;

namespace TractionCalc
{
    public partial class Calc : Form
    {

        public DaneRepository DaneRepository;
        public int index;
        public double ro;
        public double bo;
        public double wH;
        public double wh;
        public double rd;
        public double bd;
        public double dh;
        public double fi;
        public double spc;
        public double wspk;
        public double wykn;
        public double K;
        public double bp;
        public double wd;
        public double yb;
        public double s;
        public double jmax;
        public TextBox pro;
        public TextBox sbo;
        public TextBox w_h;
        public TextBox wysh;
        public TextBox r_d;
        public TextBox b_d;
        public TextBox d_h;
        public TextBox KK;
        public TextBox cc;
        public TextBox ff;
        public TextBox nn;
        public TextBox w_d;
        public TextBox k_tr;
        public TextBox b_p;
        public TextBox y_b;
        public TextBox j_max;//ZAMIAST a
        public ListView lvi;
        private List<Output> OutputList;
        private List<LTij> l_ltij;
        public Calc(int index, TextBox p_ro, TextBox s_bo, TextBox w_H,
            TextBox wys_h, TextBox rd, TextBox bd, TextBox dh, TextBox K,
            TextBox c, TextBox fi, TextBox n, TextBox Wd, TextBox ktr, TextBox bp, TextBox yb, ListView lv, TextBox j_max)
        {
            InitializeComponent();
            this.index = index;
            pro = p_ro;
            sbo = s_bo;
            w_h = w_H;
            wysh = wys_h;
            r_d = rd;
            b_d = bd;
            d_h = dh;
            KK = K;
            cc = c;
            ff = fi;
            nn = n;
            w_d = Wd;
            k_tr = ktr;
            b_p = bp;
            lvi = lv;
            this.j_max = j_max;//ZAMIAST a
        }

        private void Calc_Load(object sender, EventArgs e)
        {

            DaneRepository = new DaneRepository(NHManager.OpenSession());
            var dane = DaneRepository.GetDaneRecord(index);
            
            //try
            //{
            if (DaneRepository.DaneList().Count > 0)
            {
                listView1.Items.Clear();


                var daneList = DaneRepository.DaneList();

                string fii = string.Empty;

                ro = Convert.ToDouble(dane.promien_ro.Replace('.', ','));
                bo = Convert.ToDouble(dane.szerokosc_bo.Replace('.', ','));
                wH = Convert.ToDouble(dane.wysH.Replace('.', ','));
                wh = Convert.ToDouble(dane.wys_h.Replace('.', ','));
                rd = Convert.ToDouble(dane.rd.Replace('.', ','));
                
                bd = Convert.ToDouble(dane.bd.Replace('.', ','));
                dh = Convert.ToDouble(dane.delta_h.Replace('.', ','));
                fii = dane.fi.Replace('.', ',');

                if (!string.IsNullOrWhiteSpace(fii))
                {
                    fi = Convert.ToDouble(dane.fi);
                }
                else
                {
                    fi = 0;
                }
                spc = Convert.ToDouble(dane.sp_c.Replace('.', ','));
                wspk = Convert.ToDouble(dane.wsp_K.Replace('.', ','));
                wykn = Convert.ToDouble(dane.wyk_n.Replace('.', ','));
                K = Convert.ToDouble(dane.wsp_odkrzt_k.Replace('.', ','));
                bp = Convert.ToDouble(dane.bp.Replace('.', ','));
                wd = Convert.ToDouble(dane.Wd.Replace('.', ','));
                yb = Convert.ToDouble(dane.yb.Replace('.', ','));
                jmax = Convert.ToDouble(j_max.Text);
            }
            else
            {
                ro = Convert.ToDouble(pro.Text);
                bo = Convert.ToDouble(sbo.Text);
                wH = Convert.ToDouble(w_h.Text);
                wh = Convert.ToDouble(wysh.Text);
                rd = Convert.ToDouble(r_d.Text);
                bd = Convert.ToDouble(b_d.Text);
                dh = Convert.ToDouble(d_h.Text);
                fi = Convert.ToDouble(KK.Text);
                spc = Convert.ToDouble(cc.Text);
                wspk = Convert.ToDouble(ff.Text);
                wykn = Convert.ToDouble(nn.Text);
                K = Convert.ToDouble(w_d.Text);
                bp = Convert.ToDouble(k_tr.Text);
                wd = Convert.ToDouble(b_p.Text);
                yb = Convert.ToDouble(y_b.Text);
                jmax = Convert.ToDouble(j_max.Text);

            }
            Obliczenia();
            
        }

        private void ObliczeniaPowierzchnie()
        {
            try
            {
                WynikiPow wynPow;
                AlfaList alfa;
                Wyniki wyn;
                LTij ltij;

                int imax = 3;
                int licznik = 0;
                var Wyniki_pow = new List<WynikiPow>();
                var alfaid = new List<AlfaList>();
                var Aprimij = new List<string>();
                var wyn_aij = new List<Wyniki>();
                l_ltij = new List<LTij>();
                var ybx = y_b != null ? Convert.ToDouble(y_b.Text) : yb;
                double Aij;
                double cos_alfa_ij;
                double alfaij;
                double dltij;

                for (int aj = 0; aj <= jmax; aj++)
                {
                    var sumAij = 0;
                    double wynik_ltij = 0;

                    jmax = (int)Math.Floor(OutputList.FirstOrDefault(x => x._j == aj)._jmax);

                    //if (aj + 1> jmax)
                    //    break;

                    for (int ai = 0; ai < imax; ai++)
                    {
                        imax = (int)Math.Floor(OutputList.FirstOrDefault(x => x._i == ai && x._j == aj)._imax);

                        //if (ai > imax)
                        //    break;

                        var xij = OutputList.SingleOrDefault(x => x._j == aj && x._i == ai)._xij;
                        var xij1 = OutputList.SingleOrDefault(x => x._j == aj + 1 && x._i == ai)._xij;

                        var d = OutputList.SingleOrDefault(x => x._j == aj && x._i == ai + 1)._xij;
                        var d1 = OutputList.SingleOrDefault(x => x._j == aj + 1 && x._i == ai + 1)._xij;

                        var _hpj = OutputList.SingleOrDefault(x => x._j == aj && x._i == ai + 1)._hpj;

                        var wynik_Ap_ij = (((xij - d) + (xij1 - d1)) / 2 * ybx); // A'ij - to co liczyliśmy wczoraj (13.06)
                        var wynik_lpAij = ((xij - d) + (xij1 - d1)) / 2; // l'_Aij - długość powierzchni 
                        var wynik_lij = ((xij - d) + (xij1 - d1)) / 4; // lij - odległość środka powierzchni cząstkowej _Aij od pionowej poprzecznej płaszczyzny

                        double alfaprim = Math.Atan(dh / Convert.ToDouble(wynik_lpAij)) * (180 / Math.PI); // Kąt a'ij w stopniach!!

                        Aprimij.Add($"X '{aj},{ai}, A'ij {wynik_Ap_ij}, l'ij{wynik_lpAij}, alfa'ij{alfaprim}");

                        Wyniki_pow.Add(new WynikiPow
                        {
                            id = licznik,
                            ai = ai,
                            aj = aj,
                            wynik_aprim = wynik_Ap_ij,
                            wynik_lprim = wynik_lpAij,
                            wynik_lij = wynik_lij,
                            alfaprim = alfaprim
                        });

                        var _alfa = Wyniki_pow.SingleOrDefault(x => x.id == licznik).alfaprim;

                        alfaid.Add(new AlfaList
                        {
                            id = licznik,
                            alfa = _alfa
                        });
                        if (licznik == 0)
                        {
                            double alfa1 = alfaid.SingleOrDefault(x => x.id == licznik).alfa;
                            alfaij = alfa1 / 2;//alfa ij
                        }
                        else
                        {
                            double alfa1 = alfaid.SingleOrDefault(x => x.id == licznik - 1).alfa;
                            double alfa2 = alfaid.SingleOrDefault(x => x.id == licznik).alfa;
                            alfaij = (alfa1 + alfa2) / 2;//alfa ij
                        }

                        cos_alfa_ij = Math.Cos(alfaij) * (180 / Math.PI);
                        Aij = wynik_Ap_ij / cos_alfa_ij; //  Aij
                        wyn_aij.Add(new Wyniki
                        {
                            id = licznik,
                            ai = ai,
                            aj = aj,
                            alfaij = Convert.ToDouble(alfaij),
                            cosAlfaIJ = cos_alfa_ij,
                            _lAij = Convert.ToDouble(Aij)
                        });

                        double lA_ij = wyn_aij.SingleOrDefault(x => x.id == licznik)._lAij;//konkrenty Aij

                        if (licznik == 0)
                        {
                            double wlij1 = Wyniki_pow.SingleOrDefault(x => x.id == licznik).wynik_lij;//poprzedni
                            dltij = Math.Sqrt(Math.Pow(wlij1, 2) + Math.Pow(dh, 2));// delta l tau ij
                        }
                        else
                        {
                            double wlij1 = Wyniki_pow.SingleOrDefault(x => x.id == licznik - 1).wynik_lij;//poprzedni
                            double wlij2 = Wyniki_pow.SingleOrDefault(x => x.id == licznik).wynik_lij;//kolejny
                            dltij = Math.Sqrt(Math.Pow(wlij1 - wlij2, 2) + Math.Pow(dh, 2));// delta l tau ij
                        }
                        double j_tij = 0.1 * (dltij + (0.5 * lA_ij));// l tau ij
                        double qb = wspk * Math.Pow((_hpj / bp), wykn);

                        double Fxij = (qb * (Math.Tan(fi) * (180 / Math.PI)) + spc) * (1 - Math.Pow(Math.E, -(j_tij / K))) * Aij * cos_alfa_ij;
                        double Rnxij = (qb * wynik_Ap_ij * (Math.Tan(alfaij)) * (180 / Math.PI)); 
                        double Fn_zij = qb * wynik_Ap_ij;
                        double Fzij = (qb * (Math.Tan(fi) * (180 / Math.PI)) + spc) * (1 - Math.Pow(Math.E, -(0.1 * wynik_lij) / K)) * Aij * (Math.Sin(alfaij) * (180 / Math.PI));

                        double Fd = Fxij - Rnxij;
                        wynik_ltij = wynik_ltij + (dltij + (0.5 * lA_ij));

                        l_ltij.Add(new LTij
                        {
                            id = licznik,
                            dltij = dltij,
                            wynik_ltij = wynik_ltij,
                            jtij = j_tij,
                            F_xij = Fxij,
                            Rn_xij = Rnxij,
                            F_d = Fd,
                            Fz_ij = Fzij,
                            Fnz_ij = Fn_zij
                        });

                        licznik++;
                        listView2.Items.Clear();
                        for (int o = 0; o < Wyniki_pow.Count; o++)
                        {
                            wyn = wyn_aij[o];
                            wynPow = Wyniki_pow[o];
                            ltij = l_ltij[o];
                            listView2.Items.Add(ltij.id.ToString());
                            listView2.Items[o].SubItems.Add(wynPow.wynik_aprim.ToString("0.00"));
                            listView2.Items[o].SubItems.Add(wynPow.wynik_lprim.ToString("0.00"));
                            listView2.Items[o].SubItems.Add(wynPow.wynik_lij.ToString("0.00"));
                            listView2.Items[o].SubItems.Add(wynPow.alfaprim.ToString("0.00"));
                            listView2.Items[o].SubItems.Add(wynPow.ai.ToString());
                            listView2.Items[o].SubItems.Add(wynPow.aj.ToString());
                            listView2.Items[o].SubItems.Add(wyn.alfaij.ToString("0.00"));
                            listView2.Items[o].SubItems.Add(ltij.jtij.ToString("0.00"));
                            listView2.Items[o].SubItems.Add(ltij.wynik_ltij.ToString("0.00"));
                            listView2.Items[o].SubItems.Add(ltij.F_xij.ToString("0.00"));

                        }
                    }

                    WynikiKoncowe(l_ltij);
                }
            }
            catch(Exception)
            {

            }
        }

        private void WynikiKoncowe(List<LTij> ltij)
        {
            //LTij ltij;
            double Fx = 0;
            double R = 0;
            double Fd = 0;
            double Fnz = 0;
            double Fz = 0;
            double Wd = 0;

            Fx = ltij.Sum(x => x.F_xij);
            R = ltij.Sum(x => x.Rn_xij);
            Fd = ltij.Sum(x => x.F_d);
            Fnz = ltij.Sum(x => x.Fnz_ij);
            Fz = ltij.Sum(x => x.Fz_ij);
            Wd = Fz + Fnz;

            Fx_textBox.Text = Fx.ToString("0.00");
            R_textBox.Text = R.ToString("0.00");
            Fd_textBox.Text = Fd.ToString("0.00");
            Wd_textBox.Text = Wd.ToString("0.00");


        }
        private void Obliczenia()
        {
            Output Output;
            OutputList = new List<Output>();

            int i;   int j;    int lp = 0;  double imax = 1;
            double bpj = 0; double zij = 0; double yij = 0;
            double xij = 0; double fp;      double f = 0;
            double lpj = 0; double hpj = 0; double lt;
            double bt;      double At;      double ht; double z;
            double hp1;

            for (j = 0; j <= jmax; j++)
            {
                z = jmax * dh;
                
                for (i = 0; i <= imax; i++)
                {
                    if (j == 0)
                    {
                        f = ro - rd;
                        fp = Math.Pow((wH - rd), 2);
                        bpj = (1 - (fp / Math.Pow(wH, 2))) * (Math.Pow((bo + bd), 2));
                        bpj = Math.Abs(bpj);
                        bpj = Math.Sqrt(bpj);
                        imax = bpj / yb;
                        yij = yb * i;
                        zij = z;
                        lp += 1;
                        hpj = dh * jmax;
                        lt = Math.Sqrt(ro - Math.Pow(ro - rd, 2));
                        bt = Math.Sqrt((1 - (fp / Math.Pow(wH, 2))) * (Math.Pow((bo + bd), 2)));
                        At = Math.PI * lt * bt;
                        ht = dh * j;//ZAMIAST a
                    }
                    if ((j > 0) && (j <= jmax))
                    {
                        lp += 1;
                        fp = Math.Pow((wH - rd - j * dh), 2);
                        bpj = ((1 - (fp / Math.Pow(wH, 2)))) * (Math.Pow((bo + bd), 2));
                        bpj = Math.Abs(bpj);
                        bpj = Math.Sqrt(bpj);
                       
                        zij = (jmax - j) * dh;
                        imax = bpj / yb;

                        yij = yb * i;
                        xij = (1 - ((yij * yij) / (bpj * bpj))) * Math.Pow(lpj, 2);
                        xij = Math.Sqrt(xij);
                        if (j == 1)
                        {
                            hpj = z - (dh / 2);
                        }

                        f = ro - rd - j * dh;
                        lpj = (ro * ro) - (f * f);
                        lpj = Math.Abs(lpj);
                        lpj = Math.Sqrt(lpj);
                    }
                    OutputList.Add(new Output
                    {
                        _lp = lp,
                        _j = j,
                        _i = Convert.ToInt32(i),
                        _jmax = jmax,
                        _bpj = bpj,
                        _hpj = hpj,
                        _imax = imax,
                        _f = f,
                        _lpj = lpj,
                        _xij = xij,
                        _yij = yij,
                        _zij = zij
                    });
                    PokazWynikiCzastkowe(OutputList);
                }
                hpj = hpj - dh;
            }
        }
        private void PokazWynikiCzastkowe(List<Output> OutputList)
        {
            listView1.Items.Clear();
            Output Output;
            for (int o = 0; o < OutputList.Count; o++)
            {
                Output = OutputList[o];

                listView1.Items.Add(Output._lp.ToString());
                listView1.Items[o].SubItems.Add(Output._j.ToString());
                listView1.Items[o].SubItems.Add(Output._i.ToString());
                listView1.Items[o].SubItems.Add(Output._jmax.ToString("0.00"));
                listView1.Items[o].SubItems.Add(Output._imax.ToString("0.00"));
                listView1.Items[o].SubItems.Add(Output._f.ToString("0.00"));
                listView1.Items[o].SubItems.Add(Output._bpj.ToString("0.00"));
                listView1.Items[o].SubItems.Add(Output._hpj.ToString("0.00"));
                listView1.Items[o].SubItems.Add(Output._lpj.ToString("0.00"));
                listView1.Items[o].SubItems.Add(Output._xij.ToString("0.00"));
                listView1.Items[o].SubItems.Add(Output._yij.ToString("0.00"));
                listView1.Items[o].SubItems.Add(Output._zij.ToString("0.00"));
            }
        }

        private void back_label_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void back_label_MouseEnter(object sender, EventArgs e)
        {
            back_label.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void back_label_MouseLeave(object sender, EventArgs e)
        {
            back_label.BackColor = Color.FromArgb(33, 50, 70);
        }

        private void save_label_Click(object sender, EventArgs e)
        {
            saveXLSDialog.ShowDialog();


        }

        private void save_label_MouseEnter(object sender, EventArgs e)
        {
            save_label.BackColor = Color.FromArgb(74, 160, 232);
            pictureBox3.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void save_label_MouseLeave(object sender, EventArgs e)
        {
            save_label.BackColor = Color.FromArgb(33, 50, 70);
            pictureBox3.BackColor = Color.FromArgb(33, 50, 70);
        }

        private void schema_label_Click(object sender, EventArgs e)
        {

        }

        private void schema_label_MouseEnter(object sender, EventArgs e)
        {
            schema_label.BackColor = Color.FromArgb(74, 160, 232);
            pictureBox1.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void schema_label_MouseLeave(object sender, EventArgs e)
        {
            schema_label.BackColor = Color.FromArgb(33, 50, 70);
            pictureBox1.BackColor = Color.FromArgb(33, 50, 70);
        }

        private void marks_label_Click(object sender, EventArgs e)
        {

        }

        private void marks_label_MouseEnter(object sender, EventArgs e)
        {
            marks_label.BackColor = Color.FromArgb(74, 160, 232);
            pictureBox4.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void marks_label_MouseLeave(object sender, EventArgs e)
        {
            marks_label.BackColor = Color.FromArgb(33, 50, 70);
            pictureBox4.BackColor = Color.FromArgb(33, 50, 70);
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.FromArgb(33, 50, 70);
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

}
