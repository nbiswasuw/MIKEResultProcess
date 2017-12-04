namespace HDResultProcess
{
    partial class Form1
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
            this.btnSingleDFS0 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnLoadHD = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnDFS0Fromlist = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnHD = new System.Windows.Forms.Button();
            this.btnNAM = new System.Windows.Forms.Button();
            this.btnLoadNAM = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSingleDFS0
            // 
            this.btnSingleDFS0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSingleDFS0.Location = new System.Drawing.Point(224, 115);
            this.btnSingleDFS0.Name = "btnSingleDFS0";
            this.btnSingleDFS0.Size = new System.Drawing.Size(90, 29);
            this.btnSingleDFS0.TabIndex = 0;
            this.btnSingleDFS0.Text = "Make DFS0";
            this.btnSingleDFS0.UseVisualStyleBackColor = true;
            this.btnSingleDFS0.Visible = false;
            this.btnSingleDFS0.Click += new System.EventHandler(this.btnSingleDFS0_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select River";
            this.label1.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(96, 120);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(122, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Visible = false;
            // 
            // btnLoadHD
            // 
            this.btnLoadHD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadHD.Location = new System.Drawing.Point(12, 48);
            this.btnLoadHD.Name = "btnLoadHD";
            this.btnLoadHD.Size = new System.Drawing.Size(93, 33);
            this.btnLoadHD.TabIndex = 5;
            this.btnLoadHD.Text = "Load File";
            this.btnLoadHD.UseVisualStyleBackColor = true;
            this.btnLoadHD.Visible = false;
            this.btnLoadHD.Click += new System.EventHandler(this.btnLoadHD_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(253, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(61, 25);
            this.button3.TabIndex = 8;
            this.button3.Text = "Help";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDFS0Fromlist
            // 
            this.btnDFS0Fromlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDFS0Fromlist.Location = new System.Drawing.Point(13, 147);
            this.btnDFS0Fromlist.Name = "btnDFS0Fromlist";
            this.btnDFS0Fromlist.Size = new System.Drawing.Size(135, 34);
            this.btnDFS0Fromlist.TabIndex = 9;
            this.btnDFS0Fromlist.Text = "DFS0 From List";
            this.btnDFS0Fromlist.UseVisualStyleBackColor = true;
            this.btnDFS0Fromlist.Visible = false;
            this.btnDFS0Fromlist.Click += new System.EventHandler(this.btnDFS0Fromlist_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Loading Timesteps . . .";
            this.label2.Visible = false;
            // 
            // btnHD
            // 
            this.btnHD.Location = new System.Drawing.Point(13, 12);
            this.btnHD.Name = "btnHD";
            this.btnHD.Size = new System.Drawing.Size(100, 30);
            this.btnHD.TabIndex = 11;
            this.btnHD.Text = "HD Result";
            this.btnHD.UseVisualStyleBackColor = true;
            this.btnHD.Click += new System.EventHandler(this.btnHD_Click);
            // 
            // btnNAM
            // 
            this.btnNAM.Location = new System.Drawing.Point(126, 12);
            this.btnNAM.Name = "btnNAM";
            this.btnNAM.Size = new System.Drawing.Size(100, 30);
            this.btnNAM.TabIndex = 12;
            this.btnNAM.Text = "NAM Result";
            this.btnNAM.UseVisualStyleBackColor = true;
            this.btnNAM.Click += new System.EventHandler(this.btnNAM_Click);
            // 
            // btnLoadNAM
            // 
            this.btnLoadNAM.Location = new System.Drawing.Point(126, 48);
            this.btnLoadNAM.Name = "btnLoadNAM";
            this.btnLoadNAM.Size = new System.Drawing.Size(100, 33);
            this.btnLoadNAM.TabIndex = 13;
            this.btnLoadNAM.Text = "Browse File";
            this.btnLoadNAM.UseVisualStyleBackColor = true;
            this.btnLoadNAM.Visible = false;
            this.btnLoadNAM.Click += new System.EventHandler(this.btnLoadNAM_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 196);
            this.Controls.Add(this.btnLoadNAM);
            this.Controls.Add(this.btnNAM);
            this.Controls.Add(this.btnHD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDFS0Fromlist);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnLoadHD);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSingleDFS0);
            this.Name = "Form1";
            this.Text = "MIKE Model Result Process";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSingleDFS0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnLoadHD;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnDFS0Fromlist;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnHD;
        private System.Windows.Forms.Button btnNAM;
        private System.Windows.Forms.Button btnLoadNAM;
    }
}

