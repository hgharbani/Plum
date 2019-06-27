using Plum.Data;
using Plum.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace Plum.Form.FoodPrice
{
    public partial class Index : System.Windows.Forms.Form
    {
        public Index()
        {
            InitializeComponent();
        }

        public int _foodId;
        public bool _IsUpdate;

        private void Index_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// محاسبه قیمت نهایی هزینه های یک پرس غذا
        /// </summary>
        public void Calculator()
        {
            double sum = 0.0;
            double totalPeice = double.Parse(TotalPrice.Text);
            double personNumeric = string.IsNullOrWhiteSpace(person.Text) ? 0 : double.Parse(person.Text);
            double chashniNumeric = string.IsNullOrWhiteSpace(Chashni.Text) ? 0 : double.Parse(Chashni.Text);
            double cleanNumeric = string.IsNullOrWhiteSpace(Clean.Text) ? 0 : double.Parse(Clean.Text);
            double boxNumeric = string.IsNullOrWhiteSpace(Box.Text) ? 0 : double.Parse(Box.Text);
            double parentNumeric = string.IsNullOrWhiteSpace(Parent.Text) ? 0 : double.Parse(Parent.Text);
            double bimehNumeric = string.IsNullOrWhiteSpace(bimeh.Text) ? 0 : double.Parse(bimeh.Text, CultureInfo.InvariantCulture);
            double taxNumeric = string.IsNullOrWhiteSpace(tax.Text) ? 0 : double.Parse(tax.Text, CultureInfo.InvariantCulture);

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

            sum = sum + maliyat + SumBimeh;
            //مجموعه هزینه ها + حاصلضرب مقدار درصد کسر شده به ازای بیمه+حاصلضرب درصد کصر شده به ازای مالیات
            FullPrice.Text = sum.ToString(CultureInfo.InvariantCulture);
        }
        private void FullPrice_Click(object sender, EventArgs e)
        {
            Calculator();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<FoodSurplusPrice> models = new List<FoodSurplusPrice>();
            double personNumeric = string.IsNullOrWhiteSpace(person.Text) ? 0 : double.Parse(person.Text);
            double chashniNumeric = string.IsNullOrWhiteSpace(Chashni.Text) ? 0 : double.Parse(Chashni.Text);
            double cleanNumeric = string.IsNullOrWhiteSpace(Clean.Text) ? 0 : double.Parse(Clean.Text);
            double boxNumeric = string.IsNullOrWhiteSpace(Box.Text) ? 0 : double.Parse(Box.Text);
            double parentNumeric = string.IsNullOrWhiteSpace(Parent.Text) ? 0 : double.Parse(Parent.Text);
            double bimehNumeric = string.IsNullOrWhiteSpace(bimeh.Text) ? 0 : double.Parse(bimeh.Text,CultureInfo.InvariantCulture);
            double taxNumeric = string.IsNullOrWhiteSpace(tax.Text) ? 0 : double.Parse(tax.Text, CultureInfo.InvariantCulture);
            double fullPriceNumeric = string.IsNullOrWhiteSpace(FullPrice.Text) ? 0 : double.Parse(FullPrice.Text, CultureInfo.InvariantCulture);
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
                CostTitle = lb8.Text,
                Price = fullPriceNumeric,
                AdjustKind = 8,
                FoodId = _foodId

            });
            using (UnitOfWork db = new UnitOfWork())
            {
                if (_IsUpdate == false)
                {
                    var result = db.FoodSurplusPricService.AddFoodSurplusPrice(models);
                    if (result.IsChange)
                    {
                        RtlMessageBox.Show(result.Message);
                        DialogResult = DialogResult.OK;

                    }
                    
                }
                else
                {
                    var result = db.FoodSurplusPricService.UpdateFoodSurplusPrice(models,_foodId);
                    if (result.IsChange)
                    {
                        RtlMessageBox.Show(result.Message);
                        DialogResult = DialogResult.OK;

                    }
                }
                

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
