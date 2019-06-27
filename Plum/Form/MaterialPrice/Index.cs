using Plum.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using PersianDate;
using Plum.Form.MaterialPrice;

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
            DialogResult createMaterial = new createMaterial().ShowDialog();
            if (createMaterial == DialogResult.OK)
            {
                ShowMaterialGrid();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowMaterialGrid()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dataGridView1.AutoGenerateColumns = false;
                List<Data.MaterialPrice> material = db.MaterialRepositories.GetAll(true);
                TotalCount.Text = ((material.Count())).ToString();
                if (material.Count() <= 15)
                {
                    button1.Enabled = false;
                }
                dataGridView1.DataSource = material;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    var x = ((DateTime)row.Cells[3].Value).ToFa();
                    row.Cells[3].Value = x;


                }
            }




        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                using (UnitOfWork db = new UnitOfWork())
                {
                    Data.MaterialPrice model = db.MaterialRepositories.GetOne(id);
                    EditMateril formEdit = new EditMateril();
                    formEdit.Id.Text = id.ToString();
                    formEdit.cmdMaterial.SelectedText = model.Material.MaterialName;
                    formEdit.cmdCompany.SelectedText = model.Company.CompanyName;
                    formEdit.UnitPrice.Text = model.UnitPrice.ToString();

                    if (formEdit.ShowDialog() == DialogResult.OK)
                    {
                        ShowMaterialGrid();
                    }
                }

            }
            else
            {
                RtlMessageBox.Show("آیتمی انتخاب نشده است");
            }

        }

        private void FoodIndex_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Text = "1";
            ShowMaterialGrid();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                using (UnitOfWork db = new UnitOfWork())
                {

                    int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    string name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    if (RtlMessageBox.Show($"ایا از حذف {name} مطمئن هستید", "توجه", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Data.MaterialPrice model = db.MaterialRepositories.GetOne(id);
                        if (model.FoodMaterials.Any())
                        {
                            RtlMessageBox.Show("قادر به حذف این کالا نمی باشد زیرا در چندین غذا در حال استفاده است");
                        }
                        else
                        {
                            bool result = db.MaterialRepositories.DeleteMaterial(id);
                            db.Save();

                            RtlMessageBox.Show("کالا با موفقیت حذف شد");
                        }
                    }

                }

                ShowMaterialGrid();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                if (textBox2.Text == "")
                {
                    ShowMaterialGrid();
                }
                else
                {
                    var model = db.MaterialRepositories.GetMaterials(textBox2.Text).Where(a => a.Active);

                    dataGridView1.DataSource = model;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var x = ((DateTime)row.Cells[3].Value).ToFa();
                        row.Cells[3].Value = x;


                    }

                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) > 1)
            {
                textBox1.Text = (int.Parse(textBox1.Text) - 1).ToString();
            }

        }
    }
}
