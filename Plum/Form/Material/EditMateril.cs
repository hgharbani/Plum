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

namespace Plum.Form.Material
{
    public partial class EditMateril : System.Windows.Forms.Form
    {
        public EditMateril()
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

            if (string.IsNullOrWhiteSpace(UnitPrice.Value))
            {
                errorProvider2.SetError(UnitPrice, "لطفا قیمت را وارد نمایید");
                return;

            }

            using (UnitOfWork db = new UnitOfWork())
            {
                var model = new Data.Material()
                {
                    Id = int.Parse(Id.Text),
                    MaterialName = FoodName.Text,
                    UnitPrice = double.Parse(UnitPrice.Value),
                    Active = true,
                    InsertTime = DateTime.Now,
                    ParentId = null,
                    MaterialTypeData = Data.Material.TypeMAterial.Edit
                };

                if (db.MaterialRepositories.UpdateMaterial(model))
                {
                    //db.Save();
                    MessageBox.Show("عملیات با موفقیت انجام شد");
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

