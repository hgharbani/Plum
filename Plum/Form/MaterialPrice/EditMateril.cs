﻿using System;
using System.Windows.Forms;
using Plum.Services;

namespace Plum.Form.MaterialPrice
{
    public partial class EditMateril : System.Windows.Forms.Form
    {
        public EditMateril()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var material = (Data.Material)cmdMaterial.SelectedItem;
            var company = (Data.Company)cmdCompany.SelectedItem;
            if (material == null)
            {
                errorProvider1.SetError(cmdMaterial, "لطفا کالا را وارد نمایید");
                return;
            }
            if (company == null)
            {
                errorProvider1.SetError(cmdCompany, "لطفا شرکت را انتخاب نمایید");
                return;
            }
            if (string.IsNullOrWhiteSpace(UnitPrice.Value))
            {
                errorProvider2.SetError(UnitPrice, "لطفا قیمت را وارد نمایید");
                return;

            }

            using (UnitOfWork db = new UnitOfWork())
            {
                var model = new Data.MaterialPrice()
                {
                    Id = int.Parse(Id.Text),
                    MaterialId = material.Id,
                    CompanyId = company.CompanyId,
                    UnitPrice = double.Parse(UnitPrice.Value),
                    Active = true,
                    InsertTime = DateTime.Now,
                    ParentId = null,
                    MaterialTypeData = Data.MaterialPrice.TypeMAterial.Edit
                };

                if (db.MaterialRepositories.UpdateMaterial(model, checkBox1.Checked))
                {
                    db.Save();
                    RtlMessageBox.Show("عملیات با موفقیت انجام شد");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("کالا ثبت نگردید");

                }
            }
        }
    }
}
