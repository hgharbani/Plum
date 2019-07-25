using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Plum.Data;
using Plum.Model.Model.Food;

namespace Plum.Form.Report.Reports
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RptFoodDetails.
    /// </summary>
    public partial class RptFoodDetails : Telerik.Reporting.Report
    {
        public static string MyFormat(double num)
        {
            string format = "{0:N2}";
            if (num < 0)
            {
                format = "({0:N2})";
            }
            return string.Format(format, Math.Abs(num));
        }

        public RptFoodDetails(FoodDetailsModel foodDetailsModels)
        {
            //Create new CultureInfo
            var cultureInfo = new System.Globalization.CultureInfo("fa-IR");

            // Set the language for static text (i.e. column headings, titles)
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;

            // Set the language for dynamic text (i.e. date, time, money)
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;

            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            objectDataSource2.DataSource = foodDetailsModels.FoodMaterialsModel;
            objectDataSource1.DataSource = foodDetailsModels.FoodSurplusPrices;
            table2.DataSource = foodDetailsModels.FoodSurplusPrices;
            textBox28.Value = foodDetailsModels.FoodName;
            textBox9.Value = foodDetailsModels.FoodMaterials.Sum(a => a.MaterialTotalPrice).ToString("c0", cultureInfo);
        
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}