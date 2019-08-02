using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plum.Services;

namespace Plum.Form.Material
{
    public partial class CreateMaterial : System.Windows.Forms.Form
    {
        public int MaterialId;

        public CreateMaterial()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {

                int id = MaterialId;
                string name = MaterialName.Text;
                
            }
        }

        private void CreateMaterial_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(MaterialName.Text))
            {
                errorProvider1.SetError(MaterialName, "لطفا نام را وارد نمایید");
                return;
            }

            using (UnitOfWork db = new UnitOfWork())
            {

                if (MaterialId > 0)
                {
                    var model = new Data.Material()
                    {
                        Id = MaterialId,
                        MaterialName = MaterialName.Text,

                    };
                    var result = db.MaterialService.EditMaterial(model);
                    if (result.IsChange)
                    {
                        db.Save();
                        RtlMessageBox.Show(result.Message);
                        DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        RtlMessageBox.Show(result.Message);

                    }
                }
                else
                {
                    var model = new Data.Material()
                    {
                        MaterialName = MaterialName.Text,

                    };
                    var result = db.MaterialService.AddMaterial(model);
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
        }
    }
}
