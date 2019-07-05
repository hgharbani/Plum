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
    public partial class EditMaterialFood : System.Windows.Forms.Form
    {
        public EditMaterialFood()
        {
            InitializeComponent();
        }

        public int _foodIds;
        public int _foodMaterilId;
        public int _materialId;
        public int _companyId;

        private void EditMaterialFood_Load(object sender, EventArgs e)
        {
            Material();
            SelectMaterial(_materialId);
        }

        public void Material()
        {
            using (var db = new UnitOfWork())
            {
                var material = db.MaterialRepositories.GetAll(true, _companyId);
                var model = material.Select(a => new MaterialPriceModel()
                {
                    MateriaPriceId = a.Id,
                    MateriaName = a.Material.MaterialName,
                    UnitPrice = a.UnitPrice
                }).ToList();
                comboBox1.DataSource = model;
            }

        }

        public void SelectMaterial(int materialId)
        {
            comboBox1.SelectedValue = materialId;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new UnitOfWork())
            {
                if (string.IsNullOrWhiteSpace(Quantity.Value) && Quantity.Text == "0")
                {
                    errorProvider1.RightToLeft = true;
                    errorProvider1.SetError(Quantity, "لطفا مقدار لازم را وارد را وارد نمایید");
                }

                var material = (MaterialPriceModel) comboBox1.SelectedItem;
                //مبلغ هرکیلو تقسیم بر 1000=مبلغ هر گرم
                var unitPrice = material.UnitPrice / 1000;
                var quantity = double.Parse(Quantity.Value);
                var totalPrice = unitPrice * quantity;

                var foodMaterial = new Data.FoodMaterial()
                {
                    Id = _foodMaterilId,
                    MaterialPriceId = material.MateriaPriceId,
                    UnitPrice = material.UnitPrice,
                    Quantity = double.Parse(Quantity.Value),
                    FoodId = _foodIds,
                    MaterialTotalPrice = totalPrice,
                    Active = true
                };

                if (db.FoodMaterialService.UpdateFoodMaterial(foodMaterial))
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            using (var db = new UnitOfWork())
            {
                var priceId = 0;
                if (comboBox1.SelectedValue != null)
                {
                    priceId = (int) comboBox1.SelectedValue;
                }
                else
                {
                    priceId = _materialId;
                }
                var price = db.MaterialRepositories.GetOne(priceId);
                if(price==null) return;
                UnitPrice.Text = price.UnitPrice.ToString(CultureInfo.CurrentCulture);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var db = new UnitOfWork())
            {
                var priceId = 0;
                if (comboBox1.SelectedValue != null)
                {
                    priceId = (int)comboBox1.SelectedValue;
                }
                else
                {
                    priceId = _materialId;
                }
                var price = db.MaterialRepositories.GetOne(priceId);
                if (price == null) return;
                UnitPrice.Text = price.UnitPrice.ToString(CultureInfo.CurrentCulture);
            }
        }
    }
}
