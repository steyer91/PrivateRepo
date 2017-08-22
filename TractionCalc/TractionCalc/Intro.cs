using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TractionCalc
{
    public partial class Intro : Form
    {
        public Intro()
        {
          
            InitializeComponent();
        }

      
    
   

        private void label15_Click(object sender, EventArgs e)
        {
            InputForm inf = new InputForm();
            this.Hide();
            inf.ShowDialog();
            this.Close();
        }

        private void label15_MouseEnter(object sender, EventArgs e)
        {
            label15.BackColor = Color.FromArgb(33, 50, 70);
        }
        private void label15_MouseLeave_1(object sender, EventArgs e)
        {
            label15.BackColor = Color.FromArgb(74, 160, 232);
        }
    }
}
