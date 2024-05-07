using System.ComponentModel;

namespace NormandiaClient.ro.mpp
{
    partial class Hello
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNume = new System.Windows.Forms.TextBox();
            this.textBoxLocuri = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonRezervare = new System.Windows.Forms.Button();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.textBoxDestinatie = new System.Windows.Forms.TextBox();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(48, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nume client";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(48, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Numar locuri";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(48, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cursa";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxNume
            // 
            this.textBoxNume.Location = new System.Drawing.Point(181, 73);
            this.textBoxNume.Name = "textBoxNume";
            this.textBoxNume.Size = new System.Drawing.Size(127, 22);
            this.textBoxNume.TabIndex = 3;
            // 
            // textBoxLocuri
            // 
            this.textBoxLocuri.Location = new System.Drawing.Point(181, 123);
            this.textBoxLocuri.Name = "textBoxLocuri";
            this.textBoxLocuri.Size = new System.Drawing.Size(127, 22);
            this.textBoxLocuri.TabIndex = 4;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(61, 383);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(647, 100);
            this.listBox1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(266, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 49);
            this.label4.TabIndex = 6;
            this.label4.Text = "Firma transport";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonRezervare
            // 
            this.buttonRezervare.Location = new System.Drawing.Point(593, 85);
            this.buttonRezervare.Name = "buttonRezervare";
            this.buttonRezervare.Size = new System.Drawing.Size(115, 41);
            this.buttonRezervare.TabIndex = 7;
            this.buttonRezervare.Text = "Add Rezervare";
            this.buttonRezervare.UseVisualStyleBackColor = true;
            this.buttonRezervare.Click += new System.EventHandler(this.buttonRezervare_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(593, 154);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(115, 32);
            this.buttonLogout.TabIndex = 8;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(52, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 9;
            this.label5.Text = "Toate cursele";
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(61, 302);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(75, 23);
            this.buttonFilter.TabIndex = 10;
            this.buttonFilter.Text = "Filter";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // textBoxDestinatie
            // 
            this.textBoxDestinatie.Location = new System.Drawing.Point(277, 302);
            this.textBoxDestinatie.Name = "textBoxDestinatie";
            this.textBoxDestinatie.Size = new System.Drawing.Size(160, 22);
            this.textBoxDestinatie.TabIndex = 11;
            // 
            // textBoxData
            // 
            this.textBoxData.Location = new System.Drawing.Point(549, 302);
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(159, 22);
            this.textBoxData.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(443, 305);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 13;
            this.label6.Text = "Data plecarii:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(181, 306);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 23);
            this.label7.TabIndex = 14;
            this.label7.Text = "Destinatia:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(181, 163);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(222, 24);
            this.comboBox1.TabIndex = 15;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(61, 535);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(647, 116);
            this.listBox2.TabIndex = 16;
            // 
            // Hello
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 690);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxData);
            this.Controls.Add(this.textBoxDestinatie);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.buttonRezervare);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBoxLocuri);
            this.Controls.Add(this.textBoxNume);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Hello";
            this.Text = "Hello";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ListBox listBox2;

        private System.Windows.Forms.ComboBox comboBox1;

        private System.Windows.Forms.Label label7;

        private System.Windows.Forms.Label label6;

        private System.Windows.Forms.TextBox textBoxDestinatie;
        private System.Windows.Forms.TextBox textBoxData;

        private System.Windows.Forms.Button buttonFilter;

        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.Button buttonLogout;

        private System.Windows.Forms.Button buttonRezervare;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.TextBox textBoxNume;
        private System.Windows.Forms.TextBox textBoxLocuri;
        private System.Windows.Forms.ListBox listBox1;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}