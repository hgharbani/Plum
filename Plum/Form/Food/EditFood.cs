﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plum.Services;

namespace Plum.Form.Food
{
    public partial class EditFood : System.Windows.Forms.Form
    {
        public EditFood()
        {
            InitializeComponent();
        }

        public int foodId;
        public int CompanyId;
        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(FoodName.Text))
            {
                errorProvider1.SetError(FoodName, "لطفا نام را وارد نمایید");
                return;
            }
            
            using (UnitOfWork db = new UnitOfWork())
            {
                var model = new Data.Food()
                {
                    Id = foodId,
                    FoodName = FoodName.Text,
                    CompanyId = CompanyId,
                    Active = true,
                };

                if (db.FoodService.UpdateFood(model))
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

        private void EditFood_Load(object sender, EventArgs e)
        {

        }
    }
}
