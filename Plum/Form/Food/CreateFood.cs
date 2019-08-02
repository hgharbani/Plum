using System;
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
    public partial class CreateFood : System.Windows.Forms.Form
    {
        public CreateFood()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FoodName.Text))
            {
                errorProvider1.SetError(FoodName, "لطفا نام را وارد نمایید");
                return;
            }

            var company = (Data.Company)comboBox1.SelectedItem;
            using (UnitOfWork db = new UnitOfWork())
            {
                var model = new Data.Food()
                {
                    FoodName = FoodName.Text,
                    CompanyId = company.CompanyId,
                    Active = true,
                };

                if (db.FoodService.InsertFood(model))
                {
                    db.Save();
                    RtlMessageBox.Show("عملیات با موفقیت انجام شد");
               
                }
                else
                {
                    RtlMessageBox.Show("کالا ثبت نگردید");

                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FoodName_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateFood_Load(object sender, EventArgs e)
        {
            using (var db = new UnitOfWork())
            {
             
                var company = db.CompanyService.GetAll();
                comboBox1.DataSource = company;
            }
        }
    }
}
