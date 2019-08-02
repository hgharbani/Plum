using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PersianDate;
using Plum.Form.Report.Reports;
using Plum.Model.Model.Food;
using Plum.Services;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;
using FontSize = DocumentFormat.OpenXml.Wordprocessing.FontSize;
using Path = DocumentFormat.OpenXml.Drawing.Path;

namespace Plum.Form.Food
{
    public partial class FoodIndex : System.Windows.Forms.Form
    {
        public FoodIndex()
        {
            InitializeComponent();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowFoodGrid(bool isAll = true)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var model = new List<FoodDetailsModel>();
                dataGridView1.AutoGenerateColumns = false;


                if (isAll == true)
                {
                    var foods = db.FoodService.GetFoodsbyFoodDetails("", 0, 0);
                    dataGridView1.DataSource = foods;

                }
                else
                {
                    var food = (int)cmbFoodName.SelectedValue;
                    var company = (int)comboBox1.SelectedValue;
                    var foods = db.FoodService.GetFoodsbyFoodDetails(MaterialsName.Text, company, food);
                    if (!string.IsNullOrWhiteSpace(PriceFrom.Text))
                    {
                        var fromPrice = Convert.ToDouble(PriceFrom.Text);
                        foods = foods.Where(a => a.FinalPrice >= fromPrice).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(PriceTo.Text))
                    {
                        var fromPrice = Convert.ToDouble(PriceTo.Text);
                        foods = foods.Where(a => a.FinalPrice <= fromPrice).ToList();

                    }
                    dataGridView1.DataSource = foods;

                }



            }




        }

        private void FoodIndex_Load(object sender, EventArgs e)
        {

            GetCompany();
            MaterialsName.Text = @"برای تعریف چند کالا از علامت - استفاده نمایید";
            MaterialsName.Font = new Font(MaterialsName.Font.FontFamily, 10);
            MaterialsName.ForeColor = Color.Gray;
        }

        private void GetFood()
        {

        }

        private void GetCompany()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                comboBox1.DataSource = db.CompanyService.GetAll();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var createFoods = new CreateFood().ShowDialog();
            if (createFoods == DialogResult.OK)
            {
                ShowFoodGrid();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
        }

        
        private void button9_Click(object sender, EventArgs e)
        {
            


        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowFoodGrid(false);
        }

        private void MaterialsName_Enter(object sender, EventArgs e)
        {
            if (MaterialsName.Text == @"برای تعریف چند کالا از علامت - استفاده نمایید")
            {
                MaterialsName.Text = "";
            }
        }

