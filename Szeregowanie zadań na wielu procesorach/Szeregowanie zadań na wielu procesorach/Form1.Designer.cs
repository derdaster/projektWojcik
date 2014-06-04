namespace Szeregowanie_zadań_na_wielu_procesorach
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
            this.testAlgorithms = new System.Windows.Forms.Button();
            this.testLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // testAlgorithms
            // 
            this.testAlgorithms.Location = new System.Drawing.Point(279, 446);
            this.testAlgorithms.Name = "testAlgorithms";
            this.testAlgorithms.Size = new System.Drawing.Size(75, 23);
            this.testAlgorithms.TabIndex = 0;
            this.testAlgorithms.Text = "TEST";
            this.testAlgorithms.UseVisualStyleBackColor = true;
            this.testAlgorithms.Click += new System.EventHandler(this.testAlgorithms_Click);
            // 
            // testLabel
            // 
            this.testLabel.AutoSize = true;
            this.testLabel.Location = new System.Drawing.Point(22, 19);
            this.testLabel.Name = "testLabel";
            this.testLabel.Size = new System.Drawing.Size(31, 13);
            this.testLabel.TabIndex = 1;
            this.testLabel.Text = "Test:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 481);
            this.Controls.Add(this.testLabel);
            this.Controls.Add(this.testAlgorithms);
            this.Name = "Form1";
            this.Text = "Szeregowanie zadań na wielu procesorach";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button testAlgorithms;
        private System.Windows.Forms.Label testLabel;
    }
}

