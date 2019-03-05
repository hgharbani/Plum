using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plum.Form.Material;

namespace Plum.Form.Food
{
    public partial class Index : System.Windows.Forms.Form
    {
        public Index()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var createMaterial = new createMaterial().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var editMaterial=new EditMateril().ShowDialog();
        }

        private void FoodIndex_Load(object sender, EventArgs e)
        {

        }
    }
}
