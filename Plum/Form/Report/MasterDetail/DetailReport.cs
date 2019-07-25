using System.Collections.Generic;
using System.Linq;
using Plum.Form.Report.Reports;
using Plum.Model.Model.Food;
using Plum.Model.Model.MAterial;
using Plum.Services;

namespace Plum.Form.Report.MasterDetail
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for DetailReport.
    /// </summary>
    public partial class DetailReport : Telerik.Reporting.Report
    {
        public DetailReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //var model = new dataSource().getDataSurce(0);

            //    objectDataSource1.DataSource = model.FoodSurplusPrices;
            //    objectDataSource2.DataSource = model.FoodSurplusPrices;
            //    objectDataSource3.DataSource = model;
            }
        
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

    }

