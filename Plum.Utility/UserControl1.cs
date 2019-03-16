using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plum.Utility
{
    public partial class FloatTextBox : UserControl
    {
        public FloatTextBox()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double Number;
            bool valid = double.TryParse(Float.Text.ToString(), out Number);
            if (valid == false)
            {
                Float.Text = "0.0";
            }
        }
    }
}
