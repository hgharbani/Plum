using Plum.Data;
using Plum.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Plum.Model.Model;

namespace Plum.Form.FoodPrice
{
    public partial class EditFoodsPric : System.Windows.Forms.Form
    {
        public EditFoodsPric()
        {
            InitializeComponent();
        }

        public int _foodId;
        public bool _IsUpdate;

        private void Index_Load(object sender, EventArgs e)
        {
            using (var db = new UnitOfWork())
            {
                comboBox1.DataSource = db.CompanyService.GetAll();
            }
          
        }

        /// <summary>
        /// محاسبه قیمت نهایی هزینه های یک پرس غذا
        /// </summary>
        private PlumResult Calculator(ICollection<FoodSurplusPrice> foodsurplus)
        {

            var persons = foodsurplus.Any(a => a.AdjustKind == 1)
                ? foodsurplus.First(a => a.AdjustKind == 1).Price
                : 0.0;

          var Chashnis = foodsurplus.Any(a => a.AdjustKind == 2)
                ? foodsurplus.First(a => a.AdjustKind == 2).Price
                : 0.0;
            var Cleans = foodsurplus.Any(a => a.AdjustKind == 3)
                ? foodsurplus.First(a => a.AdjustKind == 3).Price
                : 0.0;
            var Boxs = foodsurplus.Any(a => a.AdjustKind == 4)
                ? foodsurplus.First(a => a.AdjustKind == 4).Price
                : 0.0;
            var Parents = foodsurplus.Any(a => a.AdjustKind == 5)
                ? foodsurplus.First(a => a.AdjustKind == 5).Price
                : 0.0;
            var bimehs = 0.0;
            if (foodsurplus.Any(a => a.AdjustKind == 6))
            {
                bimehs = foodsurplus.First(a => a.AdjustKind == 6).Price;
               
            }
         var taxs = foodsurplus.Any(a => a.AdjustKind == 7)
                ? foodsurplus.First(a => a.AdjustKind == 7).Price
                : 0.0;
         var Surplus = foodsurplus.Any(a => a.AdjustKind == 8)
             ? foodsurplus.First(a => a.AdjustKind == 8).CostTitle
             : "";


            double sum = 0.0;
            double totalPeice = foodsurplus.FirstOrDefault()==null? foodsurplus.FirstOrDefault().Food.FoodMaterials.Sum(a => a.MaterialTotalPrice):0;
            double personNumeric = string.IsNullOrWhiteSpace(person.Text) ? persons : double.Parse(person.Text);
            double chashniNumeric = string.IsNullOrWhiteSpace(Chashni.Text) ? Chashnis : double.Parse(Chashni.Text);
            double cleanNumeric = string.IsNullOrWhiteSpace(Clean.Text) ? Cleans : double.Parse(Clean.Text);
            double boxNumeric = string.IsNullOrWhiteSpace(Box.Text) ? Boxs : double.Parse(Box.Text);
            double parentNumeric = string.IsNullOrWhiteSpace(Parent.Text) ? Parents : double.Parse(Parent.Text);
            double bimehNumeric = string.IsNullOrWhiteSpace(bimeh.Text) ? bimehs : double.Parse(bimeh.Text, CultureInfo.InvariantCulture);
            double taxNumeric = string.IsNullOrWhiteSpace(tax.Text) ? taxs : double.Parse(tax.Text, CultureInfo.InvariantCulture);

            sum = sum + (totalPeice + personNumeric + chashniNumeric + cleanNumeric + boxNumeric + parentNumeric);
            var SumBimeh = 0.0;//حاصل جمع مقادیر * مقدار بیمه
            if (bimehNumeric > 0)
            {
                 SumBimeh = sum * bimehNumeric;
             
            }
            //مجموعه هزینه ها * مقدار مالیات
            var maliyat = 0.0;
            if (taxNumeric > 0)
            {
                 maliyat = sum * taxNumeric;
            
            }

            var totalSurplusPrice = sum + maliyat + SumBimeh;
            //مجموعه هزینه ها + حاصلضرب مقدار درصد کسر شده به ازای بیمه+حاصلضرب درصد کصر شده به ازای مالیات

            List<FoodSurplusPrice> models = new List<FoodSurplusPrice>();
            models.Add(new FoodSurplusPrice()
            {
                CostTitle = lb1.Text,
                Price = personNumeric,
                AdjustKind = 1,
                FoodId = _foodId

            });
            models.Add(new FoodSurplusPrice()
            {
                CostTitle = lb2.Text,
                Price = chashniNumeric,
                AdjustKind = 2,
                FoodId = _foodId

            });
            models.Add(new FoodSurplusPrice()
            {
                CostTitle = lb3.Text,
                Price = cleanNumeric,
                AdjustKind = 3,
                FoodId = _foodId

            });
            models.Add(new FoodSurplusPrice()
            {
                CostTitle = lb4.Text,
                Price = boxNumeric,
                AdjustKind = 4,
                FoodId = _foodId

            });
            models.Add(new FoodSurplusPrice()
            {
                CostTitle = lb5.Text,
                Price = parentNumeric,
                AdjustKind = 5,
                FoodId = _foodId

            });
            models.Add(new FoodSurplusPrice()
            {
                CostTitle = lb6.Text,
                Price = bimehNumeric,
                AdjustKind = 6,
                FoodId = _foodId

            });
            models.Add(new FoodSurplusPrice()
            {
                CostTitle = lb7.Text,
                Price = taxNumeric,
                AdjustKind = 7,
                FoodId = _foodId

            });
            models.Add(new FoodSurplusPrice()
            {
                CostTitle = Surplus,
                Price = totalSurplusPrice,
                AdjustKind = 8,
                FoodId = _foodId

            });

            using (UnitOfWork db = new UnitOfWork())
            {
               return db.FoodSurplusPricService.UpdateFoodSurplusPrice(models, _foodId);
            }
        }
        private void FullPrice_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new UnitOfWork())
            {
                var foodssurplusPrice = db.FoodService.GetAllByCompanyId((int)comboBox1.SelectedValue);
                foreach (var foodDetailsModel in foodssurplusPrice)
                {
                    _foodId = foodDetailsModel.Id;
                    var model = foodDetailsModel.FoodSurplusPrices;
                    var result = Calculator(model);
                    if (!result.IsChange)
                    {
                        RtlMessageBox.Show("فیلد های مورد نظر برای غذای " + foodDetailsModel.FoodName +
                                           "بروزرسانی نشد"+"لطفا تا زمان مشاهده پیغام اتمام عملیات صبر نمایید");
                    }
                }
                RtlMessageBox.Show("عملیات به اتمام رسید");

            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool valid = double.TryParse(bimeh.Text, NumberStyles.Number, CultureInfo.InvariantCulture,
                out double Number);
            if (valid == false)
            {
                tax.Text = "0";
            }
        }

        private void tax_TextChanged(object sender, EventArgs e)
        {

            bool valid = double.TryParse(tax.Text, NumberStyles.Number, CultureInfo.InvariantCulture,
                out double Number);
            if (valid == false)
            {
                tax.Text = "0";
            }
        }

        private void FullPrice_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
