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
                List<Data.Company> companyList = db.CompanyService.GetAll().ToList();

                dataGridView1.DataSource = companyList;

            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                using (UnitOfWork db = new UnitOfWork())
                {
                    Data.Company model = db.CompanyService.GetOne(id);
                    CreateCompany formEdit = new CreateCompany();
                    formEdit._ModelCompany.CompanyId = model.CompanyId;
                    formEdit.txtCompanyName.Text = model.CompanyName;

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

        private void Index_Load(object sender, EventArgs e)
        {
            ShowMaterialGrid();
        }

        private void txtCompanyName_TextChanged(object sender, EventArgs e)
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
    }
}

