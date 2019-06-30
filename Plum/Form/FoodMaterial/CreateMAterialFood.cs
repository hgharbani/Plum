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

namespace Plum.Form.FoodMaterial
{
    public partial class CreateMAterialFood : System.Windows.Forms.Form
    {
        public CreateMAterialFood()
        {
            InitializeComponent();
        }
        public int _foodIds;
        public int company;
        private void CreateMAterialFood_Load(object sender, EventArgs e)
        {
            Material();
        }


        public void Material()
        {
            using (var db = new UnitOfWork())
            {
                var material = db.MaterialRepositories.GetAll(true, company);

                comboBox1.DataSource = material;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db=new UnitOfWork())
            {
                if (string.IsNullOrWhiteSpace(Quantity.Value))
                {
                    errorProvider1.RightToLeft = true;
                    errorProvider1.SetError(Quantity, "لطفا مقدار لازم را وارد را وارد نمایید");
                }
                var material = (Data.MaterialPrice)comboBox1.SelectedItem;
                //مبلغ هرکیلو تقسیم بر 1000=مبلغ هر گرم
                var unitPrice = material.UnitPrice / 1000;
                var quantity = double.Parse(Quantity.Value);
                var totalPrice = unitPrice * quantity;
                
                var foodMaterial = new Data.FoodMaterial()
                {
                    MaterialId = material.Id,
                    UnitPrice = material.UnitPrice,
                    Quantity = double.Parse(Quantity.Value),
                    FoodId = _foodIds,
                    MaterialTotalPrice = totalPrice,
                    Active = true
                };

                if (db.FoodMaterialService.AddFoodMaterial(foodMaterial))
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var materialId =(Data.MaterialPrice) comboBox1.SelectedItem;
                if (materialId.Id>0)
                {
                    
                    UnitPrice.Text = materialId.UnitPrice.ToString();
                }
              
            }
        }

    }
}
