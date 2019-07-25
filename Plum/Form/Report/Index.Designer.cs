namespace Plum.Form.Report
{
    partial class Index
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.foodIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.foodNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.foodDetailsModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.count = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.foodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbFoodName = new System.Windows.Forms.ComboBox();
            this.foodBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.numericTextBox1 = new NumericTextBox.NumericTextBox();
            this.numericTextBox2 = new NumericTextBox.NumericTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.foodMaterialsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.materialPriceModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.foodDetailsModelBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.companyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.foodDetailsModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.foodBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.foodBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.foodMaterialsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialPriceModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.foodDetailsModelBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkOrchid;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("IRANSans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(12, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "اضافه کردن";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.foodIdDataGridViewTextBoxColumn,
            this.foodNameDataGridViewTextBoxColumn,
            this.MaterialPrice,
            this.quantityDataGridViewTextBoxColumn,
            this.FinalPrice});
            this.dataGridView1.DataSource = this.foodDetailsModelBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 77);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(713, 279);
            this.dataGridView1.TabIndex = 2;
            // 
            // foodIdDataGridViewTextBoxColumn
            // 
            this.foodIdDataGridViewTextBoxColumn.DataPropertyName = "FoodId";
            this.foodIdDataGridViewTextBoxColumn.HeaderText = "FoodId";
            this.foodIdDataGridViewTextBoxColumn.Name = "foodIdDataGridViewTextBoxColumn";
            this.foodIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.foodIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // foodNameDataGridViewTextBoxColumn
            // 
            this.foodNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.foodNameDataGridViewTextBoxColumn.DataPropertyName = "FoodName";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("IRANSans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.BlueViolet;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.foodNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.foodNameDataGridViewTextBoxColumn.HeaderText = "نام غذا";
            this.foodNameDataGridViewTextBoxColumn.Name = "foodNameDataGridViewTextBoxColumn";
            this.foodNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // MaterialPrice
            // 
            this.MaterialPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaterialPrice.DataPropertyName = "MaterialPrice";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("IRANSans Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.BlueViolet;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.MaterialPrice.DefaultCellStyle = dataGridViewCellStyle6;
            this.MaterialPrice.HeaderText = "قیمت هر پرس";
            this.MaterialPrice.Name = "MaterialPrice";
            this.MaterialPrice.ReadOnly = true;
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            this.quantityDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("IRANSans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = "0";
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.BlueViolet;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            this.quantityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.quantityDataGridViewTextBoxColumn.HeaderText = " تعداد هر پرس";
            this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            this.quantityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FinalPrice
            // 
            this.FinalPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FinalPrice.DataPropertyName = "FinalPrice";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("IRANSans Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.BlueViolet;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            this.FinalPrice.DefaultCellStyle = dataGridViewCellStyle8;
            this.FinalPrice.HeaderText = "قیمت نهایی";
            this.FinalPrice.Name = "FinalPrice";
            this.FinalPrice.ReadOnly = true;
            // 
            // foodDetailsModelBindingSource
            // 
            this.foodDetailsModelBindingSource.DataSource = typeof(Plum.Model.Model.Food.FoodDetailsModel);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(499, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "غذا";
            // 
            // count
            // 
            this.count.AutoSize = true;
            this.count.Location = new System.Drawing.Point(244, 26);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(47, 28);
            this.count.TabIndex = 4;
            this.count.Text = "تعداد";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(628, 388);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 28);
            this.label2.TabIndex = 6;
            this.label2.Text = "درصد مالیات";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FloralWhite;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("IRANSans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.BlueViolet;
            this.button2.Location = new System.Drawing.Point(182, 371);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(149, 59);
            this.button2.TabIndex = 8;
            this.button2.Text = "اکسل";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // foodBindingSource
            // 
            this.foodBindingSource.DataSource = typeof(Plum.Data.Food);
            // 
            // cmbFoodName
            // 
            this.cmbFoodName.DataSource = this.foodBindingSource;
            this.cmbFoodName.DisplayMember = "FoodName";
            this.cmbFoodName.Font = new System.Drawing.Font("IRANSans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmbFoodName.FormattingEnabled = true;
            this.cmbFoodName.Location = new System.Drawing.Point(296, 20);
            this.cmbFoodName.Name = "cmbFoodName";
            this.cmbFoodName.Size = new System.Drawing.Size(193, 42);
            this.cmbFoodName.TabIndex = 9;
            this.cmbFoodName.ValueMember = "Id";
            // 
            // foodBindingSource1
            // 
            this.foodBindingSource1.DataSource = typeof(Plum.Data.Food);
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.Font = new System.Drawing.Font("IRANSans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericTextBox1.FormatType = NumericTextBox.FormatType.Long;
            this.numericTextBox1.Location = new System.Drawing.Point(138, 20);
            this.numericTextBox1.MaxLength = 25;
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.Size = new System.Drawing.Size(100, 41);
            this.numericTextBox1.TabIndex = 10;
            // 
            // numericTextBox2
            // 
            this.numericTextBox2.FormatType = NumericTextBox.FormatType.Long;
            this.numericTextBox2.Location = new System.Drawing.Point(451, 385);
            this.numericTextBox2.MaxLength = 25;
            this.numericTextBox2.Name = "numericTextBox2";
            this.numericTextBox2.Size = new System.Drawing.Size(171, 35);
            this.numericTextBox2.TabIndex = 11;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Crimson;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("IRANSans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button3.Location = new System.Drawing.Point(12, 371);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(147, 59);
            this.button3.TabIndex = 12;
            this.button3.Text = "حذف کردن";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // foodMaterialsBindingSource
            // 
            this.foodMaterialsBindingSource.DataMember = "FoodMaterials";
            this.foodMaterialsBindingSource.DataSource = this.foodDetailsModelBindingSource;
            // 
            // materialPriceModelBindingSource
            // 
            this.materialPriceModelBindingSource.DataSource = typeof(Plum.Model.Model.MaterialPrice.MaterialPriceModel);
            // 
            // foodDetailsModelBindingSource1
            // 
            this.foodDetailsModelBindingSource1.DataSource = typeof(Plum.Model.Model.Food.FoodDetailsModel);
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.companyBindingSource;
            this.comboBox1.DisplayMember = "CompanyName";
            this.comboBox1.Font = new System.Drawing.Font("IRANSans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(538, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(131, 42);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.ValueMember = "CompanyId";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // companyBindingSource
            // 
            this.companyBindingSource.DataSource = typeof(Plum.Data.Company);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(677, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 28);
            this.label3.TabIndex = 13;
            this.label3.Text = "شرکت";
            // 
            // Index
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(737, 442);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.numericTextBox2);
            this.Controls.Add(this.numericTextBox1);
            this.Controls.Add(this.cmbFoodName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.count);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("IRANSans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Index";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "گزارشات";
            this.Load += new System.EventHandler(this.Index_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.foodDetailsModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.foodBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.foodBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.foodMaterialsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialPriceModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.foodDetailsModelBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label count;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource foodBindingSource;
        private System.Windows.Forms.ComboBox cmbFoodName;
        private System.Windows.Forms.BindingSource foodBindingSource1;
        private NumericTextBox.NumericTextBox numericTextBox1;
        private NumericTextBox.NumericTextBox numericTextBox2;
        private System.Windows.Forms.BindingSource foodDetailsModelBindingSource;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.BindingSource foodDetailsModelBindingSource1;
        private System.Windows.Forms.BindingSource foodMaterialsBindingSource;
        private System.Windows.Forms.BindingSource materialPriceModelBindingSource;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource companyBindingSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn foodIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn foodNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinalPrice;
    }
}