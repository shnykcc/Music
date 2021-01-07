
namespace Music
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Names = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ArtistName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Locations = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(228, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SandyBrown;
            this.button1.Location = new System.Drawing.Point(87, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 89);
            this.button1.TabIndex = 1;
            this.button1.Text = "Bilgisayarda Olmayan Müzik Kaydet";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SpringGreen;
            this.button2.Location = new System.Drawing.Point(234, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 89);
            this.button2.TabIndex = 2;
            this.button2.Text = "Bilgisayardan Müzik Bul";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Names,
            this.ArtistName,
            this.Locations});
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 207);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(612, 225);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // Names
            // 
            this.Names.Text = "İsim";
            this.Names.Width = 81;
            // 
            // ArtistName
            // 
            this.ArtistName.Text = "Sanatcı";
            this.ArtistName.Width = 84;
            // 
            // Locations
            // 
            this.Locations.Text = "Lokasyonu";
            this.Locations.Width = 96;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(117, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(358, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Moodunuza Ve Sectiğiniz Türe Uygun Müzikler";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Aqua;
            this.button3.Location = new System.Drawing.Point(386, 64);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 89);
            this.button3.TabIndex = 6;
            this.button3.Text = "Mood Ve Tür Değiştir";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Black;
            this.button4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button4.Location = new System.Drawing.Point(12, 458);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 89);
            this.button4.TabIndex = 7;
            this.button4.Text = "Kullanıcı Değiştir";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Red;
            this.button5.Location = new System.Drawing.Point(526, 458);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 89);
            this.button5.TabIndex = 8;
            this.button5.Text = "Uygulamayı Kapat";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button6.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button6.Location = new System.Drawing.Point(130, 458);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(112, 89);
            this.button6.TabIndex = 9;
            this.button6.Text = "Şarkıları Yenile";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button7.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button7.Location = new System.Drawing.Point(248, 458);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(112, 89);
            this.button7.TabIndex = 10;
            this.button7.Text = "Youtube\'da Ara";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button8.Enabled = false;
            this.button8.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button8.Location = new System.Drawing.Point(363, 458);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(112, 89);
            this.button8.TabIndex = 11;
            this.button8.Text = "Bilgisayarda Cal";
            this.button8.UseVisualStyleBackColor = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 559);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ColumnHeader Names;
        private System.Windows.Forms.ColumnHeader ArtistName;
        private System.Windows.Forms.ColumnHeader Locations;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ListView listView1;
    }
}