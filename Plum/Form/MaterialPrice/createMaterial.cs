using System;
using System.Windows.Forms;
using Plum.Data;
using Plum.Services;

namespace Plum.Form.MaterialPrice
{
    public partial class createMaterial : System.Windows.Forms.Form
    {
        public createMaterial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var material = (Data.Material)cmbMaterial.SelectedItem;
            var company = (Data.Company)comboBox2.SelectedItem;
            if (material == null)
            {
                errorProvider1.SetError(cmbMaterial, "لطفا کالا را وارد نمایید");
                return;
            }
            if (company == null)
            {
                errorProvider2.SetError(comboBox2, "لطفا شرکت را انتخاب نمایید");
                return;
            }
            if (string.IsNullOrWhiteSpace(UnitPrice.Value))
            {
                errorProvider3.SetError(UnitPrice, "لطفا قیمت را وارد نمایید");
                return;

            }
            using (UnitOfWork db = new UnitOfWork())
            {
             
                var model = new Data.MaterialPrice()
                {
                    MaterialId = material.Id,
                    UnitPrice = int.Parse(UnitPrice.Value),
                    Active = true,
                    InsertTime = DateTime.Now,
                    ParentId = null,
                    MaterialTypeData = Data.MaterialPrice.TypeMAterial.Create
                };
                
                if (db.MaterialRepositories.InsertMaterial(model))
                {
                    db.Save();
                    RtlMessageBox.Show("عملیات با موفقیت انجام شد");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    RtlMessageBox.Show("کالا ثبت نگردید");

                }
            }
            
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
