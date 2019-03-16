﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersianDate;
using Plum.Services;

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
        public void ShowFoodGrid()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dataGridView1.AutoGenerateColumns = false;
                List<Data.Food> foods = db.FoodService.GetAll(true);

                TotalCount.Text = ((foods.Count())).ToString();
                if (foods.Count() <= 15)
                {
                    //button1.Enabled = false;
                }
                dataGridView1.DataSource = foods;
                cmbFoodName.DataSource = foods;
                
            }




        }

        private void FoodIndex_Load(object sender, EventArgs e)
        {
            ShowFoodGrid();
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
                using (UnitOfWork db = new UnitOfWork())
                {
                    Data.Food model = db.FoodService.GetOne(id);
                    EditFood formEdit = new EditFood();
                    formEdit.Id.Text = id.ToString();
                    formEdit.FoodName.Text = model.FoodName;

                    if (formEdit.ShowDialog() == DialogResult.OK)
                    {
                        ShowFoodGrid();
                    }
                }

            }
            else
            {
                MessageBox.Show("آیتمی انتخاب نشده است");
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
                    if (MessageBox.Show($"ایا از حذف {name} مطمئن هستید", "توجه", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Data.Food model = db.FoodService.GetOne(id);
                        bool result = db.FoodService.DeleteFood(id);
                        db.Save();

                        MessageBox.Show("کالا با موفقیت حذف شد");
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
                frm.label2.Text = " صفحه مدیریت مواد لازم غذا:" + "  "+ dataGridView1.CurrentRow.Cells[1].Value.ToString();
                var createMaterialFoods = frm.ShowDialog();
                if (createMaterialFoods == DialogResult.OK)
                {
                    ShowFoodGrid();
                }
            }
            else
            {
                MessageBox.Show("لطفا ابتدا نام غذا را از جدول زیر انتخاب نمایید.");
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
                    var createMaterialFoods = frm.ShowDialog();
                    if (createMaterialFoods == DialogResult.OK)
                    {
                        ShowFoodGrid();
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("لطفا ابتدا نام غذا را از جدول زیر انتخاب نمایید.");
            }

        }
    }
}
