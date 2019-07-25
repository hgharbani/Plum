using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PersianDate;
using Plum.Model.Model.MaterialPrice;
using Plum.Services;

namespace Plum.Form.MaterialPrice
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
            if (createMaterial == DialogResult.Cancel)
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
                var model = material.Select(a => new MaterialPriceModel()
                {
                    MateriaPriceId = a.Id,
                    MateriaName = a.Material.MaterialName,
                    MaterialCompany = a.Company.CompanyName,
                    UnitPrice = a.UnitPrice,
                    InsertTime = a.InsertTime
                }).ToList();
                TotalCount.Text = ((material.Count())).ToString();
                if (material.Count() <= 15)
                {
                    button1.Enabled = false;
                }
                dataGridView1.DataSource = model;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    var x = ((DateTime)row.Cells[4].Value).ToFa();
                    row.Cells[4].Value = x;


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
                    formEdit.CompanyId = model.CompanyId;
                    formEdit.MaterialId = model.MaterialId;
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
            Getcompany();
        }

        private void Getcompany()
        {
            using (UnitOfWork db = new UnitOfWork())
            {

                var companymodel = new List<Data.Company>()
                {
                    new Data.Company()
                    {
                        CompanyId = 0,
                        CompanyName = "--SELECT--"
                    }
                };
                companymodel.AddRange(db.CompanyService.GetAll()
                );
                comboBox2.DataSource = companymodel;

            }
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
                            var result = db.MaterialRepositories.DeleteMaterial(id);
                            if (result.IsChange)
                            {
                                db.Save();
                                MessageBox.Show(result.Message);

                            }
                            else
                            {
                                MessageBox.Show(result.Message);
                            }

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
              
                    var companyId = 0;
                    var company = (Data.Company) comboBox2.SelectedItem;
                    if (company != null)
                    {
                        companyId = company.CompanyId;
                    }
                    var material = db.MaterialRepositories.GetMaterials(textBox2.Text, companyId).ToList();

                    dataGridView1.AutoGenerateColumns = false;
                    var model = material.Select(a => new MaterialPriceModel()
                    {
                        MateriaPriceId = a.Id,
                        MateriaName = a.Material.MaterialName,
                        MaterialCompany = a.Company.CompanyName,
                        UnitPrice = a.UnitPrice,
                        InsertTime = a.InsertTime
                    }).ToList();
                    dataGridView1.DataSource = model;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var x = ((DateTime)row.Cells[4].Value).ToFa();
                        row.Cells[4].Value = x;


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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {

                var companyId = 0;
                var company = (Data.Company)comboBox2.SelectedItem;
                if (company != null)
                {
                    companyId = company.CompanyId;
                }
                var material = db.MaterialRepositories.GetMaterials(textBox2.Text, companyId).ToList();

                dataGridView1.AutoGenerateColumns = false;
                var model = material.Select(a => new MaterialPriceModel()
                {
                    MateriaPriceId = a.Id,
                    MateriaName = a.Material.MaterialName,
                    MaterialCompany = a.Company.CompanyName,
                    UnitPrice = a.UnitPrice,
                    InsertTime = a.InsertTime
                }).ToList();
                dataGridView1.DataSource = model;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var x = ((DateTime)row.Cells[4].Value).ToFa();
                    row.Cells[4].Value = x;


                }


            }
        }
    }
}
