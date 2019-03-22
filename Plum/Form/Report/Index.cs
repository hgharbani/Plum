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
using Plum.Form.PdfReport;
using Plum.Form.Report.Reports;
using Plum.Model.Model.Food;
using Plum.Services;
using Telerik.ReportViewer.WinForms;

namespace Plum.Form.Report
{
    public partial class Index : System.Windows.Forms.Form
    {
        private readonly FoodDetailMaterialReport _foodDetailMaterialReport;
        public int foodId;
        public Index()
        {
            InitializeComponent();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Telerik.Reporting.Report report;
            report = _foodDetailMaterialReport.GetFoodDetail(foodId);
            report.DocumentName = "نمایش هزینه مازاد یک پرس غذا";
           
           

        }
    }
}
