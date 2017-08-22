using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace TractionCalc
{
    public partial class WarningBox : Form
    {
        string msg;
        public WarningBox(string wiadomosc)
        {
            msg = wiadomosc;
            InitializeComponent();
            txt_label.Text = msg;
        }

        private void WarningBox_Load(object sender, EventArgs e)
        {
        }

        private void txt_label_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ok_label_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ok_label_MouseEnter(object sender, EventArgs e)
        {
            Ok_label.BackColor = SystemColors.ActiveBorder;
        }

        private void Ok_label_MouseLeave(object sender, EventArgs e)
        {
            Ok_label.BackColor = SystemColors.InactiveCaptionText;
        }
    }
}
