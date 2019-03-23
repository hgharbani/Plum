using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plum.Services;

namespace Plum.Form.User
{
    public partial class ChangePassword : System.Windows.Forms.Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        public int userId;
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToLower() == textBox2.Text.ToLower())
            {

               
                using (var db=new UnitOfWork())
                {
                    var user = new Data.User()
                    {
                        UserId = userId,
                        Password = textBox1.Text
                    };
                    var result = db.UserServices.UpdateUser(user);
                    MessageBox.Show(result.Message);
                    db.Save();
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
