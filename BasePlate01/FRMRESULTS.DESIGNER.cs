namespace BasePlate01
{
    partial class frmResults
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
            this.txtOutputs = new System.Windows.Forms.TextBox();
            this.txtWarnings = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCalculations = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bttnClose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 378);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Demand / Capacity Ratios";
            // 
            // txtOutputs
            // 
            this.txtOutputs.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputs.Location = new System.Drawing.Point(16, 398);
            this.txtOutputs.Multiline = true;
            this.txtOutputs.Name = "txtOutputs";
            this.txtOutputs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutputs.Size = new System.Drawing.Size(954, 158);
            this.txtOutputs.TabIndex = 1;
            // 
            // txtWarnings
            // 
            this.txtWarnings.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWarnings.Location = new System.Drawing.Point(16, 588);
            this.txtWarnings.Multiline = true;
            this.txtWarnings.Name = "txtWarnings";
            this.txtWarnings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtWarnings.Size = new System.Drawing.Size(954, 158);
            this.txtWarnings.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 568);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Warnings";
            // 
            // txtCalculations
            // 
            this.txtCalculations.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCalculations.Location = new System.Drawing.Point(16, 28);
            this.txtCalculations.Multiline = true;
            this.txtCalculations.Name = "txtCalculations";
            this.txtCalculations.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCalculations.Size = new System.Drawing.Size(954, 338);
            this.txtCalculations.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Calculations";
            // 
            // bttnClose
            // 
            this.bttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnClose.Location = new System.Drawing.Point(820, 773);
            this.bttnClose.Name = "bttnClose";
            this.bttnClose.Size = new System.Drawing.Size(150, 30);
            this.bttnClose.TabIndex = 6;
            this.bttnClose.Text = "&Close";
            this.bttnClose.UseVisualStyleBackColor = true;
            this.bttnClose.Click += new System.EventHandler(this.bttnClose_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 773);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button1.Size = new System.Drawing.Size(224, 30);
            this.button1.TabIndex = 7;
            this.button1.Text = "&Write Report to Text File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.bttnClose;
            this.ClientSize = new System.Drawing.Size(982, 815);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bttnClose);
            this.Controls.Add(this.txtCalculations);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWarnings);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOutputs);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmResults";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Results";
            this.Load += new System.EventHandler(this.frmResults_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOutputs;
        private System.Windows.Forms.TextBox txtWarnings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCalculations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bttnClose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}