using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PersianDate;
using Plum.Form.Food;
using Plum.Model.Model.MaterialPrice;
using Plum.Services;

namespace Plum.Form
{
    public partial class Index : System.Windows.Forms.Form
    {

        public Index()
        {
            InitializeComponent();
        }
        public int UserId;

        private void ribbonBar2_ItemClick(object sender, EventArgs e)
        {

        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {

        }

        private void ribbonPanel1_Click(object sender, EventArgs e)
        {

        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
           
        }

      
        private void Index_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "تعاریف";
            tabPage2.Text = "مدیریت";
            tabPage3.Text = "امکانات";
           
            tabControl1.BackColor = Color.GhostWhite;
         
            //AllDataModel();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            var report = new Form.Company.Index();
            var result = report.ShowDialog();
           
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            var FoodIndex = new FoodIndex();
            var result = FoodIndex.ShowDialog();
           
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            var createFoods = new Food.Index().ShowDialog();
           
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            var materialIndex = new Material.Index();
            var result = materialIndex.ShowDialog();
           
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            var materialIndex = new MaterialPrice.Index();
            var result = materialIndex.ShowDialog();
           
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            FoodPrice.EditFoodsPric frm = new FoodPrice.EditFoodsPric();
            var createMaterialFoods = frm.ShowDialog();
          
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            var report = new Form.Report.Index();
            var result = report.ShowDialog();
           
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFiledialog = new SaveFileDialog();
            saveFiledialog.Filter = @"Database Bak|*.bak";
            saveFiledialog.Title = "Save Backup File";
            saveFiledialog.FileName = "data" + DateTime.Now.ToFa().Replace("/", "-");
            saveFiledialog.ShowDialog();
            if (saveFiledialog.FileName != "")
            {

                string command = @"BACKUP DATABASE PlumDB TO DISK='" + saveFiledialog.FileName + "'";
                SqlCommand oCommand = null;
                SqlConnection oConnection = null;
                oConnection = new SqlConnection(@"Data Source=.\SQLExpress;AttachDbFilename=|DataDirectory|PlumDB.mdf;Database=PlumDB;Trusted_Connection=Yes;");
                if (oConnection.State != ConnectionState.Open)
                    oConnection.Open();
                oCommand = new SqlCommand(command, oConnection);
                oCommand.ExecuteNonQuery();

                //using (var source = new SQLiteConnection(@"Data Source=C:\PlumFood\DataBase\localDB.db"))
                //using (var destionotion = new SQLiteConnection(@"Data Source=" + saveFiledialog.FileName + ";"))
                //{
                //    source.Open();
                //    destionotion.Open();
                //    source.BackupDatabase(destionotion, "main", "main", -1, null, 0);
                //}
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Database File(*.bak)|*.bak";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                string command = @"RESTORE DATABASE PlumDB FROM DISK='" + openFileDialog.FileName + "'" + " WITH NOUNLOAD, REPLACE, STATS = 10";

                SqlCommand oCommand = null;
                SqlCommand oCommand1 = null;
                SqlConnection oConnection = null;
                oConnection = new SqlConnection(@"Data Source=.\SQLExpress;AttachDbFilename=|DataDirectory|PlumDB.mdf;Database=PlumDB;Trusted_Connection=Yes;");
                if (oConnection.State != ConnectionState.Open)
                    oConnection.Open();

                oCommand = new SqlCommand(command, oConnection);

                var command1 = "USE master ALTER DATABASE PlumDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; ";
                command1 += "\n" + "ALTER DATABASE PlumDB SET MULTI_USER; ";

                oCommand1 = new SqlCommand(command1, oConnection);
                oCommand1.ExecuteNonQuery();

                oCommand.ExecuteNonQuery();

                System.IO.File.Copy(openFileDialog.FileName, @"C:\PlumFood\DataBase\localDB.db", true);
                MessageBox.Show("بازگردانی انجام شد");
            }

        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            var report = new Form.User.ChangePassword();
            report.userId = UserId;
            var result = report.ShowDialog();
            if (result == DialogResult.OK)
            {
                DialogResult = DialogResult.None;
            }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            var report = new AboutMe();
            report.ShowDialog();
        }
    }
}
