using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using PersianDate;
using Plum.Form.Report.Reports;
using Plum.Model.Model.Food;
using Plum.Services;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;
using FontSize = DocumentFormat.OpenXml.Wordprocessing.FontSize;

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
        public void ShowFoodGrid(bool isAll=true)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var model=new List<FoodDetailsModel>();
                dataGridView1.AutoGenerateColumns = false;


                if (isAll == true)
                {
                    var foods = db.FoodService.GetFoodsbyFoodDetails("", 0, 0);
                    dataGridView1.DataSource = foods;
                 
                }
                else
                {
                    var food = (int) cmbFoodName.SelectedValue;
                    var company = (int) comboBox1.SelectedValue;
                    var foods = db.FoodService.GetFoodsbyFoodDetails("", company, food);
                    if (!string.IsNullOrWhiteSpace(PriceFrom.Text) )
                    {
                        var fromPrice = Convert.ToDouble(PriceFrom.Text);
                        foods = foods.Where(a => a.FinalPrice >= fromPrice).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(PriceTo.Text) )
                    {
                        var fromPrice = Convert.ToDouble(PriceTo.Text);
                        foods = foods.Where(a => a.FinalPrice >= fromPrice).ToList();

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
            MaterialsName.ForeColor=Color.Gray;
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
            var createFoods=new CreateFood().ShowDialog();
            if (createFoods == DialogResult.OK)
            {
                ShowFoodGrid();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var companyId = (Data.Company) comboBox1.SelectedItem;
                using (UnitOfWork db = new UnitOfWork())
                {
                    Data.Food model = db.FoodService.GetOne(id);
                    EditFood formEdit = new EditFood();
                    formEdit.foodId = id;
                    formEdit.CompanyId = companyId.CompanyId;
                    formEdit.FoodName.Text = model.FoodName;

                    if (formEdit.ShowDialog() == DialogResult.None)
                    {
                        ShowFoodGrid();
                    }
                }

            }
            else
            {
                RtlMessageBox.Show("آیتمی انتخاب نشده است");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                using (UnitOfWork db = new UnitOfWork())
                {

                    int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    string name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    if (RtlMessageBox.Show($"ایا از حذف {name} مطمئن هستید", "توجه", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Data.Food model = db.FoodService.GetOne(id);
                        bool result = db.FoodService.DeleteFood(id);
                        db.Save();

                        RtlMessageBox.Show("کالا با موفقیت حذف شد");
                    }

                }

                ShowFoodGrid();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
            
                FoodMaterial.Index frm = new FoodMaterial.Index();
                frm._foodIds= int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                frm.companyId = (int) comboBox1.SelectedValue;
                frm.label2.Text = " صفحه مدیریت مواد لازم غذا:" + "  "+ dataGridView1.CurrentRow.Cells[1].Value.ToString();
                var createMaterialFoods = frm.ShowDialog();
                if (createMaterialFoods == DialogResult.Cancel)
                {
                    ShowFoodGrid(false);
                }
            }
            else
            {
                RtlMessageBox.Show("لطفا ابتدا نام غذا را از جدول زیر انتخاب نمایید.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
        {
            var reportViewver = new ReportViewer();
            var food = (Data.Food) cmbFoodName.SelectedItem;
            reportViewver.foodId = food.Id;
            reportViewver.ShowDialog();


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
                MaterialsName.Font=new Font(MaterialsName.Font.FontFamily,10);


            }
        }

        private void MaterialsName_TextChanged(object sender, EventArgs e)
        {
            MaterialsName.ForeColor=Color.Black;
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
              
                cmbFoodName.DataSource = db.FoodService.GetAll().Where(a=>a.CompanyId==(int)comboBox1.SelectedValue).ToList();
            }
        }
    }
}
