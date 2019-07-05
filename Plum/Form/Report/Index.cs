using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Windows.Forms;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using PdfRpt.Core.Helper;
using Plum.Form.PdfReport;
using Plum.Form.Report.Reports;
using Plum.Model.Model.Food;
using Plum.Services;
using Telerik.ReportViewer.WinForms;

namespace Plum.Form.Report
{
    public partial class Index : System.Windows.Forms.Form
    {
        public int foodId;
        public Index()
        {
            InitializeComponent();
            _listFoodDetail=new BindingList<FoodDetailsModel>();

        }

      private readonly BindingList<FoodDetailsModel> _listFoodDetail;
        private void button1_Click(object sender, EventArgs e)
        {
            var food = (Data.Food) cmbFoodName.SelectedItem;
          
            if (int.Parse(numericTextBox1.Value) == 0 || int.Parse(numericTextBox1.Value) < 0)
            {
                RtlMessageBox.Show("تعداد نمیتواند صفر باشد");
                return;
            }

            var finalprice = food.FoodSurplusPrices.Where(a => a.AdjustKind == 8).Select(a => a.Price).First() *
                             int.Parse(numericTextBox1.Value);
            FoodDetailsModel items=new FoodDetailsModel()
            {
                FoodId = food.Id,
                FoodName = food.FoodName,
                MaterialPrice = food.FoodSurplusPrices.Where(a => a.AdjustKind == 8).Select(a => a.Price).FirstOrDefault(),
                Quantity = int.Parse(numericTextBox1.Value),
                FinalPrice = finalprice
            };
            
            for (int i = 0; i < dataGridView1.RowCount ; i++) //compare data
            {
                var Row = dataGridView1.Rows[i];
                var abc = Row.Cells[0].Value;
                if (abc.ToString() ==  items.FoodId.ToString())
                {
                    RtlMessageBox.Show("رکورد تکراری می باشد");
                    return;
                }
               
            }
            _listFoodDetail.Add(items);
            dataGridView1.AutoGenerateColumns = false;
        
            dataGridView1.DataSource = _listFoodDetail;
        }

        private void Index_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
               var company = db.CompanyService.GetAll();
                comboBox1.DataSource = company;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          var sumRow= dataGridView1.Rows.Cast<DataGridViewRow>()
              .Sum(t => Convert.ToInt32(t.Cells[4].Value));
          var percent = double.Parse(numericTextBox1.Value) / 100;
          var finalsumpercewnt = sumRow * percent;
          MessageBox.Show(finalsumpercewnt.ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedValue==null)return;
            using (UnitOfWork db = new UnitOfWork())
            {
                List<Data.Food> foods = db.FoodService.GetAllByCompanyId((int)comboBox1.SelectedValue);
                cmbFoodName.DataSource = foods;
            }
        }
    }
}
