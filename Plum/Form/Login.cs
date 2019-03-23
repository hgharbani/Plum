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
using Plum.Data;
using Plum.Services;

namespace Plum.Form
{
    public partial class Login : System.Windows.Forms.Form
    {
        public Login()
        {
            InitializeComponent();
        }
        string password = "";
        byte[] hash;
        private void button1_Click(object sender, EventArgs e)
        {
            var username = textBox1.Text.Trim().ToLower();
            password = textBox2.Text.Trim();
           
            var data = Encoding.ASCII.GetBytes(password);
            
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(data);
            var hashedPassword = ASCIIEncoding.UTF8.GetString(md5data);
            using (var db=new UnitOfWork())
            {
                var user=new Data.User()
                {
                    UserName = username,
                    Password = hashedPassword
                };
             var searchUser=   db.UserServices.GetUser(user);
                if (searchUser!=null)
                {
                    var form = new Form1();
                    form.UserId = searchUser.UserId;
                    this.Hide();
                    var x = form.ShowDialog();
                    if (x == DialogResult.Cancel)
                    {
                        form.UserId = 0;
                        this.Show();
                    }


                }
                else
                {
                    MessageBox.Show("اطلاعات وارد شده صحیح نمی باشد");
                }
            }
            
        }
    }
}
