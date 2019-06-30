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

namespace Plum.Form.Company
{
    public partial class CreateCompany : System.Windows.Forms.Form
    {
        public Data.Company ModelCompany;
        public CreateCompany()
        {
            InitializeComponent();
            ModelCompany=new Data.Company();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                errorProvider1.SetError(txtCompanyName, "لطفا نام را وارد نمایید");
                return;
            }

            using (UnitOfWork db = new UnitOfWork())
            {
             
                if (ModelCompany == null)
                {
                    var model = new Data.Company()
                    {
                        CompanyName = txtCompanyName.Text,

                    };
                    var result = db.CompanyService.AddCompany(model);
                    if (result.IsChange)
                    {
                        db.Save();
                        RtlMessageBox.Show(result.Message);
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        RtlMessageBox.Show(result.Message);

                    }

                }
                else
                {
                    var model = new Data.Company()
                    {
                        CompanyId = ModelCompany.CompanyId,
                        CompanyName = txtCompanyName.Text,

                    };
                    var result = db.CompanyService.EditCompany(model);
                    if (result.IsChange)
                    {
                        db.Save();
                        RtlMessageBox.Show(result.Message);
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        RtlMessageBox.Show(result.Message);

                    }
                }
               
            }
        }

        private void CreateCompany_Load(object sender, EventArgs e)
        {

        }
    }
}
