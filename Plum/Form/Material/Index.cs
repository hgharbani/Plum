using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersianDate;
using Plum.Form.MaterialPrice;
using Plum.Model.Model.MAterial;
using Plum.Services;

namespace Plum.Form.Material
{
    public partial class Index : System.Windows.Forms.Form
    {

        public Index()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                int id = int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
                using (UnitOfWork db = new UnitOfWork())
                {
                    Data.Material model = db.MaterialService.GetOne(id);
                    CreateMaterial formEdit = new CreateMaterial();
                    formEdit.MaterialId = id;
                    formEdit.MaterialName.Text = model.MaterialName;

                    if (formEdit.ShowDialog() == DialogResult.OK)
                    {
                        ShowMaterialGrid();
                        dataGridView2.ClearSelection();
                        dataGridView2.CurrentCell = null;
                    }
                }

            }
            else
            {
                var materialIndex = new CreateMaterial();
                materialIndex.deleteMAterial.Visible = false;
                var result = materialIndex.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ShowMaterialGrid();
                    dataGridView2.ClearSelection();
                    dataGridView2.CurrentCell = null;
                }
            }

        }

        private void Index_Load(object sender, EventArgs e)
        {
            GetCompany();
            ShowMaterialGrid();
            dataGridView2.ClearSelection();
            dataGridView2.CurrentCell = null;

        }

        private void GetCompany()
        {
            using (UnitOfWork db = new UnitOfWork())
            {

                var companymodel = new List<Data.Company>()
                {
                    new Data.Company()
                    {
                        CompanyId = -1,
                        CompanyName = "--SELECT--"
                    }
                };
                companymodel.AddRange(db.CompanyService.GetAll()
                );
                cmbCompany.DataSource = companymodel;

            }
        }


        private void ShowMaterialGrid()
        {

            var company = (Data.Company)cmbCompany.SelectedItem;
            var mdel = new MaterialModel()
            {
                MaterialName = txtMaterialName.Text,
                CompanyId = company?.CompanyId ?? 0
            };
            using (UnitOfWork db = new UnitOfWork())
            {
                dataGridView2.AutoGenerateColumns = false;
                var material = db.MaterialService.GetMaterialModel(mdel);

                dataGridView2.DataSource = material;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var materialIndex = new MaterialPrice.Index();
            var result = materialIndex.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                ShowMaterialGrid();
            }

        }

        private void Index_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            dataGridView2.CurrentCell = null;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            dataGridView2.CurrentCell = null;
        }



        private void panel5_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            dataGridView2.CurrentCell = null;
        }

        private void txtMaterialName_TextChanged(object sender, EventArgs e)
        {
            var company = (Data.Company)cmbCompany.SelectedItem;

            var materialMode = new MaterialModel()
            {
                MaterialName = txtMaterialName.Text,
                CompanyId = company?.CompanyId ?? 0
            };
            using (UnitOfWork db = new UnitOfWork())
            {
                dataGridView2.AutoGenerateColumns = false;
                var material = db.MaterialService.GetMaterialModel(materialMode);

                dataGridView2.DataSource = material;

            }
        }


    }
}
