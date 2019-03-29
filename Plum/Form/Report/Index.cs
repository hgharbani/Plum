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
            using (UnitOfWork db = new UnitOfWork())
            {
                List<Data.Food> foods = db.FoodService.GetAll(true);
                cmbFoodName.DataSource = foods;
            }

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            var food = (Data.Food) cmbFoodName.SelectedItem;
            var listFoodDetail=new BindingList< FoodDetailsModel>();
            if (int.Parse(numericTextBox1.Value) == 0 || int.Parse(numericTextBox1.Value) < 0)
            {
                RtlMessageBox.Show("تعداد نمیتواند صفر باشد");
                return;
            }
            FoodDetailsModel items=new FoodDetailsModel()
            {
                FoodId = food.Id,
                FoodName = food.FoodName,
                Quantity = int.Parse(numericTextBox1.Value)
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
            listFoodDetail.Add(items);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = listFoodDetail;
        }

        private void Index_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
            }
        }
    }
}
