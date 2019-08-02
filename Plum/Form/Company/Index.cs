using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersianDate;
using Plum.Form.MaterialPrice;
using Plum.Services;

namespace Plum.Form.Company
{
    public partial class Index : System.Windows.Forms.Form
    {
        public Index()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            DialogResult createMaterial = new CreateCompany().ShowDialog();
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
                List<Data.Company> companyList = db.CompanyService.GetAll().ToList();

                dataGridView1.DataSource = companyList;

            }

        }

        private void Index_Load(object sender, EventArgs e)
        {
            ShowMaterialGrid();
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
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

                        var result = db.CompanyService.DeleteCompany(id);
                        if (result.IsChange)
                        {
                            db.Save();

                            RtlMessageBox.Show(result.Message);

                        }
                        else
                        {
                            RtlMessageBox.Show(result.Message);

                        }
                    }
                }

            }

            ShowMaterialGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                if (txtCompanyName.Text == "")
                {
                    ShowMaterialGrid();
                }
                else
                {
                    var model = db.CompanyService.GetByParamert(txtCompanyName.Text);

                    dataGridView1.DataSource = model;

                }
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {

                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var findCompany = db.CompanyService.GetOne(id);
                string name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                var form = new CreateCompany();
                form.companyId = id;
                form.txtCompanyName.Text = findCompany.CompanyName;

                DialogResult createMaterial = form.ShowDialog();
                if (createMaterial == DialogResult.OK)
                {
                    ShowMaterialGrid();
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {

                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var findCompany = db.CompanyService.GetOne(id);
                string name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                var form = new CreateCompany();
                form.companyId = id;
                form.txtCompanyName.Text = findCompany.CompanyName;
                DialogResult createMaterial = form.ShowDialog();
               
                if (createMaterial == DialogResult.OK)
                {
                    ShowMaterialGrid();
                }
            }
        }
    }
}

