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

public class dataSource
{
    public FoodDetailsModel getDataSurce(int foodId)
    {
        using (var db = new UnitOfWork())
        {
            //var foodId =(int) this.ReportParameters["FoodId"].Value;
            var food = db.FoodService.GetOne(0);

            var model = new FoodDetailsModel()
            {
                FoodId = food.Id,
                FoodName = food.FoodName,
                FoodMaterials = new List<FoodMaterialModel>(),
                FoodSurplusPrices = food.FoodSurplusPrices.ToList()
            };



            foreach (var matetrial in food.FoodMaterials)
            {
                var foodMaterialModel = new FoodMaterialModel()
                {
                    MaterialName = matetrial.Material.MaterialName,
                    Price = matetrial.UnitPrice,
                    Quantity = matetrial.Quantity,
                    TotalPrice = matetrial.MaterialTotalPrice,

                };
                model.FoodMaterials.Add(foodMaterialModel);
            }

            return model;
        }
    }
}
