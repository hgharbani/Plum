using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plum.Form.Food;

namespace Plum
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMaterial_Click(object sender, EventArgs e)
        {
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnMaterial_Click_1(object sender, EventArgs e)
        {
            var materialIndex=new Index();
            materialIndex.ShowDialog();
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            var FoodIndex = new FoodIndex();
            FoodIndex.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            WindowState = FormWindowState.Maximized;
        }
    }
}