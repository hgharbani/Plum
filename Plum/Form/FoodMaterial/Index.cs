using Plum.Model.Model.MAterial;
using Plum.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Plum.Form.Material;

namespace Plum.Form.FoodMaterial
{
    public partial class Index : System.Windows.Forms.Form
    {

        public Index()
        {
            InitializeComponent();
        }


        public int _foodIds;

        private void Index_Load(object sender, EventArgs e)
        {
            ShowMaterialFoodGrid(_foodIds);
           


        }

        public void ShowMaterialFoodGrid(int foodId)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                _foodIds = foodId;
                List<FoodMaterialModel> model = new System.Collections.Generic.List<FoodMaterialModel>();
                ICollection<Data.FoodMaterial> foodMaterial = db.FoodMaterialService.GetOneByFoodId(foodId);

                //گرفتن لیست مواد لازم یک کالا
                foreach (Data.FoodMaterial item in foodMaterial)
                {
                    FoodMaterialModel foodmaterial = new FoodMaterialModel()
                    {
                        Id = item.Id,
                        MaterialName = item.Material.MaterialName,
                        Price = item.Material.UnitPrice,
                        Quantity = item.Quantity,
                        TotalPrice = item.MaterialTotalPrice
                    };
                    model.Add(foodmaterial);
                }

                dataGridView1.DataSource = model;
                textBox2.Text = foodMaterial.Sum(a => a.MaterialTotalPrice).ToString(CultureInfo.CurrentCulture);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            CreateMAterialFood createMaterialFoods = new CreateMAterialFood
            {
                _foodIds = _foodIds
            };
            DialogResult result = createMaterialFoods.ShowDialog();
            if (result == DialogResult.OK)
            {
                ShowMaterialFoodGrid(_foodIds);
            }
        }


        private void button6_Click(object sender, EventArgs e)
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
                        Data.FoodMaterial model = db.FoodMaterialService.GetOne(id);


                        var result = db.FoodMaterialService.DeleteFood(id);
                        if (result.IsChange)
                        {
                            db.Save();

                            RtlMessageBox.Show(result.Message);
                        }
                        else
                        {
                            RtlMessageBox.Show(result.Message);
                        }

                    }

                }

                ShowMaterialFoodGrid(_foodIds);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                using (UnitOfWork db = new UnitOfWork())
                {
                    Data.FoodMaterial model = db.FoodMaterialService.GetOne(id);
                    EditMaterialFood formEdit = new EditMaterialFood();
                    formEdit._foodMaterilId = id;
                    formEdit._foodIds = model.FoodId;
                    formEdit.Quantity.Text = model.Quantity.ToString();
                    formEdit._materialId=model.MaterialId ;
                    formEdit.UnitPrice.Text = model.Material.UnitPrice.ToString();
                    if (formEdit.ShowDialog() == DialogResult.OK)
                    {
                        ShowMaterialFoodGrid(_foodIds);
                    }
                }

            }
            else
            {
                RtlMessageBox.Show("آیتمی انتخاب نشده است");
            }
        }
    }
}

