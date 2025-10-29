namespace EmguCV_NesneTanima
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnBaslat = new Button();
            pbGoruntu = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbGoruntu).BeginInit();
            SuspendLayout();
            // 
            // btnBaslat
            // 
            btnBaslat.Location = new Point(377, 468);
            btnBaslat.Name = "btnBaslat";
            btnBaslat.Size = new Size(120, 56);
            btnBaslat.TabIndex = 0;
            btnBaslat.Text = "Kamera Baslat/Durdur";
            btnBaslat.UseVisualStyleBackColor = true;
            // 
            // pbGoruntu
            // 
            pbGoruntu.Location = new Point(144, 35);
            pbGoruntu.Name = "pbGoruntu";
            pbGoruntu.Size = new Size(605, 412);
            pbGoruntu.TabIndex = 1;
            pbGoruntu.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 582);
            Controls.Add(pbGoruntu);
            Controls.Add(btnBaslat);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pbGoruntu).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnBaslat;
        private PictureBox pbGoruntu;
    }
}
