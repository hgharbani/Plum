using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PdfRpt.Core.Helper;
using PersianDate;
using Plum.Data;
using Plum.Form.Food;
using Plum.Model.Model.Food;
using Plum.Services;

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
            var materialIndex=new Index();
          var result=  materialIndex.ShowDialog();
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
          var result=  FoodIndex.ShowDialog();
            if (result == DialogResult.None)
            {
                Statistics();
            }
        }

        public void Statistics()
        {
            using (var db=new UnitOfWork())
            {
                var food = db.FoodService.GetAll(true);
                var foodDetail = food.Select(a => new FoodDetailsModel()
                {
                    FoodName = a.FoodName,
                    FinalPrice = a.FoodSurplusPrices.Any(b => b.AdjustKind == 8)
                        ? a.FoodSurplusPrices.First(b => b.AdjustKind == 8).Price
                        : 0,

                }).ToList();
                var material = db.MaterialRepositories.GetAll();
                
                comboBox1.DataSource = material.Where(a => a.Active).ToList();
                
                LbMaterial.Text=material.Count(a=>a.Active).ToString();
                SumMaterialPrice.Text=material.Where(a=>a.Active).Sum(a=>a.UnitPrice).ToString(CultureInfo.InvariantCulture);
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
                ExpenciveFoodPrice.Text  = foodDetail.Any()
                    ? foodDetail.OrderByDescending(a => a.FinalPrice).Select(a => a.FinalPrice).First().ToString(CultureInfo.InvariantCulture)
                    : "غذایی تعریف نشده است";
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
            report.ShowDialog();
        }

        /// <summary>
        /// پشتیبان گیری از اطلاعات
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackup_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFiledialog=new SaveFileDialog();
            saveFiledialog.Filter = "Database Db|*.db";
            saveFiledialog.Title = "Save Backup File";
            saveFiledialog.FileName = "data" + DateTime.Now.ToFa().Replace("/", "-");
            saveFiledialog.ShowDialog();
            if (saveFiledialog.FileName != "")
            {
                using (var source = new SQLiteConnection(@"Data Source=C:\PlumFood\DataBase\localDB.db"))
                using (var destionotion= new SQLiteConnection(@"Data Source="+saveFiledialog.FileName+";"))
                {
                    source.Open();
                    destionotion.Open();
                    source.BackupDatabase(destionotion, "main","main",-1,null,0);
                }
            }
        }

        /// <summary>
        /// فراخوانی اطلاعات
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog=new OpenFileDialog();
            openFileDialog.Filter= "Database File(*.db)|*.db";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
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
           var result= report.ShowDialog();
            if (result == DialogResult.OK)
            {
                DialogResult = DialogResult.None;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new UnitOfWork())
            {
                chart1.Series.Clear();
                var materialComboBox = (Material) comboBox1.SelectedItem;
                var firsYear = comboBox2.SelectedIndex+1;
                var selectDate = DateTime.Now.AddYears(-firsYear);
                var material = db.MaterialRepositories.GetAll(false);
               
                material = material.OrderBy(a=>a.InsertTime).Where(a => a.InsertTime >= selectDate && a.Material.MaterialName== materialComboBox.MaterialName).ToList();
                var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = materialComboBox.MaterialName,
                    Color = System.Drawing.Color.MediumSlateBlue,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                   
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
                    series1.Points.AddXY(material1.InsertTime , material1.UnitPrice);
                }
                chart1.Refresh();
                chart1.Invalidate();

            }
        }
    }
}