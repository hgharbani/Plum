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

namespace Plum.Form.Report.Reports
{
    public partial class ReportViewer : System.Windows.Forms.Form
    {
        private readonly FoodDetailMaterialReport _foodDetailMaterialReport;
        public int foodId;
        public ReportViewer()
        {
            InitializeComponent();
            _foodDetailMaterialReport =
                (FoodDetailMaterialReport)DependencyResolver.Current.GetService(typeof(FoodDetailMaterialReport));
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            Telerik.Reporting.Report report;
            report = _foodDetailMaterialReport.GetFoodDetail(foodId);
            report.DocumentName = "نمایش هزینه مازاد یک پرس غذا";
            reportViewer1.ReportSource = report;

            reportViewer1.RefreshReport();
        }
    }
}
