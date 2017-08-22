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

namespace TractionCalc
{
    public partial class Calc : Form
    {
        public string index;
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
        public ListView lvi;
        

        public Calc(string index1, TextBox p_ro, TextBox s_bo, TextBox w_H, 
            TextBox wys_h, TextBox rd, TextBox bd, TextBox dh, TextBox K, 
            TextBox c, TextBox fi, TextBox n, TextBox Wd, TextBox ktr, TextBox bp, TextBox yb, ListView lv)
        {
            InitializeComponent();
            index = index1;
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
        }
       
        private void Calc_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            List<Dane> DaneCalc1;
            List<Dane> DaneCalc2;
            try
            {
                if (lvi.SelectedItems.Count > 0)
                {
                    DaneCalc1 = Connect.DaneObliczenia1(index);
                    DaneCalc2 = Connect.DaneObliczenia2(index);

                    if ((DaneCalc1.Count > 0 || DaneCalc1.Count > 0) && (DaneCalc2.Count > 0 || DaneCalc2.Count > 0))
                    {
                        Dane dane;
                        Dane dane1;
                        string fii;
                        for (int i = 0; i < DaneCalc1.Count; i++)
                        {
                            dane = DaneCalc1[i];
                            ro = Convert.ToDouble(dane.promienro.ToString());
                            bo = Convert.ToDouble(dane.szerbo.ToString());
                            wH = Convert.ToDouble(dane.WysH.ToString());
                            wh = Convert.ToDouble(dane.Wys_h.ToString());
                            rd = Convert.ToDouble(dane.Rd.ToString());
                            bd = Convert.ToDouble(dane.Bd.ToString());
                            dh = Convert.ToDouble(dane.Dh.ToString());
                        }
                        for (int j = 0; j < DaneCalc2.Count; j++)
                        {
                            dane1 = DaneCalc2[j];
                            // = Convert.ToDouble(dane1.ID.ToString());
                            fii = dane1.Fi.ToString();
                            if (!string.IsNullOrWhiteSpace(fii))
                            {
                                fi = Convert.ToDouble(dane1.Fi.ToString());
                            }
                            else
                            {
                                fi = 0;
                            }
                            spc = Convert.ToDouble(dane1.Spc.ToString());
                            wspk = Convert.ToDouble(dane1.WspK.ToString());
                            wykn = Convert.ToDouble(dane1.Wykn.ToString());
                            K = Convert.ToDouble(dane1.Wsp_odrz_k.ToString());
                            bp = Convert.ToDouble(dane1.Bp.ToString());
                            wd = Convert.ToDouble(dane1.Wd.ToString());
                            yb = Convert.ToDouble(dane1.y_b.ToString());

                        }

                        
                    }
                    else { MessageBox.Show("Brak rekordów.", "Alert"); }
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

                }
                Obliczenia();
                DaneListView();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString()); }
        }

        private void DaneListView()
        {
            int a = 0;
            int b = 0;
            int i = 0;
            int j = 0;
            int imax = 3;
            int jmax = 5;
            for (int o = 0; o <= 10; o++)
            {
                j = o;
                b = o;
                while ((i < imax) && (j < jmax))
                {
                        if ((i == a) && (j == b))
                        {
                            label4.Text += " X(" + j + "," + i + ") ";
                            label3.Text += " Y(" + j + "," + i + ") ";
                            label5.Text += " Z(" + j + "," + i + ") ";
                        a++;

                            if (((i + 1) == a) && (j == b))
                            {
                                i = a;
                                label4.Text += " X(" + j + "," + i + ") ";
                                label3.Text += " Y(" + j + "," + i + ") ";
                                label5.Text += " Z(" + j + "," + i + ") ";
                            b++;
                                a--;
                                i = a;

                                if ((i == a) && ((j + 1) == b))
                                {
                                    j = b;
                                    label4.Text += " X(" + j + "," + i + ") ";
                                    label3.Text += " Y(" + j + "," + i + ") ";
                                    label5.Text += " Z(" + j + "," + i + ") ";
                                a++;

                                    if (((i + 1) == a) && (j == b))
                                    {
                                        j = b;
                                        i = a;
                                        label4.Text += " X(" + j + "," + i + ") \n";
                                        label3.Text += " Y(" + j + "," + i + ") \n";
                                        label5.Text += " Z(" + j + "," + i + ") \n";
                                }
                                }
                            }
                            j--;
                            b--;
                        }   
                }
                i = 0;
                j = 0;
                a = 0;
                b = 0;
                imax = 4;
            }
        }
        private void Obliczenia()
        {
            Output Output;
            var OutputList = new List<Output>();

            
            int i;
            int j;
            int lp = 0;
            double imax = 1;
            double jmax = 1;
            double bpj= 0;
            double zij = 0;
            double yij = 0;
            double xij = 0;
            double fp;
            double f;
            double lpj = 0;
            double hpj;

            for (j = 0; j <= jmax; j++)
            {


                if (j == 0)
                {
                    for (i = 0; i <= imax; i++)
                    {
                        f = ro - rd;
                        fp = Math.Pow((wH - rd), 2);
                        bpj = (1 - (fp / Math.Pow(wH, 2))) * (Math.Pow((bo + bd), 2));
                        bpj = Math.Abs(bpj);
                        bpj = Math.Sqrt(bpj);
                        imax = bpj / yb;
                        yij = yb * i;
                        zij = dh * j;
                        lp += 1;

                        OutputList.Add(new Output
                        {
                            _lp = lp,
                            _j = j,
                            _i = i,
                            _jmax = jmax,
                            _bpj = bpj,
                            _imax = imax,
                            _f = f,
                            // _lpj = lpj,
                            // _xij = xij
                            _yij = yij,
                            _zij = zij

                        });
                    }
                }
                if ((j > 0) && (j <= jmax))
                {
                    f = ro - rd - j * dh;

                    lpj = (ro * ro) - (f * f);
                    lpj = Math.Abs(lpj);
                    lpj = Math.Sqrt(lpj);



                    for (i = 0; i <= imax; i++)
                    {

                        lp += 1;
                        fp = Math.Pow((wH - rd - j * dh), 2);
                        bpj = ((1 - (fp / Math.Pow(wH, 2)))) * (Math.Pow((bo + bd), 2));
                        bpj = Math.Abs(bpj);
                        bpj = Math.Sqrt(bpj);
                        zij = dh * j;
                        jmax = lpj;
                        imax = bpj / yb;

                        yij = yb * i;
                        xij = (1 - ((yij * yij) / (bpj * bpj))) * Math.Pow(lpj, 2);
                        xij = Math.Sqrt(xij);
                        hpj = dh * j - (dh / 2);
                        if (bpj >= yij)
                        {
                            OutputList.Add(new Output
                            {
                                _lp = lp,
                                _j = j,
                                _i = Convert.ToInt32(i),
                                _jmax = jmax,

                                _bpj = bpj,
                                _imax = imax,
                                _f = f,
                                _lpj = lpj,
                                _xij = xij,
                                _yij = yij,
                                _zij = zij
                            });
                        }
                    }
            }
            
            }
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
                listView1.Items[o].SubItems.Add(Output._lpj.ToString("0.00"));
                listView1.Items[o].SubItems.Add(Output._xij.ToString("0.00"));
                listView1.Items[o].SubItems.Add(Output._yij.ToString("0.00"));
                listView1.Items[o].SubItems.Add(Output._zij.ToString("0.00"));
            }  
          
        }

        private void label17_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label17_MouseEnter(object sender, EventArgs e)
        {
            label17.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void label17_MouseLeave(object sender, EventArgs e)
        {
            label17.BackColor = Color.FromArgb(33, 50, 70);
        }

    }
    
}
