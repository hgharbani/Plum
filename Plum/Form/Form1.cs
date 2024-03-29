﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
//using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.SqlServer.Management.Smo;
using PdfRpt.Core.Helper;
using PersianDate;
using Plum.Data;
using Plum.Form.Food;
using Plum.Form.MaterialPrice;
using Plum.Model.Model.Food;
using Plum.Model.Model.MaterialPrice;
using Plum.Services;
using Index = Plum.Form.Material.Index;

namespace Plum
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int UserId;
        private void btnMaterial_Click(object sender, EventArgs e)
        {
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// فراخوانی صفحه مدیریت کالاها
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMaterial_Click_1(object sender, EventArgs e)
        {
            var materialIndex = new Index();
            var result = materialIndex.ShowDialog();
            if (result == DialogResult.None)
            {
                Statistics();
            }
        }

        /// <summary>
        /// فراخوانی صفحه مدیریت غذا
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFood_Click(object sender, EventArgs e)
        {

            var FoodIndex = new FoodIndex();
            var result = FoodIndex.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                Statistics();
            }
        }

        public void Statistics()
        {
            using (var db = new UnitOfWork())
            {
                comboBox4.DataSource = db.CompanyService.GetAll();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            WindowState = FormWindowState.Maximized;
            Statistics();
        }

        /// <summary>
        /// فراخوانی صفحه گزارش
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var report = new Form.Report.Index();
          var result=  report.ShowDialog();
          if (result == DialogResult.Cancel)
          {
              Statistics();
          }
        }

        /// <summary>
        /// پشتیبان گیری از اطلاعات
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackup_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFiledialog = new SaveFileDialog();
            saveFiledialog.Filter = @"Database Bak|*.bak";
            saveFiledialog.Title = "Save Backup File";
            saveFiledialog.FileName = "data" + DateTime.Now.ToFa().Replace("/", "-");
            saveFiledialog.ShowDialog();
            if (saveFiledialog.FileName != "")
            {

                string command = @"BACKUP DATABASE PlumDB TO DISK='" + saveFiledialog.FileName + "'";
                SqlCommand oCommand = null;
                SqlConnection oConnection = null;
                oConnection = new SqlConnection(@"Data Source=.\SQLExpress;AttachDbFilename=|DataDirectory|PlumDB.mdf;Database=PlumDB;Trusted_Connection=Yes;");
                if (oConnection.State != ConnectionState.Open)
                    oConnection.Open();
                oCommand = new SqlCommand(command, oConnection);
                oCommand.ExecuteNonQuery();

                //using (var source = new SQLiteConnection(@"Data Source=C:\PlumFood\DataBase\localDB.db"))
                //using (var destionotion = new SQLiteConnection(@"Data Source=" + saveFiledialog.FileName + ";"))
                //{
                //    source.Open();
                //    destionotion.Open();
                //    source.BackupDatabase(destionotion, "main", "main", -1, null, 0);
                //}
            }
        }

        /// <summary>
        /// فراخوانی اطلاعات
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Database File(*.bak)|*.bak";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                string command = @"RESTORE DATABASE PlumDB FROM DISK='" + openFileDialog.FileName + "'"+ " WITH NOUNLOAD, REPLACE, STATS = 10";

                SqlCommand oCommand = null;
                SqlCommand oCommand1 = null;
                SqlConnection oConnection = null;
                oConnection = new SqlConnection(@"Data Source=.\SQLExpress;AttachDbFilename=|DataDirectory|PlumDB.mdf;Database=PlumDB;Trusted_Connection=Yes;");
                if (oConnection.State != ConnectionState.Open)
                    oConnection.Open();
                
                oCommand = new SqlCommand(command, oConnection);

              var  command1 = "USE master ALTER DATABASE PlumDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; ";
              command1 += "\n" + "ALTER DATABASE PlumDB SET MULTI_USER; ";
                
                oCommand1 = new SqlCommand(command1, oConnection);
                oCommand1.ExecuteNonQuery();

                oCommand.ExecuteNonQuery();

                System.IO.File.Copy(openFileDialog.FileName, @"C:\PlumFood\DataBase\localDB.db", true);
                MessageBox.Show("بازگردانی انجام شد");
            }


        }

        /// <summary>
        /// فراخوانی صفحه تغییر رمز عبور
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var report = new Form.User.ChangePassword();
            report.userId = UserId;
            var result = report.ShowDialog();
            if (result == DialogResult.OK)
            {
                DialogResult = DialogResult.None;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new UnitOfWork())
            {
                this.chart1.Series.Remove(chart1.Series.Take(1).First());
                chart1.Series.Clear();
                chart1.Refresh();
                chart1.Invalidate();
                var materialComboBox = (MaterialPriceModel)comboBox1.SelectedItem;
                var firsYear = comboBox2.SelectedIndex + 1;
                var selectDate = DateTime.Now.AddYears(-firsYear);
                var material = db.MaterialRepositories.GetAll(false, (int)comboBox4.SelectedValue);

                material = material.OrderBy(a => a.InsertTime).Where(a => a.InsertTime >= selectDate && a.Material.MaterialName.Contains(materialComboBox.MateriaName)).ToList();
                var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = materialComboBox.MateriaName,
                    Color = System.Drawing.Color.MediumSlateBlue,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = false,

                };
                var chartype = comboBox3.SelectedIndex;
                switch (chartype)
                {
                    case 0:
                        {
                            series1.ChartType = SeriesChartType.Column;
                            break;
                        }
                    case 1:
                        {
                            series1.ChartType = SeriesChartType.Line;
                            break;
                        }

                }
                this.chart1.Series.Add(series1);
                foreach (var material1 in material)
                {
                    series1.Points.AddXY(material1.InsertTime, material1.UnitPrice);
                }
      
                chart1.Refresh();
                chart1.Invalidate();
              
                //series1.Points.DataBindXY(null);
            }
        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
            var report = new Form.Company.Index();
          var result=  report.ShowDialog();
          if (result == DialogResult.Cancel)
          {
              Statistics();
          }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedValue == null) return;
            using (var db = new UnitOfWork())
            {
                var food = db.FoodService.GetAll(true).Where(a => a.CompanyId == (int)comboBox4.SelectedValue);
                var foodDetail = food.Select(a => new FoodDetailsModel()
                {
                    FoodName = a.FoodName,
                    FinalPrice = a.FoodSurplusPrices.Any(b => b.AdjustKind == 8)
                        ? a.FoodSurplusPrices.First(b => b.AdjustKind == 8).Price
                        : 0,

                }).ToList();
                var material = db.MaterialRepositories.GetAll(true, (int)comboBox4.SelectedValue).Select(a => new MaterialPriceModel()
                {
                    MateriaPriceId = a.Id,
                    MateriaName = a.Material.MaterialName,
                    UnitPrice = a.UnitPrice
                }).ToList();

                comboBox1.DataSource = material.ToList();

                LbMaterial.Text = material.Count().ToString();
                SumMaterialPrice.Text = material.Sum(a => a.UnitPrice).ToString(CultureInfo.InvariantCulture);
                FoodTotal.Text = food.Count().ToString();
                SumFoodPrice.Text = foodDetail.Sum(a => a.FinalPrice).ToString(CultureInfo.InvariantCulture);

                CheapFood.Text = foodDetail.Any()
                    ? foodDetail.OrderBy(a => a.FinalPrice).Select(a => a.FoodName).First()
                    : "غذایی تعریف نشده است";

                CheapFoodPrice.Text = foodDetail.Any()
                    ? foodDetail.OrderBy(a => a.FinalPrice).Select(a => a.FinalPrice).First().ToString(CultureInfo.InvariantCulture)
                    : "غذایی تعریف نشده است";

                ExpensiveFood.Text = foodDetail.Any()
                    ? foodDetail.OrderByDescending(a => a.FinalPrice).Select(a => a.FoodName).First()
                    : "غذایی تعریف نشده است";
                ExpenciveFoodPrice.Text = foodDetail.Any()
                    ? foodDetail.OrderByDescending(a => a.FinalPrice).Select(a => a.FinalPrice).First().ToString(CultureInfo.InvariantCulture)
                    : "غذایی تعریف نشده است";
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var report = new AboutMe();
            report.ShowDialog();
        }
    }
}