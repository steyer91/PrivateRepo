using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace TractionCalc
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            
            InitializeComponent();
          //  listView2.Items[0].Selected = true;
            //listView1.Items[0].Selected = true;

        }

        public string index1;
        private bool mouseDown;
        private Point lastLocation;
        protected bool czypuste()
        {
            List<TextBox> textboxes = new List<TextBox>();
            textboxes.Add(p_roTxt);
            textboxes.Add(s_boTxt);
            textboxes.Add(wys_hTxt);
            textboxes.Add(w_HTxt);
            textboxes.Add(rdTxt);
            textboxes.Add(bdTxt);
            textboxes.Add(delhTxt);
            textboxes.Add(fi_Txt);
            textboxes.Add(c_Txt);
            textboxes.Add(K_Txt);
            textboxes.Add(n_Txt);
            textboxes.Add(k_trakTxt);
            textboxes.Add(bewam_bpTxt);
            textboxes.Add(Wd_obcTxt);
            textboxes.Add(yb_txt);
           
            int i = 0;

            foreach (TextBox tb in textboxes)
            {
                if (String.IsNullOrWhiteSpace(tb.Text))
                {

                }

                else
                {
                    i++;
                }
            }
            if (i == textboxes.Count)
            {
               // label101.Visible = false;
                return false;

            }
            else
            {
              //  label101.Visible = true;
                return true;
            }

        }
        private void InputForm_Load(object sender, EventArgs e)
        {
            
            listView1.Items.Clear();
            listView2.Items.Clear();
            List<Dane> ListaDane1;
            List<Dane> ListaDane;
            try
            {
                ListaDane = Connect.DataList();
                ListaDane1 = Connect.DataList1();
                if (ListaDane.Count > 0 || ListaDane1.Count > 0)
                {
                    Dane dane;
                    Dane dane1;

                    for (int i = 0; i < ListaDane.Count; i++)
                    {
                        dane = ListaDane[i];
                        listView1.Items.Add(dane.ID.ToString());
                        listView1.Items[i].SubItems.Add(dane.promienro.ToString());
                        listView1.Items[i].SubItems.Add(dane.szerbo.ToString());
                        listView1.Items[i].SubItems.Add(dane.WysH.ToString());
                        listView1.Items[i].SubItems.Add(dane.Wys_h.ToString());
                        listView1.Items[i].SubItems.Add(dane.Rd.ToString());
                        listView1.Items[i].SubItems.Add(dane.Bd.ToString());
                        listView1.Items[i].SubItems.Add(dane.Dh.ToString());
                    }
                   
                    for (int j = 0; j < ListaDane1.Count; j++)
                    {
                        dane1 = ListaDane1[j];
                        listView2.Items.Add(dane1.ID.ToString());
                        listView2.Items[j].SubItems.Add(dane1.Fi.ToString());
                        listView2.Items[j].SubItems.Add(dane1.Spc.ToString());
                        listView2.Items[j].SubItems.Add(dane1.WspK.ToString());
                        listView2.Items[j].SubItems.Add(dane1.Wykn.ToString());
                        listView2.Items[j].SubItems.Add(dane1.Wsp_odrz_k.ToString());
                        listView2.Items[j].SubItems.Add(dane1.Bp.ToString());
                        listView2.Items[j].SubItems.Add(dane1.Wd.ToString());
                        listView2.Items[j].SubItems.Add(dane1.y_b.ToString());

                    }
                }
                else { MessageBox.Show("Brak rekordów.", "Alert"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString()); }
        }

        //private void Dodaj_Click(object sender, EventArgs e)
        //{
        //    Connect.AddData(p_roTxt.Text, s_boTxt.Text, w_HTxt.Text, wys_hTxt.Text, rdTxt.Text, bdTxt.Text, delhTxt.Text);
        //    p_roTxt.Text = "";
        //    s_boTxt.Text = "";
        //    w_HTxt.Text = "";
        //    wys_hTxt.Text = "";
        //    rdTxt.Text = "";
        //    bdTxt.Text = "";
        //    delhTxt.Text = "";
        //    this.InputForm_Load(this, null);

        //}

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            string index = "";
            try
            {
               
                foreach (ListViewItem item in listView2.Items)
                {

                  if(  item.Selected == true )
                    {
                        item.Selected = false;
                    }



                }
                index1 = "";

                foreach (int i in listView1.SelectedIndices)
                {
                    index = listView1.Items[i].ToString();
                    index1 = listView1.Items[i].SubItems[0].Text.ToString();
                    listView2.Items[i].Selected = true;
                }

                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView1.SelectedItems[0];
                    p_roTxt.Text = item.SubItems[1].Text;
                    s_boTxt.Text = item.SubItems[2].Text;
                    w_HTxt.Text = item.SubItems[3].Text;
                    wys_hTxt.Text = item.SubItems[4].Text;
                    rdTxt.Text = item.SubItems[5].Text;
                    bdTxt.Text = item.SubItems[6].Text;
                    delhTxt.Text = item.SubItems[7].Text;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
               
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string index = "";
            //foreach (ListViewItem item in listView1.Items)
            //{
            //    item.Selected = false; 
            //}
            try
            {
                index = "";

                foreach (int i in listView2.SelectedIndices)
                {
                    //if (!listView2.Items[i].Selected == false || listView1.Items[i].Selected == false)
                    //{
                    if (!listView2.Items[i].Selected == false)
                    {
                        index = listView2.Items[i].ToString();
                        listView1.Items[i].Selected = true;
                        listView2.Items[i].Selected = true;
                    }
                    //}
                }
                if (listView2.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView2.SelectedItems[0];
                    
                    fi_Txt.Text = item.SubItems[1].Text;
                    c_Txt.Text = item.SubItems[2].Text;
                    K_Txt.Text = item.SubItems[3].Text;
                    n_Txt.Text = item.SubItems[4].Text;
                    k_trakTxt.Text = item.SubItems[5].Text;
                    bewam_bpTxt.Text = item.SubItems[6].Text;
                    Wd_obcTxt.Text = item.SubItems[7].Text;
                    yb_txt.Text = item.SubItems[8].Text;
                }
            }
            catch (Exception ex) { ex.ToString(); }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Close cls = new TractionCalc.Close();
            cls.ShowDialog();
            InputForm inf = new InputForm();
            inf.Enabled = false;
        }

        private void Dodaj_label19_Click(object sender, EventArgs e)
        {
            Connect.AddData(p_roTxt.Text, s_boTxt.Text, w_HTxt.Text, wys_hTxt.Text, rdTxt.Text, bdTxt.Text, delhTxt.Text, fi_Txt.Text,
            c_Txt.Text, K_Txt.Text, n_Txt.Text, k_trakTxt.Text, bewam_bpTxt.Text, Wd_obcTxt.Text, yb_txt.Text);
            p_roTxt.Text = "";
            s_boTxt.Text = "";
            w_HTxt.Text = "";
            wys_hTxt.Text = "";
            rdTxt.Text = "";
            bdTxt.Text = "";
            delhTxt.Text = "";
            fi_Txt.Text = "";
            c_Txt.Text = "";
            K_Txt.Text = "";
            n_Txt.Text = "";
            k_trakTxt.Text = "";
            bewam_bpTxt.Text = "";
            Wd_obcTxt.Text = "";
            this.InputForm_Load(this, null);

        }

        private void Usun_label20_Click(object sender, EventArgs e)
        {
            //listView1.Items.Clear();

            foreach (int i in listView1.SelectedIndices)
            {
                Connect.DeleteData(listView1.Items[i].Text);
                listView1.Items.Remove(listView1.Items[i]);
            }
            this.InputForm_Load(this, null);
        } 
        private void label16_Click(object sender, EventArgs e)
        {
            Intro intr = new Intro();
            intr.ShowDialog();
            this.Hide();
        }

        private void DoObl_label15_Click(object sender, EventArgs e)
        {
            string warn = "Zanim przejdziesz do obliczeń, należy wypełnić wszystkie pola wartościami!";
            czypuste();
            Calc clc = new Calc(index1, p_roTxt, s_boTxt, w_HTxt, wys_hTxt, 
                rdTxt, bdTxt, delhTxt, K_Txt, c_Txt, fi_Txt, n_Txt, 
                Wd_obcTxt, k_trakTxt, bewam_bpTxt, yb_txt, listView1);
            WarningBox wrn = new WarningBox(warn);
            if (czypuste() == true)
            {
                wrn.Show();
                czypuste();
            }
            else
            {
                czypuste();
                clc.ShowDialog();
            }
        }
        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click_1(object sender, EventArgs e)
        {

        }
        
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(74,160,232);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(33, 50, 70);
        }

        private void label15_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(74, 160, 232);
            label15.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void label15_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(33, 50, 70);
            label15.BackColor = Color.FromArgb(33, 50, 70);
        }
        private void label16_MouseEnter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(74, 160, 232);
            label16.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void label16_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(33, 50, 70);
            label16.BackColor = Color.FromArgb(33, 50, 70);
        }

        private void label17_MouseEnter(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(74, 160, 232);
            label17.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void label17_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(33, 50, 70);
            label17.BackColor = Color.FromArgb(33, 50, 70);

        }

        private void label18_MouseEnter(object sender, EventArgs e)
        {

        }

        private void label18_MouseLeave(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
           
            panel4.BackColor = Color.FromArgb(33, 50, 70);
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            panel4.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void InputForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void InputForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void InputForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
