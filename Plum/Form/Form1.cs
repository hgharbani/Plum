using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfRpt.Core.Helper;
using PersianDate;
using Plum.Form.Food;

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

        private void btnMaterial_Click_1(object sender, EventArgs e)
        {
            var materialIndex=new Index();
            materialIndex.ShowDialog();
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            var FoodIndex = new FoodIndex();
            FoodIndex.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            WindowState = FormWindowState.Maximized;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var report = new Form.Report.Index();
            report.ShowDialog();
        }

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
    }
}