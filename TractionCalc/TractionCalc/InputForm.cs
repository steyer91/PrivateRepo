using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TractionCalc.Models;

namespace TractionCalc
{
    public partial class InputForm : Form
    {
        public InputForm()
        {

            InitializeComponent();

        }

        public DaneRepository DaneRepository;
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
            textboxes.Add(jmax_textBox);

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
                return false;

            }
            else
            {
                return true;
            }

        }
        private void InputForm_Load(object sender, EventArgs e)
        {
            DaneRepository = new DaneRepository(NHManager.OpenSession());
            DaneRepository.DaneList();
            try
            {

                if (DaneRepository.DaneList().Count > 0)
                {
                    listView1.Items.Clear();
                    listView2.Items.Clear();

                    var daneList = DaneRepository.DaneList();

                    for (int i = 0; i < daneList.Count; i++)
                    {
                        //pierwszy listView
                        listView1.Items.Add(daneList[i].ID.ToString());
                        listView1.Items[i].SubItems.Add(daneList[i].promien_ro);
                        listView1.Items[i].SubItems.Add(daneList[i].szerokosc_bo);
                        listView1.Items[i].SubItems.Add(daneList[i].wysH);
                        listView1.Items[i].SubItems.Add(daneList[i].wys_h);
                        listView1.Items[i].SubItems.Add(daneList[i].rd);
                        listView1.Items[i].SubItems.Add(daneList[i].bd);
                        listView1.Items[i].SubItems.Add(daneList[i].delta_h);

                        //drugi listView
                        listView2.Items.Add(daneList[i].ID.ToString());
                        listView2.Items[i].SubItems.Add(daneList[i].fi);
                        listView2.Items[i].SubItems.Add(daneList[i].sp_c);
                        listView2.Items[i].SubItems.Add(daneList[i].wsp_K);
                        listView2.Items[i].SubItems.Add(daneList[i].wyk_n);
                        listView2.Items[i].SubItems.Add(daneList[i].wsp_odkrzt_k);
                        listView2.Items[i].SubItems.Add(daneList[i].bp);
                        listView2.Items[i].SubItems.Add(daneList[i].Wd);
                        listView2.Items[i].SubItems.Add(daneList[i].yb);
                    }
                }
                else { MessageBox.Show("Brak rekordów.", "Alert"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString()); }

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string index = "";
            try
            {

                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    try
                    {
                        // item.Selected = false;
                        listView2.SelectedItems.Clear();
                    }
                    catch(Exception)
                    {

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
            try
            {
                index = "";

                foreach (int i in listView2.SelectedIndices)
                {
                    if (!listView2.Items[i].Selected == false)
                    {
                        index = listView2.Items[i].ToString();
                        listView1.Items[i].Selected = true;
                        listView2.Items[i].Selected = true;
                    }
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
            var data = new Dane()
            {
                wyk_n = n_Txt.Text,
                wsp_odkrzt_k = k_trakTxt.Text,
                sp_c = c_Txt.Text,
                wsp_K = K_Txt.Text,
                Wd = Wd_obcTxt.Text,
                bd = bdTxt.Text,
                bp = bewam_bpTxt.Text,
                delta_h = delhTxt.Text,
                fi = fi_Txt.Text,
                promien_ro = p_roTxt.Text,
                rd = rdTxt.Text,
                szerokosc_bo = s_boTxt.Text,
                wysH = w_HTxt.Text,
                wys_h = wys_hTxt.Text,
                yb = yb_txt.Text
            };

            DaneRepository = new DaneRepository(NHManager.OpenSession());
            DaneRepository.SaveData(data);
            this.InputForm_Load(this, null);
        }

        private void Usun_label20_Click(object sender, EventArgs e)
        {
            foreach (int i in listView1.SelectedIndices)
            {
                DaneRepository = new DaneRepository(NHManager.OpenSession());
                var listViewItem = listView1.Items[i].Text;
                DaneRepository.DeleteData(int.Parse(listViewItem));
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
            try
            {
                int index = int.Parse(index1);
                string warn = "Zanim przejdziesz do obliczeń, należy wypełnić wszystkie pola wartościami!";
                czypuste();
                Calc clc = new Calc(index, p_roTxt, s_boTxt, w_HTxt, wys_hTxt,
                    rdTxt, bdTxt, delhTxt, K_Txt, c_Txt, fi_Txt, n_Txt,
                    Wd_obcTxt, k_trakTxt, bewam_bpTxt, yb_txt, listView1, jmax_textBox);
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
            catch (Exception)
            {
                string warn = "Zanim przejdziesz do obliczeń, należy wypełnić wszystkie pola wartościami!";
                WarningBox wrn = new WarningBox(warn);
                wrn.Show();
                czypuste();
            }
        }
        private void label15_Click(object sender, EventArgs e)
        {
            Intro intro = new Intro();
            intro.Show();
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
            panel1.BackColor = Color.FromArgb(74, 160, 232);
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

        private void Dodaj_label19_MouseEnter(object sender, EventArgs e)
        {
            Dodaj_label19.BackColor = Color.FromArgb(33, 50, 70);
        }

        private void Dodaj_label19_MouseLeave(object sender, EventArgs e)
        {
            Dodaj_label19.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void Usun_label20_MouseEnter(object sender, EventArgs e)
        {
            Usun_label20.BackColor = Color.FromArgb(33, 50, 70);
        }

        private void Usun_label20_MouseLeave(object sender, EventArgs e)
        {
            Usun_label20.BackColor = Color.FromArgb(74, 160, 232);
        }

        private void DoObl_label15_MouseEnter(object sender, EventArgs e)
        {
            DoObl_label15.BackColor = Color.FromArgb(33, 50, 70);
        }

        private void DoObl_label15_MouseLeave(object sender, EventArgs e)
        {
            DoObl_label15.BackColor = Color.FromArgb(74, 160, 232);
        }
    }
}