        private void MaterialsName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaterialsName.Text))
            {
                MaterialsName.Text = @"برای تعریف چند کالا از علامت - استفاده نمایید";
                MaterialsName.ForeColor = Color.Gray;
                MaterialsName.Font = new Font(MaterialsName.Font.FontFamily, 10);


            }
        }

        private void MaterialsName_TextChanged(object sender, EventArgs e)
        {
            MaterialsName.ForeColor = Color.Black;
            MaterialsName.Font = new Font(MaterialsName.Font.FontFamily, 12);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ShowFoodGrid(true);
        }

        private void cmbFoodName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var model = new List<Data.Food>()
              {
                  new Data.Food()
                  {
                      Id = 0,
                      FoodName = "همه"
                  }
              };
                model.AddRange(db.FoodService.GetAll().Where(a => a.CompanyId == (int)comboBox1.SelectedValue).ToList());

                cmbFoodName.DataSource = model;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        { 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 5)
            {
                FoodMaterial.Index frm = new FoodMaterial.Index();
                frm._foodIds = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                frm.companyId = (int) comboBox1.SelectedValue;
                var createMaterialFoods = frm.ShowDialog();
                if (createMaterialFoods == DialogResult.Cancel)
                {
                    ShowFoodGrid(false);
                }
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 6)
            {
                if (dataGridView1.CurrentRow != null)
                {
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        FoodPrice.Index frm = new FoodPrice.Index();
                        frm._foodId = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        var model = db.FoodMaterialService.GetOneByFoodId(id);
                        var totalPrice = model.Sum(a => a.MaterialTotalPrice);
                        frm.TotalPrice.Text = totalPrice.ToString();
                        frm.label1.Text = " هزینه های مازاد یک پرس :" + "  " +
                                          dataGridView1.CurrentRow.Cells[1].Value.ToString();

                        //گرفتن لیست هزینه های مازاد یک پرس غذا 
                        var foodsurplus = db.FoodSurplusPricService.GetFoodSurplusPricesByFoodId(id);
                        if (foodsurplus.Any())
                        {
                            frm._IsUpdate = true; //فعال سازی مود اپدیت کردن

                        }
                        else
                        {
                            frm._IsUpdate = false; //فعال سازی مود ثبت کردن
                        }

                        frm.person.Text = foodsurplus.Any(a => a.AdjustKind == 1)
                            ? foodsurplus.First(a => a.AdjustKind == 1).Price.ToString()
                            : "0";

                        frm.Chashni.Text = foodsurplus.Any(a => a.AdjustKind == 2)
                            ? foodsurplus.First(a => a.AdjustKind == 2).Price.ToString()
                            : "0";
                        frm.Clean.Text = foodsurplus.Any(a => a.AdjustKind == 3)
                            ? foodsurplus.First(a => a.AdjustKind == 3).Price.ToString()
                            : "0";
                        frm.Box.Text = foodsurplus.Any(a => a.AdjustKind == 4)
                            ? foodsurplus.First(a => a.AdjustKind == 4).Price.ToString()
                            : "0";
                        frm.Parent.Text = foodsurplus.Any(a => a.AdjustKind == 5)
                            ? foodsurplus.First(a => a.AdjustKind == 5).Price.ToString(CultureInfo.InvariantCulture)
                            : "0";
                        if (foodsurplus.Any(a => a.AdjustKind == 6))
                        {
                            var x = foodsurplus.First(a => a.AdjustKind == 6).Price.ToString(CultureInfo.InvariantCulture);
                            frm.bimeh.Text = x.ToString();
                        }
                        else
                        {
                            frm.bimeh.Text = "0";
                        }

                        frm.tax.Text = foodsurplus.Any(a => a.AdjustKind == 7)
                            ? foodsurplus.First(a => a.AdjustKind == 7).Price.ToString(CultureInfo.InvariantCulture)
                            : "0";
                        frm.Calculator();

                        var createMaterialFoods = frm.ShowDialog();
                        if (createMaterialFoods == DialogResult.OK)
                        {
                            ShowFoodGrid(false);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("لطفا ابتدا نام غذا را از جدول زیر انتخاب نمایید.");
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowFoodGrid(false);
        }

        private void خروجیPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       

        private void خروجیPdfToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
           

            var reportViewver = new ReportViewer();
            var food = (Data.Food)cmbFoodName.SelectedItem;
            if (food.Id > 0)
            {
                reportViewver.foodId = food.Id;
                reportViewver.ShowDialog();
            }
            else
            {
                MessageBox.Show("لطفا نام کالا را انتخاب کنید");
            }

        }

        private void خروجیExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFiledialog = new SaveFileDialog();
            saveFiledialog.Filter = @"Excel Xls|*.xls";
            saveFiledialog.Title = "Save Excel File";
            saveFiledialog.FileName = "data" + DateTime.Now.ToFa().Replace("/", "-");
            saveFiledialog.ShowDialog();
            if (saveFiledialog.FileName != "")
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    var finalList = new List<FoodDetailsModel>();
                    var food = (int)cmbFoodName.SelectedValue;
                    var company = (int)comboBox1.SelectedValue;
                    var foods = db.FoodService.GetFoodsbyFoodDetails(MaterialsName.Text, company, food);
                    if (!string.IsNullOrWhiteSpace(PriceFrom.Text))
                    {
                        var fromPrice = Convert.ToDouble(PriceFrom.Text);
                        foods = foods.Where(a => a.FinalPrice >= fromPrice).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(PriceTo.Text))
                    {
                        var fromPrice = Convert.ToDouble(PriceTo.Text);
                        foods = foods.Where(a => a.FinalPrice <= fromPrice).ToList();

                    }

                    using (var p = new ExcelPackage())
                    {
                        var workbook = p.Workbook;
                        var ws = p.Workbook.Worksheets.Add("گزارش مصرف کلی ماهیانه");
                        ws.View.RightToLeft = true;


                        var record = 1;
                        var nextRecord = 1;
                        foreach (var row in foods)
                        {


                            ws.Cells[record, 1, record, 8].Merge = true;
                            ws.Cells[record, 1, record, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[record, 1, record, 8].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Bisque);
                            ws.Cells[record, 1, record, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 1, record, 8].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 1, record, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 1, record, 8].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 1].Value = "ریز اجزای تشکیل دهنده یک پرس :" + " " + row.FoodName;
                            record += 1;
                            ws.Cells[record, 1, record, 2].Merge = true;
                            ws.Cells[record, 1, record, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 1, record, 2].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 1, record, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 1, record, 2].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 1].Value = "نام کالا";

                            ws.Cells[record, 3, record, 4].Merge = true;
                            ws.Cells[record, 3, record, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 3, record, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 3, record, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 3, record, 4].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 3].Value = "(گرم)تعداد ";

                            ws.Cells[record, 5, record, 6].Merge = true;
                            ws.Cells[record, 5, record, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 5, record, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 5, record, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 5, record, 6].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 5].Value = "قیمت هر کیلو ";

                            ws.Cells[record, 7, record, 8].Merge = true;
                            ws.Cells[record, 7, record, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 7, record, 8].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 7, record, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 7, record, 8].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[record, 7].Value = "قیمت نهایی  ";

                            nextRecord = record;
                            //ws.Row(record).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            //ws.Row(record).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            //ws.Row(record).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gainsboro);
                            //ws.Row(record).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gainsboro);
                            foreach (var rowFoodMaterial in row.FoodMaterials.ToList())
                            {
                                nextRecord += 1;
                                ws.Cells[nextRecord, 1, nextRecord, 2].Merge = true;
                                ws.Cells[nextRecord, 1, nextRecord, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 1, nextRecord, 2].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 1, nextRecord, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 1, nextRecord, 2].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 1].Value = rowFoodMaterial.MaterialPrice.Material.MaterialName;

                                ws.Cells[nextRecord, 3, nextRecord, 4].Merge = true;
                                ws.Cells[nextRecord, 3, nextRecord, 4].Merge = true;
                                ws.Cells[nextRecord, 3, nextRecord, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 3, nextRecord, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 3, nextRecord, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 3, nextRecord, 4].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 3].Value = rowFoodMaterial.Quantity;

                                ws.Cells[nextRecord, 5, nextRecord, 6].Merge = true;
                                ws.Cells[nextRecord, 5, nextRecord, 6].Merge = true;
                                ws.Cells[nextRecord, 5, nextRecord, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 5, nextRecord, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 5, nextRecord, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 5, nextRecord, 6].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 5].Value = rowFoodMaterial.UnitPrice;

                                ws.Cells[nextRecord, 7, nextRecord, 8].Merge = true;
                                ws.Cells[nextRecord, 7, nextRecord, 8].Merge = true;
                                ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 7].Value = rowFoodMaterial.MaterialTotalPrice;
                                //ws.Row(nextRecord).Style.Fill.PatternType = ExcelFillStyle.Solid;
                                //ws.Row(nextRecord).Style.Fill.PatternType = ExcelFillStyle.Solid;
                                //ws.Row(nextRecord).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gainsboro);
                                //ws.Row(nextRecord).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gainsboro);
                            }

                            nextRecord += 1;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Merge = true;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Merge = true;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 1].Value = "جمع کل";
                            ws.Cells[nextRecord, 7, nextRecord, 8].Merge = true;
                            ws.Cells[nextRecord, 7, nextRecord, 8].Merge = true;
                            ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 7].Value = row.FinalPrice;
                            ws.Row(nextRecord).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Row(nextRecord).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Row(nextRecord).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gainsboro);
                            ws.Row(nextRecord).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gainsboro);
                            record = nextRecord + 1;
                            nextRecord += 1;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Merge = true;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Merge = true;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 1].Value = "هزینه های مازاد";
                            ws.Cells[nextRecord, 7, nextRecord, 8].Merge = true;
                            ws.Cells[nextRecord, 7, nextRecord, 8].Merge = true;
                            ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            ws.Cells[nextRecord, 7].Value = "مبلغ";
                            //ws.Row(nextRecord).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            //ws.Row(nextRecord).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            //ws.Row(nextRecord).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gainsboro);
                            //ws.Row(nextRecord).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gainsboro);
                            foreach (var rowFoodSurplusPrice in row.FoodSurplusPrices)
                            {

                                ws.Cells[nextRecord, 1, nextRecord, 6].Merge = true;
                                ws.Cells[nextRecord, 1, nextRecord, 6].Merge = true;
                                ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 1, nextRecord, 6].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 1].Value = rowFoodSurplusPrice.CostTitle;
                                ws.Cells[nextRecord, 7, nextRecord, 8].Merge = true;
                                ws.Cells[nextRecord, 7, nextRecord, 8].Merge = true;
                                ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 7, nextRecord, 8].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                ws.Cells[nextRecord, 7].Value = rowFoodSurplusPrice.Price;
                                nextRecord += 1;
                            }

                            record = nextRecord + 2;
                        }

                        p.SaveAs(new FileInfo(System.IO.Path.Combine(saveFiledialog.FileName)));
                        MessageBox.Show("ذخیره شد");
                    }

                }
            }
        }
    }
}
