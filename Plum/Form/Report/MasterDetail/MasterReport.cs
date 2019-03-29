using System.Collections.Generic;
using System.Linq;
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
    /// Summary description for MasterReport.
    /// </summary>
    public partial class MasterReport : Telerik.Reporting.Report
    {
        public MasterReport(List<FoodDetailsModel> list )
        {
            InitializeComponent();
           
                objectDataSource3.DataSource = list;
           




        }
            //
            // Required for telerik Reporting designer support
            //
            

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
