namespace MidFinalProject
{
    partial class ManageStudentsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageStudentsForm));
            this.mainPageMainPnl = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addStudentBtn = new System.Windows.Forms.Button();
            this.updateStudentBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.mainPageMainPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPageMainPnl
            // 
            this.mainPageMainPnl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mainPageMainPnl.ColumnCount = 3;
            this.mainPageMainPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.00637F));
            this.mainPageMainPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.99363F));
            this.mainPageMainPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 151F));
            this.mainPageMainPnl.Controls.Add(this.pictureBox1, 0, 0);
            this.mainPageMainPnl.Controls.Add(this.pictureBox2, 2, 0);
            this.mainPageMainPnl.Controls.Add(this.label1, 1, 0);
            this.mainPageMainPnl.Controls.Add(this.addStudentBtn, 1, 1);
            this.mainPageMainPnl.Controls.Add(this.updateStudentBtn, 1, 2);
            this.mainPageMainPnl.Controls.Add(this.button2, 1, 3);
            this.mainPageMainPnl.Location = new System.Drawing.Point(2, 4);
            this.mainPageMainPnl.Name = "mainPageMainPnl";
            this.mainPageMainPnl.RowCount = 4;
            this.mainPageMainPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.72222F));
            this.mainPageMainPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.27778F));
            this.mainPageMainPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 147F));
            this.mainPageMainPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.mainPageMainPnl.Size = new System.Drawing.Size(806, 509);
            this.mainPageMainPnl.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(28, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 68);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(671, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(117, 68);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Tan;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(316, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 36);
            this.label1.TabIndex = 3;
            this.label1.Text = "Manage Students";
            // 
            // addStudentBtn
            // 
            this.addStudentBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.addStudentBtn.BackColor = System.Drawing.Color.Tan;
            this.addStudentBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStudentBtn.Location = new System.Drawing.Point(315, 126);
            this.addStudentBtn.Name = "addStudentBtn";
            this.addStudentBtn.Size = new System.Drawing.Size(200, 39);
            this.addStudentBtn.TabIndex = 4;
            this.addStudentBtn.Text = "Add A Student";
            this.addStudentBtn.UseVisualStyleBackColor = false;
            this.addStudentBtn.Click += new System.EventHandler(this.addStudentBtn_Click);
            // 
            // updateStudentBtn
            // 
            this.updateStudentBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.updateStudentBtn.BackColor = System.Drawing.Color.Tan;
            this.updateStudentBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateStudentBtn.Location = new System.Drawing.Point(315, 270);
            this.updateStudentBtn.Name = "updateStudentBtn";
            this.updateStudentBtn.Size = new System.Drawing.Size(200, 39);
            this.updateStudentBtn.TabIndex = 5;
            this.updateStudentBtn.Text = "Update A Student";
            this.updateStudentBtn.UseVisualStyleBackColor = false;
            this.updateStudentBtn.Click += new System.EventHandler(this.updateStudentBtn_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.Color.Tan;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(315, 416);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 39);
            this.button2.TabIndex = 6;
            this.button2.Text = "Delete A Student";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // ManageStudentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(811, 516);
            this.Controls.Add(this.mainPageMainPnl);
            this.Name = "ManageStudentsForm";
            this.Text = "ManageStudentsForm";
            this.mainPageMainPnl.ResumeLayout(false);
            this.mainPageMainPnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainPageMainPnl;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addStudentBtn;
        private System.Windows.Forms.Button updateStudentBtn;
        private System.Windows.Forms.Button button2;
    }
}