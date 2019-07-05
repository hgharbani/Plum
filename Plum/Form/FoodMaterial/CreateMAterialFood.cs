using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plum.Model.Model.MaterialPrice;
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
                var model = material.Select(a => new MaterialPriceModel()
                {
                    MateriaPriceId = a.Id,
                    MateriaName = a.Material.MaterialName,
                    UnitPrice = a.UnitPrice
                }).ToList();
                comboBox1.DataSource = model;
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
                var material = (MaterialPriceModel)comboBox1.SelectedItem;
                //مبلغ هرکیلو تقسیم بر 1000=مبلغ هر گرم
                var unitPrice = material.UnitPrice / 1000;
                var quantity = double.Parse(Quantity.Value);
                var totalPrice = unitPrice * quantity;
                
                var foodMaterial = new Data.FoodMaterial()
                {
                    MaterialPriceId = material.MateriaPriceId,
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
            using (var db = new UnitOfWork())
            {
                var price = db.MaterialRepositories.GetOne((int)comboBox1.SelectedValue);
                UnitPrice.Text = price.UnitPrice.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
