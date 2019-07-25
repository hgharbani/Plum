namespace Plum.Form.Material
{
    partial class CreateMaterial
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.deleteMAterial = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MaterialName = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("IRANSans", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(192, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 54);
            this.button1.TabIndex = 17;
            this.button1.Text = "ثبت";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.deleteMAterial);
            this.panel1.Location = new System.Drawing.Point(1, 181);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 51);
            this.panel1.TabIndex = 18;
            // 
            // deleteMAterial
            // 
            this.deleteMAterial.BackColor = System.Drawing.Color.Transparent;
            this.deleteMAterial.BackgroundImage = global::Plum.Properties.Resources.Trash___Empty;
            this.deleteMAterial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deleteMAterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteMAterial.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.deleteMAterial.Location = new System.Drawing.Point(15, 8);
            this.deleteMAterial.Name = "deleteMAterial";
            this.deleteMAterial.Size = new System.Drawing.Size(34, 34);
            this.deleteMAterial.TabIndex = 0;
            this.deleteMAterial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.deleteMAterial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.deleteMAterial.UseVisualStyleBackColor = false;
            this.deleteMAterial.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("IRANSans", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(474, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 24);
            this.label1.TabIndex = 20;
            this.label1.Text = "نام :";
            // 
            // MaterialName
            // 
            this.MaterialName.Font = new System.Drawing.Font("IRANSans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaterialName.Location = new System.Drawing.Point(118, 46);
            this.MaterialName.Name = "MaterialName";
            this.MaterialName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MaterialName.Size = new System.Drawing.Size(281, 35);
            this.MaterialName.TabIndex = 19;
            this.MaterialName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.RightToLeft = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("IRANSans", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(291, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 24);
            this.label2.TabIndex = 21;
            // 
            // CreateMaterial
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(576, 240);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MaterialName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("IRANSans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateMaterial";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ویرایش کالا";
            this.Load += new System.EventHandler(this.CreateMaterial_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox MaterialName;
        public System.Windows.Forms.Button deleteMAterial;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.Label label2;
    }
}