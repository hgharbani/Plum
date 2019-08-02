using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plum.Model.Model.Food;
using Plum.Services;

namespace Plum.Form.Food
{
    public partial class Index : System.Windows.Forms.Form
    {
        public Index()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var companyId = (Data.Company)comboBox1.SelectedItem;
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

        private void btnDelete_Click(object sender, EventArgs e)
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
                        bool result = db.FoodService.DeleteFood(id);
                        if (result)
                        {
                            db.Save();

                            RtlMessageBox.Show("کالا با موفقیت حذف شد");
                        }
                        RtlMessageBox.Show("کالا  حذف نشد");

                    }

                }

                ShowFoodGrid();
            }
        }

        private void GetCompany()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                comboBox1.DataSource = db.CompanyService.GetAll();
            }
        }
        private void Getfood()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var foods= db.FoodService.GetAll();
                cmbFoodName.ValueMember = "Id";
                cmbFoodName.DisplayMember = "FoodName";
                cmbFoodName.DataSource = foods;
            }
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
                var food = (int)cmbFoodName.SelectedValue;
                var company = (int)comboBox1.SelectedValue;
                var foods = db.FoodService.GetFoodsbyFoodDetails("", company, food);
                dataGridView1.DataSource = foods;

            }

        }

        private void Index_Load(object sender, EventArgs e)
        {
            GetCompany();
            Getfood();
            ShowFoodGrid();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var createFoods = new CreateFood().ShowDialog();
            if (createFoods == DialogResult.Cancel)
            {
                Getfood();
                ShowFoodGrid();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowFoodGrid();
        }
    }
}
